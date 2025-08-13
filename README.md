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
https://github.com/user-attachments/assets/0a2d6099-d784-4a40-8fc2-a44f57d2ff26

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


## Sources & Assets
You can find all the sources and assets I used in this Word document:  
[**Sources & Assets**](https://docs.google.com/document/d/1LrV8sxgsNLd5clktmgWa2SVCkJgxFOmjXMhrLYuYcd8/edit?usp=sharing)
