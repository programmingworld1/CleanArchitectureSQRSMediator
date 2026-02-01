1) Dependency “besmetting”

Stel je hebt Infrastructure met EF Core + Azure Service Bus + Redis + Stripe SDK.
-iemand in persistence wil “even” een event publishen → pakt message bus code erbij
-Voor je het weet heb je kruisingen zoals:
-persistence → messaging
-messaging → persistence
-external → caching → persistence
Dan krijg je:
-circular dependencies (logisch, niet per se compile-time)
-onduidelijke grenzen
-“waarom faalt dit?” omdat alles aan alles hangt
Met losse projecten kun je dit simpel blokkeren:
-Infrastructure.Persistence mag niet naar Infrastructure.Messaging referencen (compile-time)
-Infrastructure.ExternalApis mag niet naar Infrastructure.Persistence referencen (compile-time)


2) Je wilt niet altijd álle infra mee in elke host
Bijvoorbeeld:
-API host: DB + external APIs + caching
-Worker host: messaging + DB
-CLI tool: alleen DB migrations

Als infra één project is, sleep je automatisch:
-messagebus libs mee in je API (onnodig)
-stripe sdk mee in je worker (onnodig)
-redis mee in je migration tool (onnodig)

3) Stel je wil een nuget package maken om het te delen: 
Als package A package B refereert, dan krijgt iedereen die A installeert óók B — of hij het wil of niet.
Als iemand alleen infra.persistance wil en ook service bus erbij krijgt:
-Zijn app kan crashen door iets dat hij niet gebruikt
-Configuratie wordt verplicht (zetten van connection string voor service bus), maar als je alleen persisent nodig hebt maar je krijgt service bus er ook bij, dan moet je het opeens configureren anders crash
-Build,memory overheat, kost	en
-Omdat extra dependencies automatisch meekomen, dwing je bij elke update of breaking change van één onderdeel ook andere packages te versioneren en upgraden, zelfs als ze functioneel niets met die wijziging te maken hebben.