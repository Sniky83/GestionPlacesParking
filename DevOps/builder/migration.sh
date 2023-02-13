#!/bin/bash
/root/.dotnet/tools/dotnet-ef migrations add "AddTableUser" --project=/src/myWebApp/GestionPlacesParking.Core.Infrastructure
/root/.dotnet/tools/dotnet-ef database update --project=/src/myWebApp/GestionPlacesParking.Core.Infrastructure
dotnet /app/GestionPlacesParking.Web.UI.dll
