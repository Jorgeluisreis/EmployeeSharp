FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 2041

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmployeeSharp.Web/EmployeeSharp.Web.csproj", "EmployeeSharp.Web/"]
RUN dotnet restore "./EmployeeSharp.Web/EmployeeSharp.Web.csproj"
COPY . .
WORKDIR "/src/EmployeeSharp.Web"
RUN dotnet build "./EmployeeSharp.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EmployeeSharp.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeSharp.Web.dll"]
