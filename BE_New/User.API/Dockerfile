# Sử dụng image .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /source

# Copy file cần thiết từ Base vào thư mục tạm trong image
COPY ./Base ./Base
COPY ./User.API ./User.API

RUN dotnet restore "./User.API/User.API/User.API.csproj" --disable-parallel
RUN dotnet publish "./User.API/User.API/User.API.csproj" -c Release -o /app --no-restore

# Sử dụng image runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy ứng dụng đã được publish từ giai đoạn build
COPY --from=build-env /app ./

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "User.API.dll"]