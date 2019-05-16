# CachingWorkshop
## Benodigdheden
1. .NET Core Versie 3.0 Preview 5
2. Microsoft Visual Studio 2019 Preview
3. [Redis server](https://github.com/microsoftarchive/redis/releases)

# Opzetten van onze applicatie
## Stap 1 - Maak een nieuw project aan
1. Open Visual Studio 2019 en maak een nieuw MVC ASP.Net Core project aan.
2. Geef je project een mooie naam (Wij gebruiken “CachingMetReddis”) en klik op create.
3. Niks aanpassen gewoon nog een keer op create klikken.

## Stap 2 - Installeer de correcte packages
Open de "Package Manager Console" en voer de volgende commands uit om de benodigde packages te installeren.

### Packages
Command om alle packages te installeren
```
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.SqlServer.Design

Install-Package StackExchange.Redis
```

Scaffolding
```
Scaffold-DbContext "Server=<INSERT_SERVER_NAME>;Database=<INSERT_DB_NAME>;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```

## Stap 3 - Connectionstring aanpassen
Navigeer naar "appsettings.json" en voeg de volgende code toe
```
"ConnectionStrings": {
   "DefaultConnection": "Server=<INSERT_SERVER_NAME>;Database=<INSERT_DB_NAME>;Trusted_Connection=True;"   
}
```

## Stap 4 - Startup.cs aanpassen
Navigeer naar Startup.cs en voeg in de methode ConfigureServices() het volgende toe.
```
//ConnectionString is now retreived from appsettings.json             
services.AddDbContext<<INSERT_DB_CONTEXT>>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
```

## Stap 5 - Verwijder de functie OnConfiguring()
Verwijder in je DbContext file 
```
OnConfiguring(DbContextOptionsBuilder optionsBuilder)
``` 

## Stap 6 - Voeg een contructor toe
```
//Constructor which allows configuration to be passed into the context by DI         
public AIRBNBContext(DbContextOptions<AIRBNBContext> options) : base(options) { } 
```

# Opzetten van Reddis
## Stap 1 - Downloaden van de Reddis server
Zorg dat je de Reddis server als zip hebt gedownload van de volgende link [Redis server](https://github.com/microsoftarchive/redis/releases)

## Stap 2 - Uitpakken van de zip
Zet de zip in een handige directory en pak de zip uit

## Stap 3 - Openen van Reddis
Run reddis-server.exe. Je server is nu opgestart.

# Reddis gebruiken in de code
## Het opslaan van een value in de Reddis Cache
``` 
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();
db.StringSet("<INSERT_OWN_KEY>", <INSERT_VALUE_TO_STORE>);
```

## Het ophalen van een value uit de Reddis Cache
```
string value = db.StringGet("INSERT_OWN_KEY");
```

# Bronnen
- https://stackexchange.github.io/StackExchange.Redis/
- https://github.com/microsoftarchive/redis/releases
