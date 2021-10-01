#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Services/Api.Gateway/Api.Gateway.csproj", "Services/Api.Gateway/"]
COPY ["Services/Projection/Projection.Implementation.Monolithic/Projection.Implementation.Monolithic.csproj", "Services/Projection/Projection.Implementation.Monolithic/"]
COPY ["Services/Projection/Projection.Domain/Projection.Domain.csproj", "Services/Projection/Projection.Domain/"]
COPY [".core/Moedi.Data.Ef/Moedi.Data.Ef.csproj", ".core/Moedi.Data.Ef/"]
COPY [".core/Moedi.Data.Core/Moedi.Data.Core.csproj", ".core/Moedi.Data.Core/"]
COPY [".cmn/Cmn.Models/Cmn.Models.csproj", ".cmn/Cmn.Models/"]
COPY [".core/Moedi.Cqrs/Moedi.Cqrs.csproj", ".core/Moedi.Cqrs/"]
COPY [".core/Moedi.Cqrs.Messages/Moedi.Cqrs.Messages.csproj", ".core/Moedi.Cqrs.Messages/"]
COPY ["Services/Projection/Projection.Contract/Projection.Contract.csproj", "Services/Projection/Projection.Contract/"]
COPY [".core/Core.Service.Host/Core.Service.Host.csproj", ".core/Core.Service.Host/"]
COPY [".core/Core.Service.Host.Convention/Core.Service.Host.Convention.csproj", ".core/Core.Service.Host.Convention/"]
COPY [".core/Core.Tool/Core.Tool.csproj", ".core/Core.Tool/"]
COPY [".core/Core.Service.Host.Interfaces/Core.Service.Interfaces.csproj", ".core/Core.Service.Host.Interfaces/"]
COPY ["Services/Gis/Gis.Contract/Gis.Contract.csproj", "Services/Gis/Gis.Contract/"]
COPY [".core/Core.Service.Host.Client/Core.Service.Host.Client.csproj", ".core/Core.Service.Host.Client/"]
RUN dotnet restore "Services/Api.Gateway/Api.Gateway.csproj"
COPY . .
WORKDIR "/src/Services/Api.Gateway"
RUN dotnet build "Api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Gateway.dll"]
