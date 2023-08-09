# Birinci image publishi sdk ile edirik
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
COPY *.csproj .
RUN dotnet restore 
COPY . . 
RUN	dotnet publish AspNetCoreMVC.csproj -c Release -o out


# Ikinci image --> Runtime uzerinden ayaga qaldiririq, punlish olunmus datani alib 
# runtime-da isledirik

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS="http://*:4500"
ENTRYPOINT ["dotnet", "AspNetCoreMVC.dll"]
