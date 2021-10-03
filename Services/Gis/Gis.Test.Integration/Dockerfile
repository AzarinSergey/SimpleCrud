FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app

COPY ["Services/Gis/Gis.Test.Integration", "Services/Gis/Gis.Test.Integration/"]
COPY ["Services/Gis/Gis.Model", "Services/Gis/Gis.Model/"]
COPY ["Services/Gis/Gis.Domain", "Services/Gis/Gis.Domain/"]

COPY [".core/Moedi.Data.Ef", ".core/Moedi.Data.Ef/"]
COPY [".core/Moedi.Data.Core", ".core/Moedi.Data.Core/"]
COPY [".core/Moedi.Cqrs", ".core/Moedi.Cqrs/"]
COPY [".core/Moedi.Cqrs.Messages", ".core/Moedi.Cqrs.Messages/"]
COPY [".core/Core.Tool", ".core/Core.Tool/"]
COPY [".core/Core.Service.Host.Interfaces", ".core/Core.Service.Host.Interfaces/"]


RUN dotnet restore Services/Gis/Gis.Test.Integration/Gis.Test.Integration.csproj

#ENTRYPOINT ["tail", "-f", "/dev/null"]