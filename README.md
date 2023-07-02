Implementando 2FA (Autenticação de Dois Fatores) em sua API .NET com GoogleAuthenticator
````
dotnet add package GoogleAuthenticator

````
JWT 
````
dotnet add package Microsoft.AspNetCore.Authentication
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6
dotnet add package Microsoft.IdentityModel.Tokens 
dotnet add package System.IdentityModel.Tokens.Jwt 

````
Para rodar o Dockerfile
docker buikd -t apitools
docker run --name apitools -d -t 8000:80 apitools


Migration
````
dotnet tool install --global dotnet-ef
dotnet ef --version
dotnet ef migrations add Stopword
dotnet ef migrations add InitialCreate
dotnet ef database update
````