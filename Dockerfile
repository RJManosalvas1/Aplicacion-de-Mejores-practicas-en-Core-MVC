# Dockerfile actualizado a .NET 8 (LTS) para despliegue en Render.
# El SDK 3.1 original esta fuera de soporte; se migro el TargetFramework
# del .csproj a net8.0 (ver BestPractices/Best Practices.csproj).

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BestPractices/Best Practices.csproj", "BestPractices/"]
RUN dotnet restore "BestPractices/Best Practices.csproj"
COPY . .
WORKDIR "/src/BestPractices"
RUN dotnet build "Best Practices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Best Practices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Render inyecta la variable de entorno PORT en tiempo de ejecucion.
# Kestrel debe escuchar en ese puerto y en HTTP (Render termina el TLS
# en su propio borde, no es necesario HTTPS dentro del contenedor).
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
ENTRYPOINT ["sh", "-c", "dotnet 'Best Practices.dll' --urls http://0.0.0.0:${PORT:-8080}"]
