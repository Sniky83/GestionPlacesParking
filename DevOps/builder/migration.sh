#!/bin/bash
/root/.dotnet/tools/dotnet-ef migrations add "AddTableUser" --project=/src/myWebApp/GestionPlacesParking.Core.Infrastructure
/root/.dotnet/tools/dotnet-ef database update --project=/src/myWebApp/GestionPlacesParking.Core.Infrastructure
rm -f -R /src/myWebApp
apt-get remove -y dotnet-sdk-7.0
dotnet /app/GestionPlacesParking.Web.UI.dll
