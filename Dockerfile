# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivos de solución y de proyectos
COPY *.sln ./
COPY CentroEventos.Aplicacion/*.csproj CentroEventos.Aplicacion/
COPY CentroEventos.Repositorios/*.csproj CentroEventos.Repositorios/
COPY CentroEventos.UI/*.csproj CentroEventos.UI/

# Restaurar dependencias
RUN dotnet restore

# Copiar el resto del código fuente
COPY . .

# Publicar en modo Release
RUN dotnet publish CentroEventos.UI/CentroEventos.UI.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar lo publicado desde la etapa anterior
COPY --from=build /app/publish .

# Configurar puerto para Render
ENV ASPNETCORE_URLS=http://+:10000
ENV PORT=10000

# Ejecutar la app
ENTRYPOINT ["dotnet", "CentroEventos.UI.dll"]
