FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore WebAppTemplate.API/WebAppTemplate.API.csproj

RUN dotnet publish WebAppTemplate.API/WebAppTemplate.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebAppTemplate.API.dll"]