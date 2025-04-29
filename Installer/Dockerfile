FROM mcr.microsoft.com/dotnet/aspnet:6.0

LABEL description="Quoter.Web docker image using .NET 6"

# Copy all files and folder from current client directory to container's directory
COPY . /var/www/quoter

# Set the current working directory
WORKDIR /var/www/quoter

# Expose port 80
EXPOSE 80

ENTRYPOINT ["dotnet", "Quoter.Web.dll"]