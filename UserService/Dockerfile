# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY UserService/ UserService/
RUN dotnet restore UserService/UserService.csproj

# Copy the entire project and build it
COPY . ./
RUN dotnet publish UserService/UserService.csproj -c Release -o /app/publish

# Use the official .NET runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Copy the published app from the build image
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "UserService.dll"]