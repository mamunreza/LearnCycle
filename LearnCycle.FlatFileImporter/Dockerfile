#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["LearnCycle.FlatFileImporter/LearnCycle.FlatFileImporter.csproj", "LearnCycle.FlatFileImporter/"]
#RUN dotnet restore "LearnCycle.FlatFileImporter/LearnCycle.FlatFileImporter.csproj"
#COPY . .
#WORKDIR "/src/LearnCycle.FlatFileImporter"
#RUN dotnet build "LearnCycle.FlatFileImporter.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "LearnCycle.FlatFileImporter.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "LearnCycle.FlatFileImporter.dll"]


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS lcbuild
WORKDIR /src
COPY . .

RUN dotnet restore LearnCycle.FlatFileImporter \
    && dotnet build LearnCycle.FlatFileImporter --configuration Release \
    && dotnet publish LearnCycle.FlatFileImporter --configuration Release --self-contained=false --runtime linux-x64 --output output

#Run
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /app
COPY --from=lcbuild /src/output/ .
ENTRYPOINT ["dotnet", "LearnCycle.FlatFileImporter.dll"]

