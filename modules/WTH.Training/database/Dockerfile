FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build
COPY . .

WORKDIR /templates/service/host/AuthServerHost
RUN dotnet restore
RUN dotnet ef migrations script -i -o migrations-AuthServerHost.sql

WORKDIR /templates/service/host/WTH.Training.Host
RUN dotnet restore
RUN dotnet ef migrations script -i -o migrations-Training.sql

FROM mcr.microsoft.com/mssql-tools AS final
WORKDIR /src
COPY --from=build /templates/service/host/AuthServerHost/migrations-AuthServerHost.sql migrations-AuthServerHost.sql
COPY --from=build /templates/service/host/WTH.Training.Host/migrations-Training.sql migrations-Training.sql
COPY --from=build /templates/service/database/entrypoint.sh .
RUN /bin/bash -c "sed -i $'s/\r$//' entrypoint.sh"
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]