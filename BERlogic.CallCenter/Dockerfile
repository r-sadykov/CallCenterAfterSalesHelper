FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5001

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["BERlogic.CallCenter/BERlogic.CallCenter.csproj", "BERlogic.CallCenter/"]
COPY ["DinkToPdf/DinkToPdf.csproj", "DinkToPdf/"]
COPY ["JsonLocalization/JsonLocalization.csproj", "JsonLocalization/"]
RUN dotnet restore "../BERlogic.CallCenter/BERlogic.CallCenter.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "BERlogic.CallCenter.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BERlogic.CallCenter.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BERlogic.CallCenter.dll"]