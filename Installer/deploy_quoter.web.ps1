param(
    [Parameter(Mandatory=$true)][string]$TAG,
    [Parameter(Mandatory=$true)][string]$USER,
    [Parameter(Mandatory=$true)][string]$PASS,
    [Parameter(Mandatory=$true)][string]$REG,
    [Parameter(Mandatory=$true)][string]$SSH
)

# 1. Create git TAG with web- prefix
Write-Host "Creating new tag" -ForegroundColor Green
$webTag = "web-$TAG"
git tag $webTag
git push origin $webTag
Write-Host "Created tag: $webTag" -ForegroundColor Green

# 2. Publish the Quoter.Web project
Write-Host "Publishing Quoter.Web" -ForegroundColor Green
$publishDir = "../Quoter/Quoter.Web/bin/Release/net6.0/publish"
dotnet publish ../Quoter/Quoter.Web/Quoter.Web.csproj -c Release -o $publishDir

# 2.1 Add version file with TAG inside the publish folder
Write-Host "Adding version info to publish directory" -ForegroundColor Green
Set-Content -Path "$publishDir\version" -Value $TAG

# 3. Build docker image
Write-Host "Building docker image" -ForegroundColor Green
$imageName = "$REG/minuteverse/quoter.web:$TAG"
docker build -t $imageName $publishDir

# 4. Push docker image
Write-Host "Pushing docker image: $imageName" -ForegroundColor Green
docker push $imageName

# 5. Run remote commands
Write-Host "Running remote commands" -ForegroundColor Green
ssh $SSH "docker stop minuteverse || true && docker rm minuteverse || true && docker pull $imageName && docker run -d --restart unless-stopped -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Production -e QUOTER_ADMIN_USER=$USER -e QUOTER_ADMIN_PASS=$PASS -v minuteverse:/root/.local/share --name minuteverse $imageName"

Write-Host "Deployment complete for $imageName" -ForegroundColor Green