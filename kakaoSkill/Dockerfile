##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build
#WORKDIR /src
#COPY ["kakaoSkill/kakaoSkill.csproj", "kakaoSkill/"]
#RUN dotnet restore "kakaoSkill/kakaoSkill.csproj"
#COPY . .
#WORKDIR "/src/kakaoSkill"
#RUN dotnet build "kakaoSkill.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "kakaoSkill.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "kakaoSkill.dll"]
FROM microsoft/dotnet:2.1-aspnetcore-runtime
 
COPY . /app
WORKDIR /app
 
ENV ASPNETCORE_URLS http://*:9827
EXPOSE 9827
 
ENTRYPOINT ["dotnet", "kakaoSkill.dll"]