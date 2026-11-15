FROM mcr.microsoft.com/dotnet/aspnet:10.0

LABEL description="Quoter.Web docker image using .NET 10"

# Copy all files and folder from current client directory to container's directory
COPY . /var/www/quoter

# Set the current working directory
WORKDIR /var/www/quoter

# Expose port 8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Quoter.Web.dll"]