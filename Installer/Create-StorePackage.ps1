param(
	[Parameter(Mandatory = $true)]
	[ValidatePattern('^[A-Za-z0-9.-]{3,50}$')]
	[string]$PackageName,

	[Parameter(Mandatory = $true)]
	[string]$Publisher,

	[Parameter(Mandatory = $true)]
	[string]$PublisherDisplayName,

	[string]$DisplayName = 'Minute Verse',
	[string]$Version,
	[string]$OutputDirectory = (Join-Path $PSScriptRoot 'AppPackages')
)

$ErrorActionPreference = 'Stop'

if ([string]::IsNullOrWhiteSpace($Publisher)) {
	throw 'Publisher cannot be empty. Use the exact Publisher value from Partner Center.'
}
if ([string]::IsNullOrWhiteSpace($PublisherDisplayName)) {
	throw 'PublisherDisplayName cannot be empty.'
}
if ([string]::IsNullOrWhiteSpace($DisplayName)) {
	throw 'DisplayName cannot be empty.'
}

$projectPath = Join-Path $PSScriptRoot '..\Quoter\Quoter.App\Quoter.App.csproj'
$logoPath = Join-Path $PSScriptRoot '..\Quoter\Quoter.App\Resources\MinuteVerseLogoSmall.png'
$manifestPath = Join-Path $PSScriptRoot 'Msix\AppxManifest.xml'

if (-not $Version) {
	$projectXml = [xml][System.IO.File]::ReadAllText((Resolve-Path $projectPath))
	$Version = $projectXml.Project.PropertyGroup.Version
}
if ($Version -notmatch '^\d+\.\d+\.\d+(\.\d+)?$') {
	throw "Invalid package version '$Version'. Use three or four numeric components."
}
if ($Version.Split('.').Length -eq 3) {
	$Version += '.0'
}

$parsedVersion = [version]$Version
foreach ($component in @($parsedVersion.Major, $parsedVersion.Minor, $parsedVersion.Build, $parsedVersion.Revision)) {
	if ($component -gt 65535) {
		throw "Invalid package version '$Version'. Each component must be 65535 or lower."
	}
}

$sdkBin = Join-Path ${env:ProgramFiles(x86)} 'Windows Kits\10\bin'
$makeAppx = Get-ChildItem -Path (Join-Path $sdkBin '*\x64\makeappx.exe') -ErrorAction SilentlyContinue |
	Sort-Object { [version]$_.Directory.Parent.Name } -Descending |
	Select-Object -First 1
if (-not $makeAppx) {
	throw "makeappx.exe was not found under '$sdkBin'. Install the Windows 10/11 SDK."
}

$outputDirectory = [System.IO.Path]::GetFullPath($OutputDirectory)
[System.IO.Directory]::CreateDirectory($outputDirectory) | Out-Null
$packagePath = Join-Path $outputDirectory ($PackageName + '_' + $Version + '_x64.msix')
$stagePath = Join-Path ([System.IO.Path]::GetTempPath()) ('MinuteVerse-MSIX-' + [guid]::NewGuid().ToString('N'))
$packageRoot = Join-Path $stagePath 'Package'
$assetsPath = Join-Path $packageRoot 'Assets'

try {
	[System.IO.Directory]::CreateDirectory($packageRoot) | Out-Null
	[System.IO.Directory]::CreateDirectory($assetsPath) | Out-Null

	& dotnet publish $projectPath -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o $packageRoot
	if ($LASTEXITCODE -ne 0) {
		throw "dotnet publish failed with exit code $LASTEXITCODE."
	}

	Add-Type -AssemblyName System.Drawing
	$sourceLogo = [System.Drawing.Image]::FromFile((Resolve-Path $logoPath))
	try {
		$logoSizes = @{
			'StoreLogo.png' = @(50, 50)
			'Square44x44Logo.png' = @(44, 44)
			'Square71x71Logo.png' = @(71, 71)
			'Square150x150Logo.png' = @(150, 150)
			'Square310x310Logo.png' = @(310, 310)
			'Wide310x150Logo.png' = @(310, 150)
		}

		foreach ($logo in $logoSizes.GetEnumerator()) {
			$width = $logo.Value[0]
			$height = $logo.Value[1]
			$bitmap = New-Object System.Drawing.Bitmap($width, $height)
			try {
				$graphics = [System.Drawing.Graphics]::FromImage($bitmap)
				try {
					$graphics.Clear([System.Drawing.Color]::Transparent)
					$graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
					$graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality

					$scale = [Math]::Min(($width - 4) / $sourceLogo.Width, ($height - 4) / $sourceLogo.Height)
					$drawWidth = [int][Math]::Round($sourceLogo.Width * $scale)
					$drawHeight = [int][Math]::Round($sourceLogo.Height * $scale)
					$x = [int][Math]::Floor(($width - $drawWidth) / 2)
					$y = [int][Math]::Floor(($height - $drawHeight) / 2)
					$graphics.DrawImage($sourceLogo, $x, $y, $drawWidth, $drawHeight)
				}
				finally {
					$graphics.Dispose()
				}

				$bitmap.Save((Join-Path $assetsPath $logo.Key), [System.Drawing.Imaging.ImageFormat]::Png)
			}
			finally {
				$bitmap.Dispose()
			}
		}
	}
	finally {
		$sourceLogo.Dispose()
	}

	$xmlEscape = [System.Security.SecurityElement]
	$manifest = [System.IO.File]::ReadAllText((Resolve-Path $manifestPath))
	$manifest = $manifest.Replace('{{PACKAGE_NAME}}', $xmlEscape::Escape($PackageName))
	$manifest = $manifest.Replace('{{PUBLISHER}}', $xmlEscape::Escape($Publisher))
	$manifest = $manifest.Replace('{{PUBLISHER_DISPLAY_NAME}}', $xmlEscape::Escape($PublisherDisplayName))
	$manifest = $manifest.Replace('{{DISPLAY_NAME}}', $xmlEscape::Escape($DisplayName))
	$manifest = $manifest.Replace('{{VERSION}}', $Version)
	[System.IO.File]::WriteAllText((Join-Path $packageRoot 'AppxManifest.xml'), $manifest, (New-Object System.Text.UTF8Encoding($false)))

	& $makeAppx.FullName pack /d $packageRoot /p $packagePath /o
	if ($LASTEXITCODE -ne 0) {
		throw "makeappx pack failed with exit code $LASTEXITCODE."
	}

	Write-Host "Created Store package: $packagePath"
}
finally {
	if (Test-Path $stagePath) {
		Remove-Item -LiteralPath $stagePath -Recurse -Force
	}
}
