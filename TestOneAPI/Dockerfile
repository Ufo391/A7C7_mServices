#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestOneAPI/TestOneAPI.csproj", "TestOneAPI/"]
RUN dotnet restore "TestOneAPI/TestOneAPI.csproj"
COPY . .
WORKDIR "/src/TestOneAPI"
RUN dotnet build "TestOneAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestOneAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestOneAPI.dll"]