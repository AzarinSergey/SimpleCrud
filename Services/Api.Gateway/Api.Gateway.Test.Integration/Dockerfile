FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
#ENTRYPOINT ["dotnet", "test"]
#ENTRYPOINT ["tail", "-f", "/dev/null"]