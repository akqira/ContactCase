# Contact management case
A simple demo of contact manager app using layer architecture, Dependency Injection, Unit testing...

## Technical information
* dotnet webapi core 3.1
* ef core
* swagger documentation
* xUnit 
* simple layered architecture to show available future possibilites
* github

## Database
You can use SqlServer or in Memory database by specifiying the correct connection string in appsettings file
### example In Memory db
{
  "ConnectionStrings": {
    "DefaultConnection": "Data source = CoreReact.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
### example sql server database
{
  "ConnectionStrings": {
    "ContactMgrConnectionString": "Server=localhost;Database=contactDB;Persist Security Info=False;User ID=SA;Password=P@ssword1;MultipleActiveResultSets=False;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}

## Swagger documentation
If running on localhost please go to following page to find swagger documentation after running the solution
https://localhost:5001/swagger/index.html





