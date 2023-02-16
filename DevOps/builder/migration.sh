#!/bin/bash

#Compilation du projet .net et creation des fichiers executable de l'app dans le dossier /app
dotnet restore /src/myWebApp/GestionPlacesParking/GestionPlacesParking.Web.UI.csproj
dotnet build /src/myWebApp/GestionPlacesParking/GestionPlacesParking.Web.UI.csproj -c Release -o /app/
dotnet publish /src/myWebApp/GestionPlacesParking/GestionPlacesParking.Web.UI.csproj -c Release -o /app/

#Execution des migrations apporté au projets
/root/.dotnet/tools/dotnet-ef migrations add "AddTableUser" --project=/src/myWebApp/GestionPlacesParking.Core.Infrastructure
/root/.dotnet/tools/dotnet-ef database update --project=/src/myWebApp/GestionPlacesParking.Core.Infrastructure

#Désinstall du SDK7.0 et suppression du dossier projet
apt-get remove -y dotnet-sdk-7.0
rm -f -R /src/myWebApp

#Execution de l'application avec ASPNET
dotnet /app/GestionPlacesParking.Web.UI.dll
