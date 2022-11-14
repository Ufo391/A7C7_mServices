#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MessageBroker/MessageBroker.csproj", "MessageBroker/"]
RUN dotnet restore "MessageBroker/MessageBroker.csproj"
COPY . .
WORKDIR "/src/MessageBroker"
RUN dotnet build "MessageBroker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageBroker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageBroker.dll"]