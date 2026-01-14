# Rockstar

- This project is of type: ASP.NET CORE API

- Stap 0, open solution in VS.

- Step 1, start project: EF core is used. When you run the code, the database should
be automatically created if it does not exist yet, and all migrations should 
autimatically be applied. However you need to have a localhost (server name) SQL server running. If you face issues with connectivity, change the connection string in the appsettings-dev.json. Once it works, a database called "RockstarDB" should be created with the corresponding tables.

- Step 2, interact with project: When running the project (just press start in VS), OpenAPI page should open and show the different existing endpoints. That page can also be used to trigger those endpoints. Or use postman or any other tool you like. But I recommend to open solution -> Rockstart.http because in that file
all of the endpoint requests are already setup.

- Step3, Authentication/Authorization: The code uses authentication + authorization + API key.
    - API-Key: You need to provide an API key to the request header "X-Api-Key". In the appsettings-dev.json it shows the list of allowed API-Keys. (If you use Rockstar.http, take the key from the appsettings and assign it to the variable in Rockstar.http)
    - Register: After, you need to call the register endpoint (If you use Rockstar.http file, just add the key to the existing variable) with "teamrockstars.nl" at the end of the email and a password.
    - After calling the register endpoint, a Bearer token should be returned. Copy the bearer token and use it in your requests in the Authorization header (If you are using Rockstar.http, add it to the existing variable)
    - Note: the Bearer token is a JWT token, for which the code uses a secret instead of a private key to generate the token. This secret can be found in the appsettings-dev.json.

- Note: All of the secrets (api-key, JWT secret, DB connectivity string) is set in the appsettings-dev.json so that you dont have to do it by adding a secrets file. I only did this to make starting the application easier, normally I wouldnt do this because this is not something you want to commit to git because of security reasons, normally you would want to add it to the secrets file, but again, this is purely done to make starting the app easier. Even documenting all of the authorization/authentication I normally wouldnt do in a Readme for security reasons.

- The project is based on Clean Architecture + SQRS + Mediator pattern.