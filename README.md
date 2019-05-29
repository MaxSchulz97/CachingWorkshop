# CachingWorkshop
Mocht er onverwachts iets niet lukken tijdens het volgen van de workshop, dan kan het uitegewerkte project worden gedownload van de volgende link https://github.com/MaxSchulz97/CachingWorkshop.git.

## Benodigdheden
1. .NET Core Versie 3.0 Preview 5
2. Microsoft Visual Studio 2019 Preview
3. Activeer azure acount

# Opzetten van Azure/Reddis
Wij gebruiken hier niet onze HAN-account om dat die al geactiveerd was. Als je HAN-account is verlopen, kan je deze stappen volgen met je persoonlijke email om een nieuw account te maken. Hier heb je dan wel minder krediet.

## Stap 1 - Ga naar Azure en klik op activate
Mocht je na het klikken op de groene knop "Activate Now" een pagina met de tekst "400 - Bad request" krijgen, kijk dan onder het kopje "Known Issues" in deze README

[Ga naar Azure for Students (link van Marcel)](https://azure.microsoft.com/nl-nl/free/)

[Ga naar Azure for Students](https://azure.microsoft.com/en-us/free/students/)
![Activate Azure](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/1ActivateAzureAccount.jpeg)

## Stap 2 - Log in met je persoonlijke email
![Login to your Azure](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/2LoginToAzureAccount.jpeg)

## Stap 3 - Dashboard page
Klik op deze pagina links bovenin op "Create A Resource"

![Dashboard page](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/3AzureDashboard.jpeg)

## Stap 4 - "Creating a resource"
Klik op "Databases" en vervolgens op "Azure Cache for Redis"

![Activate Azure](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/4CreateAzureCache.jpeg)

## Stap 5 - Maak nieuwe Radis Cache
Vul de verschillende gegevens in voor het maken van de Cache

![Activate Azure](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/5CreateTheCache.jpeg)

#### LET OP! Reddis kost krediet. Ga zorgvuldig om met welke selectie je maakt. (Het goedkoopste pakket zou genoeg moeten zijn)

## Stap 6 - Wacht circa. 15 minuten
Haal een bakie koffie en wacht een klein kwartiertje!
![Wait a while](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/6WaitAWhile.jpeg)

## Known issues
- Als het in Chrome niet werkt (400 - Bad request pagina), gebruik dan FireFox.

# Opzetten van onze Database
Bij het gebruik maken van de database zijn er twee mogelijkheden. Je kan je eigen AirBnB database gebruiken of je kan een nieuwe database gebruiken. Voor deze demo hebben wij een .bak bestand gemaakt met een tabel, deze is te vinden in het mapje Database. Plaats dit bak bestandje op je C schijf zodat hij makkelijk terug te vinden is.

## Stap 1 - Open Microsoft SQL Server Management Studio
Open je Microsoft SQL Server Management Studio

## Stap 2 - Restore database
Klik met je rechter muisknop op de folder "Databases" en selecteer "Restore Database...".

## Stap 3 - Selecteer .bak bestand
Importeer net als bij de AirBnB database (gedaan aan het begin van deze course) ook het .bak bestand uit deze repo.

## Stap 4 - Klik op OK
Na het selecteren van het correcte bestand klik op "OK". Er is nu als het goed is een database aangemaakt.

# Opzetten van onze applicatie
## Stap 1 - Maak een nieuw project aan
1. Open Visual Studio 2019 en maak een nieuw MVC ASP.Net Core project aan.
2. Geef je project een mooie naam (Wij gebruiken “CachingMetReddis”) en klik op create.
3. Zorg dat je Model-View-Controller hebt geselecteerd en klik op create.

## Stap 2 - Installeer de correcte packages
Open de "Package Manager Console" en voer de volgende commands uit om de benodigde packages te installeren.

### Packages
Command om alle packages te installeren
```
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.SqlServer.Design
Install-Package Newtonsoft.Json

Install-Package StackExchange.Redis
```

### Scaffolding
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
Verwijder in je DbContext file de volgende functie volledig.
```
OnConfiguring(DbContextOptionsBuilder optionsBuilder)
``` 

# Implementeren van Redis
## Stap 1 - Maak een folder "Services" aan
Maak in je solution een folder "Services". Maak binnen deze folder een class genaamd "CacheService.cs".

## Stap 2 - Edit de CacheService.cs
Plak de volgende code in de class
```
private static ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(<INSERT_REDIS_ACCES_KEY>);
private static IDatabase database = connectionMultiplexer.GetDatabase();

public static IDatabase GetInstance()
{
   return database;
}
```

## Stap 3 - Navigeer naar je Acces-Key
In Azure navigeer naar de volgende pagina "All Services" --> "Acces Keys" --> "Primary connection string (StackExchange.Redis)", kopieer deze key.

![Copy the correct key](https://github.com/MaxSchulz97/CachingWorkshop/blob/master/Screenshots/b21671e8-ebf8-4489-8aea-4cd790ab5074.jpg)

## Stap 4 - Plak de key op de correcte plek in je code
Plak de gekopieerde Key in de bij de stap 2 gemaakte code, op de plek van <INSERT_REDIS_ACCES_KEY>.

## Stap 5 - Navigeer naar de HomeController.cs
Plak de volgende code boven "public IActionResult Index()"
```
private readonly AirBNBRedisContext _airBNBRedisContext;

public HomeController(AirBNBRedisContext airBNBRedisContext)
{
      _airBNBRedisContext = airBNBRedisContext;
}

IEnumerable<SummaryListings> summaryListings = null;

if (CacheService.GetInstance().KeyExists("SummaryListings"))
{
     summaryListings = JsonConvert.DeserializeObject<IEnumerable<SummaryListings>>(CacheService.GetInstance().StringGet("SummaryListings"));
}
else
{
     // Add to cache
     summaryListings = _airBNBRedisContext.SummaryListings.ToList();
     CacheService.GetInstance().StringSet("SummaryListings", JsonConvert.SerializeObject(summaryListings), TimeSpan.FromDays(1));
}

return Ok(summaryListings);
```

## Stap 6 - Run het project
Als nu het project wordt opgestart zal de eerste keer de data uit je lokale database gehaald worden. Hierbij wordt in het bovenstaande codevoorbeeld dus de "else" gepakt. Hierdoor wordt de data in je Azure Cache opgeslagen. De volgende keer als de pagina wordt geladen zal in bovenstaande de code dus het "if block" worden gebruikt.

Gefeliciteerd je hebt nu een werkende cache met Azure!

# Bronnen
- https://stackexchange.github.io/StackExchange.Redis/
- https://github.com/microsoftarchive/redis/releases
-  (["Azure Cache for Redis" opstart tutorial](https://docs.microsoft.com/en-us/azure/azure-cache-for-redis/cache-web-app-howto))
-  ([Reddis playground](http://try.redis.io))
