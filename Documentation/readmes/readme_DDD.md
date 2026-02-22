DDD = the structure and language of the code should match that of the business domain.

Core Principles:
- its about creating a language shared by both the business and the developers. This language should be used in the code and in the conversations between the developers and the business.
Ubiquitous language means that everyone involved in the project uses the same terms/language. In dutch it means "alomtegenwoordige taal = is overal aanwezig". Names should not be technical, but business-oriented

Developers and domain experts use the same words:
Order
Customer
Invoice

These words appear:
In meetings
In documentation
In code

So when someone says:
“An Order cannot be paid if it’s cancelled”
You can almost paste that into code.
- Business logic lives with the business concept: the domain model should be the only place where business logic is implemented instead of being scattered across the application.
- Protecting invariants: All changes to the domain model should go through the aggregate root, which ensures that the business rules and invariants are enforced. This helps to maintain data consistency across the system.
- Encapsulation: The domain model should encapsulate the business logic and data, and should not expose its internal state to the outside world. This helps to protect the integrity of the domain model and prevents external code from modifying it in ways that could violate business rules or invariants.
- The domain model drives: Code structure, Class design, Method names, Boundaries. You don’t design tables first. You design concepts first.
- Focus domain: The domain (business problem) is the most important part of the system.Not frameworks.Not databases.Not UI. Design starts from business understanding, not technical architecture.
- Rich Domain Model (Not Anemic): Domain objects contain Data, Behavior, Rules. Not just properties with getters/setters.

Typical symptoms in non-DDD systems:
- Business rules scattered across controllers, services, and repositories
- Nobody knows where logic lives
- “Just add an if statement” becomes dangerous: (because there could be like 5 places where this if statement needs to be added, and if you forget one, it could break the system)
- Changing one rule breaks three others: because rules are scattered across the system, changing one rule could have unintended consequences on other parts of the system that rely on that rule. Because you cant see the link. But placing it in the domain you will see the link.
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
// Are these the SAME artist?
var artist1 = new Artist { Id = 1, Name = "Taylor Swift" };
var artist2 = new Artist { Id = 2, Name = "Taylor Swift" };
// NO! Different people, same name



Value Objects
- Biggest advantage: We can enforce business rules on value objects and be sure that they are always valid. For example, we can enforce that a money value object always has a positive amount and a valid currency.
- Use records in C# 9.0 to create value objects, because they provide built-in support for immutability and value-based equality.
- Even if there is only 1 value (string email) you can still make it a value object, because you can enforce business rules on it (e.g., valid email format).
- If all values are the same, then the value objects are considered equal. With entities, even if all attributes are the same, they are still different entities because they have different identities.
- Immutable, we cannot change them after they are created.
Why is 6 always 6? It's because 6's identity is determined by what it represents, namely the state of having six of something. 
You can't change that 6 represents that. Now, this is the fundamental concept of Value Objects. 
Their value is determined by their state. An Entity, however, is not determined by its state. 
A Customer can change their last name, or their address and still be the same Customer. 
This is why Value Objects should be immutable. Their state determines their identity; 
if their states changes, their identity should change.
- They are defined by their attributes, not by an identity. 
- For example, a money value object may have attributes like amount and currency, but it does not have an identity that distinguishes it from other money value objects with the same attributes.
Or for example, a date value object may have attributes like day, month, and year, but it does not have an identity that distinguishes it from other date value objects with the same attributes.
// Is this the SAME email?
var email1 = new Email("john@example.com");
var email2 = new Email("john@example.com");
// YES! They're identical. We don't care about "which instance"
- It doesnt have it's own table in the database, it is stored as part of the entity that owns it. For example, a money value object may be stored as part of an order entity (as a column), rather than having its own table in the database.
public record Money(decimal Amount, string Currency);
class Order
{
    public Guid Id { get; }
    public Money TotalPrice { get; private set; }
}
In the DB:
Orders
------------------------
Id
TotalPrice_Amount
TotalPrice_Currency

Why call it value object:
// Als je zegt: "Email is een Value Object"
public record Email { ... }
// Dan weet iedereen:
// ❌ Geen Id property toevoegen
// ❌ Geen DbSet<Email> maken
// ❌ Geen EmailRepository maken
// ❌ Geen setters toevoegen
// ✅ Validatie in Create() method
// ✅ Immutable houden
// ✅ Embedded in entity opslaan

// Als je zegt: "Email is een class"
public class Email { ... }
// Dan denkt iemand misschien:
// 🤔 "Moet ik hier een Id aan toevoegen?"
// 🤔 "Moet ik dit in een aparte tabel opslaan?"
// 🤔 "Moet ik een EmailRepository maken?"
// 🤔 "Kan ik hier setters aan toevoegen?"

Criterium	        Vraag	                Voorbeeld
1. Geen Identity	Heeft het een ID nodig?	❌ Email heeft geen ID
2. Value Equality	Zijn twee objecten met dezelfde waarde hetzelfde?	✅ "10 EUR" = "10 EUR"
3. Immutable	Kan het veranderen na creatie?	❌ No, create new instance
4. Self-Validating	Enforced het business rules?	✅ Money > 0, valid currency
5. No Lifecycle	Wordt het apart opgeslagen/getrackt?	❌ No separate table
6. Domain Concept	Is het een business concept?	✅ "Money", "Address", "Email"

Summary: An immutable record, with no ID (is defined by its value rather than its identity), that needs to enforce business rules, is a Value Object.

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