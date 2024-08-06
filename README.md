# Backend for Panteon Admin Panel
This is the backend for my admin panel project. 

Technologies used:
- .NET 8.0
- Authentication:
	- EF Core + JWT
	- MySQL
- Storing table data:
	- DynamoDB
- AutoMapper

The code is made to be easily extensible for any database setup, but if you want to use DynamoDB  you should have two tables named "BuildingTypes" and "Configurations".

Generate migrations and update your database for authentcation.

Ensure that you create appsettings.json in the PanteonAdminPanel.API directory and fill out the fields for your setup.

**appsettings.json**

    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "ConnectionStrings": {
        "AuthConnection": "Server=${DB_SERVER};Database=${DB_NAME};User=${DB_USER};Password=${DB_PASSWORD}"
      },
      "Jwt": {
        "Key": "${JWT_KEY}",
        "Issuer": "${JWT_ISSUER}",
        "Audience": "${JWT_AUDIENCE}"
      },
      "AWS": {
        "Profile": "default",
        "Region": "${AWS_REGION}",
        "AccessKey": "${AWS_ACCESS_KEY}",
        "SecretKey": "${AWS_SECRET_KEY}"
      }
    }
