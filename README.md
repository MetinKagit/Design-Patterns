# Design Patterns

This repository showcases the core design patterns I’ve learned.  
In this adventure, I followed [Robert Nystrom’s *Game Programming Patterns*](https://gameprogrammingpatterns.com/) book through small, hands-on Unity projects.  

In each scene, you’ll find example code that illustrates a pattern (such as **Command**, **Component**, **Object Pool**, etc.) both in theory and in practice.  
My goal is to show how these patterns can be integrated into games and to share the best practices I discovered along the way.  

This repo is open to anyone looking for a reference or inspiration when learning design patterns.  
You can also explore the "Sources & Assets" section for detailed information on the sources and assets I used.

---

<details>
<summary>Command Pattern – Maze Replay & Undo/Redo System</summary>

## Command Pattern – Maze Replay & Undo/Redo System

This Unity project demonstrates the **Command Design Pattern** through an interactive maze game.

The player controls a **red cube** and must navigate it across valid tiles to reach the **star**.  
Every movement is stored as a **command**, enabling two key features:
- **Undo**: Reverse the last move.
- **Redo**: Reapply a previously undone move.

When the player reaches the star, the **entire move history** is automatically **replayed**, showcasing how the Command Pattern can store, reverse, and re-execute actions.

🎥 **Demo:**  

https://github.com/user-attachments/assets/d26b3d3e-f7db-44a8-83c8-231620d9dd5b

### Features
- Movement control using the Command Pattern.
- Undo and Redo functionality for player moves.
- Automatic replay of all moves upon reaching the goal.
- Clear example of decoupling input handling from execution logic.

### How It Works
1. **Input Handling** – Player input is translated into movement commands.
2. **Command Execution** – The player cube moves according to the executed command.
3. **History Tracking** – Commands are stored in a stack for undo/redo operations.
4. **Replay** – When the star is reached, commands are executed in sequence to replay the path.

</details>

---

<details>
<summary>Flyweight Pattern – Performance Comparison</summary>

## Flyweight Pattern – Performance Comparison

This Unity project demonstrates the **Flyweight Design Pattern** by comparing two versions of a simple carrot spawning system:  
1. **Non-Flyweight Version** – Each object holds its own unique data, resulting in higher memory usage and draw calls.  
2. **Flyweight Version** – Shared intrinsic data between objects reduces memory usage and improves rendering performance.

The purpose of this project is to show how applying the Flyweight Pattern can optimize **memory consumption** and **batch rendering** in Unity.

<p align="center">
  <img src="https://github.com/user-attachments/assets/c516adbd-41d9-4c38-a2b4-3f03bec85e7b" alt="Non-Flyweight" width="45%" />
  <img src="https://github.com/user-attachments/assets/26c809e7-1107-4c79-928c-59211f232e8e" alt="Flyweight" width="45%" />
</p>

### Key Takeaways
- Flyweight Pattern is highly effective for scenarios where many similar objects share common data.  
- This optimization is particularly useful for games with large numbers of repeated objects, such as bullets, tiles, or vegetation.

</details>

---
<details>
<summary>Observer Pattern – Event-Driven Achievements & HUD</summary>

## Observer Pattern – Event-Driven Achievements & HUD

This Unity scene applies the **Observer Design Pattern** to keep achievements and HUD updates **decoupled** from the event producers.  
When the player collects carrots/cauliflowers, jumps 12 times, or checks the mailbox, the relevant **achievement icon switches from grayscale to colored**, and the HUD counters update in real time.  
Progress **persists visually** even if the UI panel was closed; once opened, it reflects the correct state immediately thanks to a lightweight “replay on subscribe” mechanism.

🎥 **Demo:**  


https://github.com/user-attachments/assets/2597f4fb-8082-4993-863e-3c20426cba4c


### Features
- **Loose coupling:** Producers (Subjects) and listeners (Observers) are independent.
- **Achievements:**
  - Collect **9 carrots**
  - Collect **9 cauliflowers**
  - **Jump 12 times**
  - **Check the mailbox** (press **E** near it)
- **HUD:** Carrot/cauliflower counters update instantly.
- **Visual state:** Start with **grayscale** sprites, switch to **colored** on completion.
- **Replay on subscribe:** New listeners receive the current state right away.
- **Minimal core:** `ISubject<T>` / `IObserver<T>` only; no event bus, no third-party libs.

### How It Works
1. **Subjects**
   - `JumpSubject` → increments and notifies on each successful jump.
   - `CollectSubject` → tracks carrot/cauliflower counts and notifies.
   - `MailboxSubject` → one-time mailbox check, then notifies.
2. **Observers**
   - `HUDCounter` → updates HUD texts.
   - `AchievementIcon_CollectThreshold` → unlocks at **9/9** for the configured item type.
   - `AchievementIcon_JumpThreshold` → unlocks at **12** jumps.
   - `AchievementIcon_Mailbox` → unlocks on mailbox check.
3. **Replay**  
   Each Subject **replays** its current state to new subscribers so the UI shows correct progress even if the panel was previously inactive.
</details>

---

<details>
<summary>Prototype Pattern – Basic Dungeon Enemy Spawner (Data Clone + Prefab Clone)</summary>
  
## Prototype Pattern – Basic Dungeon Enemy Spawner (Data Clone + Prefab Clone)
  
This demo shows the **Prototype Pattern** in two layers:

- **Data Prototype (ScriptableObject)**: `EnemyData.Clone()` creates a **deep copy** of enemy stats.  
- **Prefab Prototype**: `Instantiate(prefab)` creates scene copies of the enemy object.

🎥 **Demo:**  


https://github.com/user-attachments/assets/b29678f1-695b-48e5-8010-0f40f3535fde


## How It Works

1. `EnemySpawner` clones the `BaseEnemy` data prototype.  
2. `WaveModifier` applies wave-based changes (HP, speed, color).  
3. The prefab is instantiated, and `Enemy.Init(data)` injects the cloned values.  
4. `EnemyMove` uses the speed value to move the enemy towards the `Goal`.  


## Running the Demo

- Open the project → load the `Scenes/Prototype` scene → press **Play**.  
- The top UI shows the current wave and total spawned enemies.  

## Why Prototype?

- **Prefab** = practical prototype clone for objects.  
- **ScriptableObject** = data prototype.  
- Deep copy prevents runtime changes from affecting the original asset.  
  
</details>

--- 

<details>
<summary>Singleton & Service Locator – Minimal Logging</summary>

## Singleton & Service Locator – Minimal Logging

This scene implements the **same logging task** with two patterns:
- **Singleton**: `SingletonLogger` exposes a single global instance.
- **Service Locator**: `ILog` is resolved through `Services.Log` without coupling to a concrete class.

UI: one Text element, two buttons  
- **Singleton** button → writes via the Singleton path  
- **Locator** button → writes via the Service Locator path  

<p align="center">
  <img src="https://github.com/user-attachments/assets/b0eaaf59-ad55-4133-84f7-159f29679a87" alt="Singleton Log Demo" width="49%" />
  <img src="https://github.com/user-attachments/assets/fde4da64-d8ea-4847-aa12-f2b00ef0960f" alt="Service Locator Log Demo" width="49%" />
</p>

### How It Works
- On scene load, `Installer.Awake()` registers a provider: `Services.Provide(new MemoryLog())`.
- Press **Singleton**: `SingletonLogger.Instance.Write("...")` → `logText.text = SingletonLogger.Instance.ReadAll()`.
- Press **Locator**: `Services.Log.Write("...")` → `logText.text = Services.Log.ReadAll()`.
- If no provider is registered, `Services.Log` falls back to **NullLog** (no-op), so calls are safely ignored.

</details>

--- 

<details>
<summary>State Pattern – Enemy AI</summary>
  
# State Pattern – Enemy AI

This project demonstrates a grid-based enemy AI built with the **State Pattern**.  
Each state inherits from a shared base class (`EnemyState`) and follows a lifecycle of `Enter`, `Exit`, `UpdateState`, and `DecideDir`.

🎥 **Demo:**  

https://github.com/user-attachments/assets/e8c84cca-f5d7-4700-a43b-c32dae2b295f

## Behaviors

- **Wander**: Picks a random passable direction (avoids turning back if possible).  
- **Chase**: Moves toward the player, minimizing Manhattan distance.  
- **Frightened**: Moves away from the player. When entered, the enemy turns cyan. When the timer ends, the state is popped off the stack.

## Transition Rules

- The enemy starts in **Wander**.  
- After each tile step, the distance to the player is checked:
  - `dist ≤ chaseRangeCells` → **Chase**  
  - `dist > chaseRangeCells` → **Wander**  
- If **Frightened** is active, automatic transitions are ignored.  
- Collecting a big score pushes **Frightened**; when time is up, it pops.

## Movement Flow (Tile-Based)

1. The active state decides direction using `DecideDir`.  
2. `TryStartStep` checks collisions; if clear, the enemy starts moving to the target tile.  
3. `FixedUpdate` moves the enemy until it reaches the tile, then ends the step.

## Future Improvements

This project focuses on demonstrating the **State Pattern** itself.  
Pathfinding algorithms (e.g., **A\***, **BFS**, **DFS**) were intentionally left out to keep the focus clear, but they can be added later to achieve more advanced chasing and evasion behavior.
  
</details>

---

<details>
<summary>Decorator Pattern – Dynamic Spell Modification with ScriptableObjects</summary>

# Decorator Pattern – Dynamic Spell Modification with ScriptableObjects

This Unity project demonstrates the Decorator Design Pattern by creating a dynamic spell system where a base spell’s properties can be altered at runtime.

The system uses ScriptableObjects to define a base spell (SpellDefinitionSO) and a series of modifications (SpellModSO). These modifications act as decorators, wrapping the base spell to change its Damage and ManaCost without altering its core class. The UI updates in real-time to reflect the final stats of the decorated spell.

🎥 Demo:


https://github.com/user-attachments/assets/6e34f5df-4896-44e6-ae4b-ad40e57cb128


## Features

**Dynamic Modification:** Add functionalities like damage boosts or mana discounts to a spell at runtime.

**Data-Driven Design:** Base spells and modifications are managed as ScriptableObject assets, allowing for easy configuration and reuse.

**Loose Coupling:** The base spell (BasicSpell) is completely unaware of the decorators (DamageBoost, ManaDiscount) that wrap it.

**Real-Time UI Feedback:** The UI instantly reflects the combined effects of all applied decorators, showing the final stats and highlighting changes.

## How It Works

**Base Object:** A SpellDefinitionSO creates the initial ISpell object (a BasicSpell) with default stats.

**Decorators as Assets:** Each modification, like DamageBoostSO or ManaDiscountSO, is a ScriptableObject that knows how to "wrap" an existing ISpell object with its corresponding decorator class.

**Runtime Wrapping:** The CardPresenter class builds the final spell by starting with the base spell and sequentially wrapping it with decorators based on UI interactions (e.g., button toggles).

**Property Delegation:** When a property like Damage is accessed on the final object, the call is delegated down the chain. Each decorator modifies the result from the object it wraps before passing it back up.

**UI Update:** The CardPresenter calculates the final stats from the fully decorated spell object and updates the CardViewUnity to display the results, providing immediate visual feedback.

</details>

---

<details>
<summary>Object Pool Pattern – Carrot Spawner</summary>

# Object Pool Pattern  – Carrot Spawne
This Unity project demonstrates the Object Pool Design Pattern for performance optimization within a carrot spawning system.

Creating (Instantiate) and destroying (Destroy) objects repeatedly, especially in quick succession, can be a performance bottleneck and lead to hitches. The Object Pool pattern eliminates these costly operations.

In this project, instead of being continuously destroyed and recreated, carrots are stored in a pool. They are then retrieved and reused as needed. When a carrot goes off-screen or enters a designated zone, it is not destroyed but is simply returned to the pool for later use.

🎥 Demo:



https://github.com/user-attachments/assets/c5f6a29b-87ac-4e82-8490-56bb10587ebb



## How It Works
**Pool Initialization:** When the game starts, the CarrotPool class pre-creates a specific number of carrot objects and stores them in a pool.

**Object Retrieval (Get):** When the CarrotSpawner needs to launch a new carrot, it retrieves an available object from the pool. This process is much faster than Instantiate.

**Object Release (Release):** When a carrot becomes invisible or collides with the KillZone, it signals the pool to be returned, instead of being destroyed.

**Reusability:** Once a carrot is returned to the pool, it is ready to be reused for the next spawn.

## Why Use It?
**Performance:** It avoids expensive Instantiate and Destroy operations, which can increase frame rate (FPS) and prevent hitches.

**Memory Management:** It reduces memory allocation, leading to less frequent garbage collection.

**Flexibility:** It provides greater control over the objects' lifecycle.
</details>

---



## Sources & Assets
You can find all the sources and assets I used in this Word document:  
[**Sources & Assets**](https://docs.google.com/document/d/1LrV8sxgsNLd5clktmgWa2SVCkJgxFOmjXMhrLYuYcd8/edit?usp=sharing)
