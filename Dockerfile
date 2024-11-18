# Estágio de construção
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar o arquivo de solução e restaurar as dependências
COPY ApiTools.sln ./
COPY *.csproj ./
RUN dotnet restore ApiTools.sln

# Copiar o restante dos arquivos e construir
COPY . ./
RUN dotnet publish ApiTools.sln -c Release -o out

# Estágio de execução
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
VOLUME /app/data

ENTRYPOINT ["dotnet", "ApiTools.dll"]
