#!/bin/bash
dotnet restore /src/myWebApp/GestionPlacesParking/GestionPlacesParking.Web.UI.csproj
dotnet build /src/myWebApp/GestionPlacesParking/GestionPlacesParking.Web.UI.csproj -c Release -o /app/
dotnet publish /src/myWebApp/GestionPlacesParking/GestionPlacesParking.Web.UI.csproj -c Release -o /app/
/root/.dotnet/tools/dotnet-ef migrations add "AddTableUser" --project=/src/myWebApp/GestionPlacesParking.Core.Infrastructure
/root/.dotnet/tools/dotnet-ef database update --project=/src/myWebApp/GestionPlacesParking.Core.Infrastructure
rm -f -R /src/myWebApp
apt-get remove -y dotnet-sdk-7.0
dotnet /app/GestionPlacesParking.Web.UI.dll
