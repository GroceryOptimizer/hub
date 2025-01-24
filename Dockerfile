# Build, Test and Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /app

COPY . ./

RUN dotnet restore

RUN dotnet build --no-restore -c Release -o /out

RUN dotnet test --no-restore -c Release

RUN dotnet publish --no-restore -c Release -o /out

# Run the application
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine AS runtime

WORKDIR /app

COPY --from=build /out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "hub.dll"]
