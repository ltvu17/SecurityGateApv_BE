#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update; apt-get install -y fontconfig
# refresh system font cache
RUN fc-cache -f -v
COPY "SecurityGateApv.Infras/Fonts/ARIAL.TTF" "/usr/local/share/fonts/"
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ARG DATABASE_ENV=Server=nmh1223.myvnc.com;Uid=sa;Pwd=Password789;Database=SecurityGateApv;TrustServerCertificate=True
ENV ConnectionStrings=$DATABASE_ENV
ENV TZ="Asia/Ho_Chi_Minh"
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SecurityGateApv.WebApi/SecurityGateApv.WebApi.csproj", "SecurityGateApv.WebApi/"]
RUN dotnet restore "./SecurityGateApv.WebApi/./SecurityGateApv.WebApi.csproj"
COPY . .
WORKDIR "/src/SecurityGateApv.WebApi"
RUN dotnet build "./SecurityGateApv.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SecurityGateApv.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecurityGateApv.WebApi.dll"]