# Turn-Based RPG

This project is a small-scale turn-based RPG prototype built in Unity 6.
The goal is to show clean architecture in a gameplay-heavy feature set: battle flow, unit logic, unlock/progression, and persistence.
It intentionally focuses on readability, extendability and showing my knowlage over “quick scripting”.

## Tech Stack

- Unity 6000.2.14f1
- C# 
- DOTween (animations)
- TextMesh Pro (UI text)

## Features

### Turn-Based Battle System
Combat that changes player and enemy turns. State machine manages battle flow with smooth transitions between states.

### Character Selection
Select your team of 3 heroes before entering battle. Each character has unique stats and abilities.

### Progression System
- **XP & Leveling** - Earn experience from victories, level up to increase stats
- **Stat Scaling** - HP and Attack power scale with level (configurable %)
- **Character Unlock** - New heroes unlock as you win more battles

### Clean Architecture
- **MVC Pattern** - Clear separation between data, visuals, and logic
- **State Machine** - Predictable battle and unit state management
- **Command Pattern** - Extensible attack system
- **Event Bus** - Decoupled component communication
- **Service Locator** - Centralized dependency management

### Persistence
Game progress saved automatically using PlayerPrefs. Battle state preserved if app closes mid-battle.

### Configurable
All game balance settings managed via ScriptableObject - no code changes needed for tuning.


## How to Play

### Main Menu
1. View your unlocked heroes
2. See your heroes' details (XP, level, name etc.) by long pressing on the hero picture.
3. Select 3 heroes for your battle team
4. Press "Battle" when ready

### Battle
1. **Your Turn** - Tap on any of your heroes to attack
2. **Target** - Attack will hit an enemy (random or targeted based on attack type)
3. **Enemy Turn** - Wait for enemies to complete their attacks
4. **Repeat** - Continue until one side is defeated

### Victory
- Earn XP for surviving heroes
- Heroes level up when XP threshold is reached
- New characters unlock after certain number of battles

### Tips
- Higher level heroes deal more damage and have more HP
- Keep your heroes alive to earn XP rewards
- Unlock all 10 heroes by winning battles

---

## Architecture

### MVC Pattern

The project follows Model-View-Controller pattern for character management:

<img width="561" height="281" alt="Untitled Diagram drawio" src="https://github.com/user-attachments/assets/8b30e2f3-bd0e-499f-89b3-71ddd28eb880" />

**Examples:**
- `UnitBattleModel` - HP, attack power, level data
- `BattleUnitView` - Sprite, HP bar, animations
- `UnitBattleController` - Turn logic, damage handling, state management

---

### Service Locator

Services are registered at startup and accessed globally via `ServiceLocator`:

```csharp
// Registration (GameManager.cs)
ServiceLocator.Instance.Register<IHealthLogicService>(new HealthLogicService(...));
ServiceLocator.Instance.Register<IRewardService>(new ExpRewardService(...));
ServiceLocator.Instance.Lock(); // No more registrations allowed

// Usage (anywhere)
var healthService = ServiceLocator.Instance.Get<IHealthLogicService>();
int totalHp = healthService.GetTotalHealth(baseHp, level);
```

**All Services:**

| Service | Purpose |
|---------|---------|
| `IGameConfigService` | Game configuration (ScriptableObject) |
| `IDataProviderService` | Character data loading |
| `IHealthLogicService` | HP calculation per level |
| `IAttackPowerLogicService` | Attack power calculation |
| `ILevelUpLogicService` | Level-up logic |
| `IRewardService` | XP rewards |
| `ICharacterUnlockLogicService` | Character unlock rules |

All services are easy to test and extend.

---

### Event Bus and Event Layer

Decoupled communication between components using string-based events:

- EventNames.cs has all the event names as readable identifier
- IEvent.cs is an interface for Event argument classes to share data with event call
- EventLayer.cs is a class to use with other classes and makes it more readable
  - Fire, Subscribe, Unsubscribe methods
  - MonoBehaiourEventLayer.cs makes this usage to share with unity objects

---

## Battle System

### State Machine

The battle flow is managed by `BattleStateMachine` with cached states:
<img width="995" height="531" alt="Untitled Diagram (2)" src="https://github.com/user-attachments/assets/b0be75fd-73a8-473c-bdeb-10cfc49f0609" />


**States:**
- `PlayerTurnState` - Player can select and attack
- `PlayerActionInProgress` - Attack animation playing
- `EnemyTurnState` - Enemy AI attacks
- `EnemyActionInProgress` - Enemy attack animation
- `WinState` - Victory, rewards given
- `LoseState` - Defeat

### Unit States

Each state is mainly used for animation calling but it can be extanble in the future with other tasks. 

- `UnitTurnStartedState` - Unit's turn began
- `UnitTurnEndedState` - Unit's turn ended
- `UnitAttackingState` - Performing attack
- `UnitTakeDamageState` - Taking damage animation
- `UnitDiedState` - Death animation

---

## Combat System

### Command Pattern

Attacks are encapsulated as commands:
It is quite easy to extend this attack types with this structure. For example we can add, ranged attack etc by just adding one more command.
Also in this way, all of the characters can use all types of attack. 

```
ICommand
    └── AttackCommandBase
            ├── MeleeTargetedAttackCommand
            └── MeleeRandomAttackCommand
```

**Factory creates appropriate command:**

```csharp
AttackCommand = AttackCommandFactory.Create(
    view,
    attackType,      // MeleeTargeted or MeleeRandom
    targets,
    damage,
    onStarted,
    onEnded
);

AttackCommand.Execute();
```

**Attack Flow:**
1. Command created with all data in constructor
2. `Execute()` triggers animation
3. `ApplyDamage()` called on hit
4. Callbacks notify state machine

---

## Configuration

Game settings are managed via GameConfig ScriptableObject:

| Setting | Description |
|---------|-------------|
| `FirstUnlockedHeroes` | Starting character references |
| `LevelUpOnEachXp` | XP needed per level |
| `CharacterUnlockAfterBattle` | Battles needed to unlock new character |
| `AttackIncreasePercentOnEachLevel` | % attack increase per level |
| `HealthIncreasePercentOnEachLevel` | % HP increase per level |
| `RequiredHeroCountForBattle` | Heroes needed to start battle |
| `MaxHeroCount` | Maximum unlockable heroes |

---

## Design Patterns Used

| Pattern | Where | Purpose |
|---------|-------|---------|
| MVC | Character system | Separation of responsibilities |
| State Machine | Battle flow, Unit states | Turn management |
| Command | Combat system | Testablitity and reusability |
| Service Locator | Core services | Dependency management |
| Factory | Character creation, Attack commands | Object creation |
| Observer | Event Bus | Decoupled communication |
| Object Pool | Floating text | Performance |

---
