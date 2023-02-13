FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY /todolist-aspnetcore ./todolist-aspnetcore

EXPOSE 5001

RUN dotnet publish todolist-aspnetcore/todolist-aspnetcore.csproj -c Release -o ./out

WORKDIR /app/out

ENTRYPOINT ["dotnet", "todolist-aspnetcore.dll"]