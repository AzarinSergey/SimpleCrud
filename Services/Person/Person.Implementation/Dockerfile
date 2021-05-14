#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Services/Person/Person.Implementation/Person.Implementation.csproj", "Services/Person/Person.Implementation/"]
COPY ["Services/Person/Person.Contract/Person.Contract.csproj", "Services/Person/Person.Contract/"]
COPY [".core/Moedi.Cqrs.Messages/Moedi.Cqrs.Messages.csproj", ".core/Moedi.Cqrs.Messages/"]
COPY [".core/Core.Service.Host.Interfaces/Core.Service.Interfaces.csproj", ".core/Core.Service.Host.Interfaces/"]
COPY [".core/Core.Service.Host/Core.Service.Host.csproj", ".core/Core.Service.Host/"]
COPY [".core/Core.Service.Host.Convention/Core.Service.Host.Convention.csproj", ".core/Core.Service.Host.Convention/"]
COPY [".core/Core.Tool/Core.Tool.csproj", ".core/Core.Tool/"]
COPY ["Services/Person/Person.Domain/Person.Domain.csproj", "Services/Person/Person.Domain/"]
COPY ["Services/Person/Person.Model/Person.Model.csproj", "Services/Person/Person.Model/"]
COPY [".core/Moedi.Data.Ef/Moedi.Data.Ef.csproj", ".core/Moedi.Data.Ef/"]
COPY [".core/Moedi.Data.Core/Moedi.Data.Core.csproj", ".core/Moedi.Data.Core/"]
COPY [".core/Moedi.Cqrs/Moedi.Cqrs.csproj", ".core/Moedi.Cqrs/"]
RUN dotnet restore "Services/Person/Person.Implementation/Person.Implementation.csproj"
COPY . .
WORKDIR "/src/Services/Person/Person.Implementation"
RUN dotnet build "Person.Implementation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Person.Implementation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Person.Implementation.dll"]
