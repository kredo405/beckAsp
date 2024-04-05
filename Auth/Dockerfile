# Используем базовый образ для сборки приложения ASP.NET Core
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Копируем файлы проекта и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем остальные файлы проекта и собираем проект
COPY . ./
RUN dotnet publish -c Release -o out

# Создаем образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Auth.dll"]