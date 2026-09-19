FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["SistemaReserva.API/SistemaReserva.API.csproj","SistemaReserva.API/"]
COPY ["SistemaReserva.Application/SistemaReserva.Application.csproj","SistemaReserva.Application/"]
COPY ["SistemaReserva.Domain/SistemaReserva.Domain.csproj","SistemaReserva.Domain/"]
COPY ["SistemaReserva.Infrastructure/SistemaReserva.Infrastructure.csproj","SistemaReserva.Infrastructure/"]
COPY ["SistemaReserva.InfraIoC/SistemaReserva.InfraIoC.csproj","SistemaReserva.InfraIoC/"]

RUN dotnet restore "SistemaReserva.API/SistemaReserva.API.csproj"

COPY . .

WORKDIR "/src/SistemaReserva.API"

RUN dotnet build "SistemaReserva.API.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "SistemaReserva.API.csproj" \
	-c Release \
	-o /app/publish \
	/p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet","SistemaReserva.API.dll"]