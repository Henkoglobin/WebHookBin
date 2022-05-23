#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

ARG GIT_HASH=UNKNOWN

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG GIT_HASH
WORKDIR /src
COPY ["WebHookBin/WebHookBin.csproj", "WebHookBin/"]
RUN dotnet restore "WebHookBin/WebHookBin.csproj"
COPY . .
WORKDIR "/src/WebHookBin"
RUN dotnet build "WebHookBin.csproj" -c Release -o /app/build -p:SourceRevisionId=${GIT_HASH}

FROM build AS publish
ARG GIT_HASH
RUN dotnet publish "WebHookBin.csproj" -c Release -o /app/publish -p:SourceRevisionId=${GIT_HASH}

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebHookBin.dll"]