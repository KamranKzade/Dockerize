#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY . .
RUN dotnet restore 
RUN	dotnet publish AspNetCoreMVC.csproj -c Release -o out
WORKDIR out
ENV ASPNETCORE_URLS="http://*:4500"
ENTRYPOINT ["dotnet", "AspNetCoreMVC.dll"]