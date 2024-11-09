# Sử dụng image .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /source
COPY . .
RUN mkdir -p ./Base
COPY ../Base/* ./Base

RUN dotnet restore "./Authen.API/Authen.API.csproj" --disable-parallel
RUN dotnet publish "./Authen.API/Authen.API.csproj" -c Release -o /app --no-restore

# Sử dụng image runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app ./

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Authen.API.dll"]
