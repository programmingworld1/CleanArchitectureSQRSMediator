# Copilot Instructions

Ensure functions have docstrings explaining parameters and return types.
Ensure that for each method you create you create a unit test.
Ensure that for each public method you create an integration test.

Use this for all functions, including private ones. This helps other developers understand the purpose of the function and how to use it correctly.


## Architecture
- Use Clean Architecture principles
- Domain layer must not depend on Infrastructure
- Use MediatR for commands and queries

## Coding standards
- Use .NET 8
- Use minimal APIs
- Prefer Mapster over AutoMapper
- Use Result pattern for errors

## Naming
- Commands end with Command
- Queries end with Query
- Handlers end with Handler


📂 Waarom staat het in .github/workflows dit bestand?
Dat is eigenlijk een beetje verwarrend.
.github/workflows is normaal voor GitHub Actions.
Maar Copilot kijkt in de .github map voor repo-specifieke instructies.
Het is dus geen pipeline.
Er zit geen automatische scanning op de hele repo, dus als je het bestand ergens anders neerzet, zal Copilot het niet vinden.
En het moet ook echt copilot-instructions.md heten, want dat is de naam die Copilot verwacht.