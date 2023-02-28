#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["jwt_auth/jwt_auth.csproj", "jwt_auth/"]
RUN dotnet restore "jwt_auth/jwt_auth.csproj"
COPY . .
WORKDIR "/src/jwt_auth"
RUN dotnet build "jwt_auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "jwt_auth.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jwt_auth.dll"]