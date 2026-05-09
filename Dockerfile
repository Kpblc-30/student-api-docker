# Стадия 1: Сборка приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем csproj файл и восстанавливаем зависимости
COPY StudentApi.csproj .
RUN dotnet restore

# Копируем все файлы и публикуем приложение
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Запуск приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Копируем опубликованное приложение из стадии сборки
COPY --from=build /app/publish .

# Открываем порт 8080 
EXPOSE 8080

# Запускаем приложение
ENTRYPOINT ["dotnet", "StudentApi.dll"]