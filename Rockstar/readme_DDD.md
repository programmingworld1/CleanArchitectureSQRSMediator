DDD = the structure and language of the code should match that of the business domain.

Core Principles:
- its about creating a language shared by both the business and the developers. This language should be used in the code and in the conversations between the developers and the business.
Ubiquitous language means that everyone involved in the project uses the same terms/language. In dutch it means "alomtegenwoordige taal = is overal aanwezig"
- Centralized business rules: the domain model should be the only place where business logic is implemented instead of being scattered across the application.
- Protecting invariants: All changes to the domain model should go through the aggregate root, which ensures that the business rules and invariants are enforced. This helps to maintain data consistency across the system.
- Encapsulation: The domain model should encapsulate the business logic and data, and should not expose its internal state to the outside world. This helps to protect the integrity of the domain model and prevents external code from modifying it in ways that could violate business rules or invariants.


Typical symptoms in non-DDD systems:
- Business rules scattered across controllers, services, and repositories
- Nobody knows where logic lives
- Changing one rule breaks three others
- “Just add an if statement” becomes dangerous
- New developers are afraid to touch code


Domain Events
- A domain event is something that happens in the domain that is of interest to the business. For example, a customer placing an order is a domain event.
- They are immutable, meaning that once they are created, they cannot be changed. This is because they represent something that has already happened in the past.

Entities
- Have an identity that distinguishes them from other entities, even if they have the same attributes. 
For example, two different customers may have the same name and address, but they are still different entities because they have different identities (usually a diffretent ID value).
- Their attributes can be modified unlike value objects. For example, a customer's name and address may change over time, but the customer entity itself remains the same because it has the same identity.
- These changes to the attributes publish domain events that other parts of the system can react to. 
For example, if a customer's address changes, a domain event may be published to notify other parts of the system that need to know about this change (e.g., shipping department, billing department, etc.).
- Each entity has it's own history of domain events that have occurred to it. For example, a customer entity may have a history of domain events such as "customer created", "customer updated", "customer deleted", etc.

Value Objects
- Immutable, we cannot change them after they are created.
- They are defined by their attributes, not by an identity. 
- For example, a money value object may have attributes like amount and currency, but it does not have an identity that distinguishes it from other money value objects with the same attributes.
Or for example, a date value object may have attributes like day, month, and year, but it does not have an identity that distinguishes it from other date value objects with the same attributes.

Aggregate Roots / Aggregate
- An Aggregate is a cluster of domain objects (entities and value objects) that is treated as a single unit for consistency.
- An aggregate root is an entity that serves as the entry point to aggregates. All calls from outside the >aggregate< should go through the >aggregate root<>.
- The aggregate root ensures the correct state of the aggregate by enforcing business rules and invariants. 
For example, if we have an order aggregate that consists of an order entity and several order line item entities, the order entity would be the aggregate root. 
All interactions with the order line items would go through the order entity, 
which would enforce any business rules or invariants related to the order as a whole (e.g., ensuring that the total amount of the order is correct based on the line items).

Example:

Album (Aggregate Root)
 ├── Track (Entity)
 └── ReleaseInfo (Value Object)

Rules:
- You cannot add a Track without going through Album
- External code cannot directly modify Track
- All validation happens inside Album

Repositories


When NOT to Use DDD
DDD is overkill for:
•	Simple CRUD applications (just reading/writing data)
•	Small projects with simple business rules
•	Projects with no complex domain logic
•	Prototypes and MVPs
Use DDD when:
•	Complex business rules exist
•	Multiple teams need shared understanding
•	Domain logic is scattered and hard to maintain
•	The domain is core to business value



Reddit:
its a set of tools you can use to break down large, complex domains. that's all it is. the tools are things like bounded contexts, aggregate roots, value objects, acl's, etc.
at the end of the day its about putting an extreme emphasis on your domain models and the relationships between them. no more anemic models and service layers. you're domain should be rich objects and contain behavior.

Note: 
Language Lives in Code, Mostly in:
- Class names
- Method names
- Properties
- Domain exceptions