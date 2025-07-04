# Get the latest tag starting with 'web'
$lastTag = git tag --list "web-*" --sort=-v:refname | Select-Object -First 1
if (-not $lastTag) { $lastTag = "web0.0.0" }

# Extract the numeric part (remove 'web')
$version = $lastTag -replace '^web-', ''
$parts = $version -split '\.'

# Ensure version parts are numeric
if ($parts.Count -ne 3 -or $parts | Where-Object { $_ -notmatch '^\d+$' }) {
    Write-Error "Latest tag '$lastTag' does not match expected format 'webX.Y.Z'."
    exit 1
}

# Bump minor version
$newTag = "web$($parts[0]).$([int]$parts[1] + 1).0"

# Create new tag
git tag $newTag
git push origin $newTag

# Write to version file (adjust path as needed; example: wwwroot/version.txt)
Set-Content -Path "./wwwroot/version.txt" -Value $newTag