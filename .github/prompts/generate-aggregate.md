## Section A — Ask me these questions (one by one, guid me thorugh it)

1) What is the **Aggregate Root name**?
2) What is the **Identity** (type + name)? (e.g., `OrderId (Guid)` or `OrderId (strongly-typed)`).
3) List the **properties** of the aggregate (name + type + required/optional).
4) Are there **child entities** inside the aggregate?  
   - If yes: list each entity name + identity + properties.
5) Are there **Value Objects**?  
   - If yes: list each VO name + fields + validation rules.
6) What are the **invariants/business rules** that must always hold? (bullets)
7) What are the **commands/behaviors** on the aggregate?  
   - e.g., `Create`, `AddLine`, `RemoveLine`, `Confirm`, `Cancel`
8) Which operations should be **idempotent**? (if any)
9) Any **concurrency** requirements?  
   - e.g., optimistic concurrency, version field, “cannot change after Confirmed”
10) Should we emit **Domain Events**?  
   - If yes: list event names + when they fire + payload fields.
11) Any **serialization/persistence considerations**?  
   - e.g., EF Core owned types, backing fields, private setters (but keep domain clean)

---

## Section B — Generate these deliverables

After you collected answers, generate:

### 1) Domain layer code (no Infrastructure)
- Aggregate Root class with:
  - identity
  - state
  - behaviors/methods enforcing invariants
  - controlled mutation (private setters/backing fields)
- Child entity classes (if any)
- Value Objects (if any) with validation and equality
- Domain Events (if any)

### 2) Optional supporting types
- Strongly-typed IDs (only if I asked for it)
- Enums or small supporting types (if needed)

### 3) Notes
- Short note on key invariants and how the code enforces them
- If something is ambiguous, list assumptions explicitly

### Output rules
- Provide production-ready C# code blocks per type
- No EF Core attributes in Domain
- Keep it concise but complete
- No controller/application code unless I explicitly ask