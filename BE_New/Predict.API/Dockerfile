# Sử dụng image .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /source

# Copy file cần thiết từ Base vào thư mục tạm trong image
COPY ./Base ./Base
COPY ./Predict.API ./Predict.API

RUN apt -y update && apt -y upgrade && apt install -y curl && apt install -y nano
RUN ln -f -s /usr/share/zoneinfo/Etc/GMT-7 /etc/localtime

RUN dotnet restore "./Predict.API/Predict.API/Predict.API.csproj" --disable-parallel
RUN dotnet publish "./Predict.API/Predict.API/Predict.API.csproj" -c Release -o /app --no-restore

# Sử dụng image runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy ứng dụng đã được publish từ giai đoạn build
COPY --from=build-env /app ./

EXPOSE 80
EXPOSE 8080
EXPOSE 443

ENTRYPOINT ["dotnet", "Predict.API.dll"]