FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/UserWebService/UserWebService.csproj", "src/UserWebService/"]
RUN dotnet restore "src/UserWebService/UserWebService.csproj"
COPY . .
WORKDIR "/src/src/UserWebService"
RUN dotnet build "UserWebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserWebService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserWebService.dll"]
