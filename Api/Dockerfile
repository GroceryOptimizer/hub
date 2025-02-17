#syntax
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

#Copy from the project files
COPY . ./Api
COPY ../Core ./Core
COPY ../Data ./Data
COPY ../StoreApi ./StoreApi

#Restore dependencies
WORKDIR /app/Api
RUN dotnet restore

#Buil the application
RUN dotnet publish -c Release -o /publish

#Use a lightweight runtime image for final execution
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

#Expose the port the hub runs on
EXPOSE 7049
EXPOSE 5241

#Set the entry point to start the hub
CMD ["dotnet", "Api.dll"]
