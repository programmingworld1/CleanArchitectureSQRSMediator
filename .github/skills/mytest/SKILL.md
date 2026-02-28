---
name: mytest
description: Run tests and fix failures until green
---

Workflow:

1. Run `dotnet test`
2. Parse failures
3. Fix minimal necessary code
4. Run tests again
5. Stop when all tests pass

Never introduce new architecture changes.
Keep changes minimal.