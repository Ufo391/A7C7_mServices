#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoreApi/CoreApi.csproj", "CoreApi/"]
RUN dotnet restore "CoreApi/CoreApi.csproj"
COPY . .
WORKDIR "/src/CoreApi"
RUN dotnet build "CoreApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreApi.dll"]