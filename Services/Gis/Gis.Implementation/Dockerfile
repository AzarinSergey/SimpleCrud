#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Services/Gis/Gis.Implementation/Gis.Implementation.csproj", "Services/Gis/Gis.Implementation/"]
COPY ["Services/Gis/Gis.Contract/Gis.Contract.csproj", "Services/Gis/Gis.Contract/"]
COPY [".core/Moedi.Cqrs.Messages/Moedi.Cqrs.Messages.csproj", ".core/Moedi.Cqrs.Messages/"]
COPY [".core/Core.Service.Host.Interfaces/Core.Service.Interfaces.csproj", ".core/Core.Service.Host.Interfaces/"]
COPY [".core/Core.Service.Host/Core.Service.Host.csproj", ".core/Core.Service.Host/"]
COPY [".core/Core.Service.Host.Convention/Core.Service.Host.Convention.csproj", ".core/Core.Service.Host.Convention/"]
COPY [".core/Core.Tool/Core.Tool.csproj", ".core/Core.Tool/"]
COPY ["Services/Gis/Gis.Domain/Gis.Domain.csproj", "Services/Gis/Gis.Domain/"]
COPY ["Services/Gis/Gis.Model/Gis.Model.csproj", "Services/Gis/Gis.Model/"]
COPY [".core/Moedi.Data.Ef/Moedi.Data.Ef.csproj", ".core/Moedi.Data.Ef/"]
COPY [".core/Moedi.Data.Core/Moedi.Data.Core.csproj", ".core/Moedi.Data.Core/"]
COPY [".core/Moedi.Cqrs/Moedi.Cqrs.csproj", ".core/Moedi.Cqrs/"]
RUN dotnet restore "Services/Gis/Gis.Implementation/Gis.Implementation.csproj"
COPY . .
WORKDIR "/src/Services/Gis/Gis.Implementation"
RUN dotnet build "Gis.Implementation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gis.Implementation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gis.Implementation.dll"]
