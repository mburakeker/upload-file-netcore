#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UploadFileBackend.Api/UploadFileBackend.Api.csproj", "UploadFileBackend.Api/"]
COPY ["UploadFileBackend.Core/UploadFileBackend.Core.csproj", "UploadFileBackend.Core/"]
COPY ["UploadFileBackend.Models/UploadFileBackend.Models.csproj", "UploadFileBackend.Models/"]
RUN dotnet restore "UploadFileBackend.Api/UploadFileBackend.Api.csproj"
COPY . .
WORKDIR "/src/UploadFileBackend.Api"
RUN dotnet build "UploadFileBackend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UploadFileBackend.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UploadFileBackend.Api.dll"]