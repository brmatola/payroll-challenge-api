FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["payroll-challenge-api/payroll-challenge-api.csproj", "payroll-challenge-api/"]
RUN dotnet restore "payroll-challenge-api/payroll-challenge-api.csproj"
COPY . .
WORKDIR "/src/payroll-challenge-api"
RUN dotnet build "payroll-challenge-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "payroll-challenge-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "payroll-challenge-api.dll"]
