#!/bin/sh
/root/.dotnet/tools/dotnet-ef migrations add "AddTableUser"
/root/.dotnet/tools/dotnet-ef database update --project=/src/myWebApp/GestionPlacesParking.Core.Infrastructure
