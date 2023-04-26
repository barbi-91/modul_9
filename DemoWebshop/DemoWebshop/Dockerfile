FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DemoWebshop.csproj", "DemoWebshop/"]
RUN dotnet restore "DemoWebshop/DemoWebshop.csproj"


WORKDIR "/src/DemoWebshop"
COPY . .

RUN dotnet build "DemoWebshop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoWebshop.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoWebshop.dll"]