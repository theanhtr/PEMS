# Sử dụng image .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /source

# Copy toàn bộ BuildFolder để đảm bảo Docker có quyền truy cập vào cả Report.API và Base
COPY . .

# Copy file cần thiết từ Base vào thư mục tạm trong image
COPY ./Base /source/Base

# Restore và publish dự án từ thư mục Report.API
WORKDIR /source/Report.API
RUN dotnet restore "./Report.API.csproj" --disable-parallel
RUN dotnet publish "./Report.API.csproj" -c Release -o /app --no-restore

# Sử dụng image runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy ứng dụng đã được publish từ giai đoạn build
COPY --from=build-env /app ./

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Report.API.dll"]