FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

RUN dotnet tool install --global dotnet-ef --version 2.2.0
RUN dotnet ef database update --context UserStoreDbContext
RUN dotnet ef database update --ToDoDbContext
RUN dotnet ef database update --context TokenStoreDbContext

EXPOSE 5432

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/out .
CMD ["dotnet", "ToDoListApi.dll"]