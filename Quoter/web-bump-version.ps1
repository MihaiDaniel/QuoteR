# Get the latest tag starting with 'web'
$lastTag = git tag --list "web*" --sort=-v:refname | Select-Object -First 1
if (-not $lastTag) { $lastTag = "web0.0.0" }

# Trim and extract version number using regex
if ($lastTag -match '^web(\d+)\.(\d+)\.(\d+)$') {
    $major = [int]$matches[1]
    $minor = [int]$matches[2]
    $patch = [int]$matches[3]
}
else {
    Write-Error "Latest tag '$lastTag' does not match expected format 'webX.Y.Z'."
    exit 1
}

# Bump minor version, reset patch
$minor++
$newTag = "web$major.$minor.0"

# Create new tag
git tag $newTag
git push origin $newTag

# Write to version file
Set-Content -Path "Quoter.Web/version.txt" -Value $newTag