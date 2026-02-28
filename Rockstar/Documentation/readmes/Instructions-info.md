Je kunt op een aantal manieren interacteren met copilot:
- Custom agent
Omdat de rest “gewoon context” is, kan het in lange gesprekken minder prominent worden dan een agent-profiel (dat expliciet als agent-instructie wordt gebruikt).

In de YAML frontmatter kun je aangeven welke tools de agent mag gebruiken (bijv. terminal/workspace/MCP). Dat is handig om een review-agent alleen lees/annotatie te laten doen, en een test-agent wél dotnet test te laten draaien.
Met een losse .md in chat kun je dit vragen, maar je kunt het niet zo hard “afkaderen” in het agent-profiel.

Je hoeft niet steeds prompt bestanden te noemen. Agent selecteren = klaar.

Purpose: Define a persona or workflow orchestrator that can combine skills and instructions.
Use case: When you want a named, persistent “agent” that handles complex workflows end-to-end.
Behavior: Custom agents select skills, follow repo instructions, and manage task orchestration in a consistent way.
Rule of thumb: Use custom agents when you want a consistent helper that can handle multi-step workflows or complex tasks.


- Prompt file
Snel hergebruik van standaardvragen of opdrachten.

- copilot-instructions
Gebruik: Voor het instellen van globale instructies die Copilot altijd volgt.
Voordeel: Gedraagt zich als een “handleiding” voor alle interacties, werkt sessiebreed.

-Skill bestanden
Purpose: Encapsulate specific, reusable capabilities that agents can call on-demand.
Use case: Task-oriented, modular logic that can be shared across agents and repositories.
Behavior: Skills are invoked automatically by the agent if relevant. They are portable.
Rule of thumb: Use skills for task-specific logic that can be reused in multiple contexts.
When Copilot determines a skill is relevant to your task, it loads the instructions and follows them—including any resources you’ve included in the skill directory.
Until recently, you could get part of the way there with custom instructions and prompt files. They are both great, but they do not fully solve the same problem: packaging a repeatable, multi-step workflow with its own supporting assets.
Agent Skills is an open standard (see agentskills.io) that works with GitHub Copilot in VS Code, Copilot CLI, and the Copilot coding agent.
A Skill is an on-demand, reusable workflow for Copilot. A Skill lives in a folder, has a required SKILL.md, and can include supporting resources such as scripts, references, and templates.

At a high level, it is designed for:

Repeatable workflows you want to reuse across a team
Multi-step procedures that benefit from checklists and branching logic
Bundled assets such as scripts, templates, and short reference docs

The key idea is progressive loading:

Copilot first uses the Skill name and description for discovery.
If the request matches, it loads the Skill instructions.
It only loads extra resources when the Skill references them.
This makes Skills a good fit for DevOps because you can keep the default Copilot experience lean, then load a specialised runbook only when you need it.

- Of gewoon tekst toevoegen aan chat window

- Context (bestanden slepen en droppen in chat)



Omdat het “gewoon context” is, kan het in lange gesprekken minder prominent worden dan een agent-profiel (dat expliciet als agent-instructie wordt gebruikt).