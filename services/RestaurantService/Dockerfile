FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RestaurantService.API/RestaurantService.API.csproj", "RestaurantService.API/"]
COPY ["RestaurantService.BLL/RestaurantService.BLL.csproj", "RestaurantService.BLL/"]
COPY ["RestaurantService.DAL/RestaurantService.DAL.csproj", "RestaurantService.DAL/"]
RUN dotnet restore "RestaurantService.API/RestaurantService.API.csproj"
COPY . .
WORKDIR "/src/RestaurantService.API"
RUN dotnet build "./RestaurantService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RestaurantService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestaurantService.API.dll"]
