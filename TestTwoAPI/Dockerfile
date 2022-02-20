#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestTwoAPI/TestTwoAPI.csproj", "TestTwoAPI/"]
RUN dotnet restore "TestTwoAPI/TestTwoAPI.csproj"
COPY . .
WORKDIR "/src/TestTwoAPI"
RUN dotnet build "TestTwoAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTwoAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTwoAPI.dll"]