FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/SocialMediaDashboard.Web/SocialMediaDashboard.Web.csproj", "src/SocialMediaDashboard.Web/"]
COPY ["src/SocialMediaDashboard.Logic/SocialMediaDashboard.Logic.csproj", "src/SocialMediaDashboard.Logic/"]
COPY ["src/SocialMediaDashboard.Data/SocialMediaDashboard.Data.csproj", "src/SocialMediaDashboard.Data/"]
COPY ["src/SocialMediaDashboard.Domain/SocialMediaDashboard.Domain.csproj", "src/SocialMediaDashboard.Domain/"]
COPY ["src/SocialMediaDashboard.Common/SocialMediaDashboard.Common.csproj", "src/SocialMediaDashboard.Common/"]
RUN dotnet restore "src/SocialMediaDashboard.Web/SocialMediaDashboard.Web.csproj"
COPY . .
WORKDIR "/src/src/SocialMediaDashboard.Web"
RUN dotnet build "SocialMediaDashboard.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialMediaDashboard.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SocialMediaDashboard.Web.dll
