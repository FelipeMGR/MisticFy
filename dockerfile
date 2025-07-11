# 1) imagem SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# 2) copia sln e csproj para cache de restore
COPY MisticFy.sln ./
COPY MisticFy.csproj ./

# 3) restaura pacotes
RUN dotnet restore "MisticFy.csproj"

# 4) copia **todo** o restante do código (inclui Program.cs, src/, etc)
COPY . ./

# 5) configura porta e URL
ENV ASPNETCORE_URLS="http://+:5000"
EXPOSE 5000

# 6) entrypoint
ENTRYPOINT ["dotnet", "run", "--no-launch-profile", "--project", "MisticFy.csproj"]
