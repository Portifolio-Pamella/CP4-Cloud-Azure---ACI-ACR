# ==========================================
# ETAPA 1 - BUILD
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copia o arquivo do projeto
COPY Aegis.Api/Aegis.Api.csproj Aegis.Api/

# Restaura as dependências
RUN dotnet restore Aegis.Api/Aegis.Api.csproj

# Copia o restante do projeto
COPY Aegis.Api/ Aegis.Api/

# Publica a aplicação
RUN dotnet publish Aegis.Api/Aegis.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


# ==========================================
# ETAPA 2 - RUNTIME
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

# Cria usuário sem privilégios administrativos
RUN useradd --create-home --uid 1001 appuser

# Copia a aplicação publicada
COPY --from=build /app/publish .

# Executa como usuário não-root
USER appuser

# Porta da aplicação
EXPOSE 8080

# Informa ao ASP.NET Core para escutar na porta 8080
ENV ASPNETCORE_URLS=http://+:8080

# Inicializa a API
ENTRYPOINT ["dotnet", "Aegis.Api.dll"]