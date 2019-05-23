# CachingWorkshop
## Benodigdheden
1. .NET Core Versie 3.0 Preview 5
2. Microsoft Visual Studio 2019 Preview
3. Activeer azure acount

# Opzetten van Azure/Reddis
Wij gebruiken hier niet onze HAN-account om dat die al geactiveerd was. Als je HAN-account is verlopen, kan je deze stappen volgen met je persoonlijke email om een nieuw account te maken. Hier heb je dan wel minder te goed.
## Stap 1 - Ga naar Azure en klik op activate
([Ga naar Azure for Students](https://azure.microsoft.com/en-us/free/students/))
![Activate Azure](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/1ActivateAzureAccount.jpeg)

## Stap 2 - Log in met je persoonlijke email
![Login to your Azure](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/2LoginToAzureAccount.jpeg)

## Stap 3 - Dashboard page
![Dashboard page](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/3AzureDashboard.jpeg)

## Stap 4 - "Create a resource"
![Activate Azure](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/4CreateAzureCache.jpeg)

## Stap 5 - Maak nieuwe Radis Cache
![Activate Azure](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/5CreateTheCache.jpeg)

#### LET OP! Reddis kost krediet. Ga zorgvuldig om met welke selectie je maakt. (Het goedkoopste pakket zou genoeg moeten zijn)

## Stap 6 - Wacht circa. 15 minuten
![Wait a while](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/6WaitAWhile.jpeg)

## Known issues
- Als het in Chrome niet werkt (400 - Bad request pagina), gebruik dan FireFox.

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



# Bronnen
- https://stackexchange.github.io/StackExchange.Redis/
- https://github.com/microsoftarchive/redis/releases
-  (["Azure Cache for Redis" opstart tutorial](https://docs.microsoft.com/en-us/azure/azure-cache-for-redis/cache-web-app-howto))
