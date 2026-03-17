# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TestAudisoft/TestAudisoft.csproj", "TestAudisoft/"]
COPY ["TestAudisoft.Application/TestAudisoft.Application.csproj", "TestAudisoft.Application/"]
COPY ["TestAudisoft.Domain/TestAudisoft.Domain.csproj", "TestAudisoft.Domain/"]
COPY ["TestAudisoft.Infrastructure/TestAudisoft.Infrastructure.csproj", "TestAudisoft.Infrastructure/"]

RUN dotnet restore "TestAudisoft/TestAudisoft.csproj"

COPY . .

WORKDIR "/src/TestAudisoft"
RUN dotnet publish "TestAudisoft.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "TestAudisoft.dll"]