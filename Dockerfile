FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /app

COPY src/Users.Api/Users.Api.csproj                          ./src/Users.Api/Users.Api.csproj 
COPY src/Users.Application/Users.Application.csproj          ./src/Users.Application/Users.Application.csproj
COPY src/Users.Domain/Users.Domain.csproj                    ./src/Users.Domain/Users.Domain.csproj
COPY src/Users.HttpModels/Users.HttpModels.csproj            ./src/Users.HttpModels/Users.HttpModels.csproj
COPY src/Users.Infrastructure/Users.Infrastructure.csproj    ./src/Users.Infrastructure/Users.Infrastructure.csproj
COPY src/Users.DataLayer/Users.DataLayer.csproj              ./src/Users.DataLayer/Users.DataLayer.csproj

RUN dotnet restore ./src/Users.Api/Users.Api.csproj

COPY . ./
RUN dotnet publish ./src/Users.Api/Users.Api.csproj  -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

EXPOSE 80
EXPOSE 443

COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Users.Api.dll"]