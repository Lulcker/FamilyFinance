FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["FamilyFinance.UI/FamilyFinance.UI.csproj", "FamilyFinance.UI/"]
COPY ["FamilyFinance.ProxyApiMethods/FamilyFinance.ProxyApiMethods.csproj", "FamilyFinance.ProxyApiMethods/"]
COPY ["FamilyFinance/FamilyFinance.csproj", "FamilyFinance/"]
COPY ["FamilyFinance.Application/FamilyFinance.Application.csproj", "FamilyFinance.Application/"]
COPY ["FamilyFinance.Domain/FamilyFinance.Domain.csproj", "FamilyFinance.Domain/"]
COPY ["FamilyFinance.Core/FamilyFinance.Core.csproj", "FamilyFinance.Core/"]
COPY ["FamilyFinance.DTO/FamilyFinance.DTO.csproj", "FamilyFinance.DTO/"]
COPY ["FamilyFinance.Persistence/FamilyFinance.Persistence.csproj", "FamilyFinance.Persistence/"]
COPY ["FamilyFinance.Infrastructure/FamilyFinance.Infrastructure.csproj", "FamilyFinance.Infrastructure/"]
COPY ["FamilyFinance.Generators/FamilyFinance.Generators.csproj", "FamilyFinance.Generators/"]

RUN dotnet restore "FamilyFinance.UI/FamilyFinance.UI.csproj"

COPY ["FamilyFinance.UI/", "FamilyFinance.UI/"]
COPY ["FamilyFinance.ProxyApiMethods/", "FamilyFinance.ProxyApiMethods/"]
COPY ["FamilyFinance/", "FamilyFinance/"]
COPY ["FamilyFinance.Application/", "FamilyFinance.Application/"]
COPY ["FamilyFinance.Domain/", "FamilyFinance.Domain/"]
COPY ["FamilyFinance.Core/", "FamilyFinance.Core/"]
COPY ["FamilyFinance.DTO/", "FamilyFinance.DTO/"]
COPY ["FamilyFinance.Persistence/", "FamilyFinance.Persistence/"]
COPY ["FamilyFinance.Infrastructure/", "FamilyFinance.Infrastructure/"]
COPY ["FamilyFinance.Generators/", "FamilyFinance.Generators/"]

FROM build AS publish
WORKDIR "/src/FamilyFinance.UI"
RUN dotnet publish "./FamilyFinance.UI.csproj" --no-restore -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final

RUN rm -rf /usr/share/nginx/html/*

COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf

ARG API_BASE_URL
RUN echo "{ \"ApiBaseUrl\": \"${API_BASE_URL}\" }" > /usr/share/nginx/html/appsettings.json

EXPOSE 80