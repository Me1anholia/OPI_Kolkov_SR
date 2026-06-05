FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копируем файл проекта и восстанавливаем зависимости
COPY ["Lab-3 OPI.csproj", "./"]
RUN dotnet restore "./Lab-3 OPI.csproj"

# Копируем весь остальной код
COPY . .
RUN dotnet publish "Lab-3 OPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Копируем собранный проект из первого этапа
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Lab-3 OPI.dll"]