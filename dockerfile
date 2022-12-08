FROM mcr.microsoft.com/dotnet/sdk:6.0
COPY . /src/myWebApp
RUN mkdir /app
ENV ParkingContextConnectionString='Server=mssql1;Database=parcIndus;User Id=parcadmin;Password=!@pside2022!;'
ENV WebsiteUri='http://localhost'
RUN mv /src/myWebApp/GestionPlacesParking/Settings/keycloak.Development.json /src/myWebApp/GestionPlacesParking/Settings/keycloak.Production.json 
RUN dotnet tool install -g dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
WORKDIR /src/myWebApp
RUN dotnet restore /src/myWebApp/GestionPlacesParking/GestionPlacesParking.Web.UI.csproj
RUN dotnet build /src/myWebApp/GestionPlacesParking/GestionPlacesParking.Web.UI.csproj -c Release -o /app/
RUN dotnet publish /src/myWebApp/GestionPlacesParking/GestionPlacesParking.Web.UI.csproj -c Release -o /app/
CMD  ["bash","/src/myWebApp/DevOps/builder/migration.sh"]