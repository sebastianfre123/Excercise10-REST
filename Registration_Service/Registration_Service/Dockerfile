FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Registration_Service/Registration_Service.csproj", "Registration_Service/"]
RUN dotnet restore "Registration_Service/Registration_Service.csproj"
COPY . .
WORKDIR "/src/Registration_Service"
RUN dotnet build "Registration_Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Registration_Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Registration_Service.dll"]
