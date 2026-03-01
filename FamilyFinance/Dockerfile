FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["FamilyFinance/FamilyFinance.csproj", "FamilyFinance/"]
COPY ["FamilyFinance.Application/FamilyFinance.Application.csproj", "FamilyFinance.Application/"]
COPY ["FamilyFinance.Domain/FamilyFinance.Domain.csproj", "FamilyFinance.Domain/"]
COPY ["FamilyFinance.Core/FamilyFinance.Core.csproj", "FamilyFinance.Core/"]
COPY ["FamilyFinance.DTO/FamilyFinance.DTO.csproj", "FamilyFinance.DTO/"]
COPY ["FamilyFinance.Persistence/FamilyFinance.Persistence.csproj", "FamilyFinance.Persistence/"]
COPY ["FamilyFinance.Infrastructure/FamilyFinance.Infrastructure.csproj", "FamilyFinance.Infrastructure/"]

RUN dotnet restore "FamilyFinance/FamilyFinance.csproj"

COPY ["FamilyFinance/", "FamilyFinance/"]
COPY ["FamilyFinance.Application/", "FamilyFinance.Application/"]
COPY ["FamilyFinance.Domain/", "FamilyFinance.Domain/"]
COPY ["FamilyFinance.Core/", "FamilyFinance.Core/"]
COPY ["FamilyFinance.DTO/", "FamilyFinance.DTO/"]
COPY ["FamilyFinance.Persistence/", "FamilyFinance.Persistence/"]
COPY ["FamilyFinance.Infrastructure/", "FamilyFinance.Infrastructure/"]

FROM build AS publish
WORKDIR "/src/FamilyFinance"
RUN dotnet publish "./FamilyFinance.csproj" --no-restore -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FamilyFinance.dll"]