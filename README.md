# Curse of Redville 2025 - AI Agent Development Guide

This document provides guidelines for AI agents contributing to the "Curse of Redville 2025" Unity project. Adhering to these conventions is crucial for maintaining code quality and consistency.

## 1. Project Overview

**Curse of Redville** is a survival horror game that replaces direct combat with a complex and strategic trap-crafting system.

*   **Genre**: Survival Horror, Psychological Horror.
*   **Core Mechanic**: Players cannot engage in direct combat. Survival depends on scavenging resources to craft, deploy, and chain together various traps to hinder, deter, or neutralize enemies.
*   **Gameplay Loop**: The core loop consists of exploration, resource gathering, discovering trap recipes, crafting traps, and strategically deploying them.
*   **Narrative**: The player is drawn to the cursed town of Redville by a cryptic invitation from the mayor concerning the player's long-lost father. The story involves uncovering the town's dark secrets and the protagonist's connection to them.

## 2. Core Architecture

The project follows a separation of concerns pattern, loosely based on MVC/MVP, with a strong emphasis on an event-driven architecture.

*   **Models**: Located in `Assets/__MAIN/Source/Models`. These are plain C# classes responsible for holding data and game state (e.g., `MovementModel`).
*   **Controllers**: Located in `Assets/__MAIN/Source/Controllers`. These are `MonoBehaviour` classes that contain game logic, respond to player input, and manipulate the models (e.g., `PlayerController`).
*   **Event System**: Located in `Assets/__MAIN/Source/Events`. A custom event system (`VoidEvent`, `GameEvent`) is used for communication between decoupled components. Prefer using events over direct method calls to reduce coupling.
*   **Inventory**: Located in `Assets/__MAIN/Source/Inventory`. This module manages all inventory logic, including item data (`ItemData`), item stacks (`ItemStack`), and inventory implementations (`SlotInventory`).
*   **Editor Scripts**: All Unity Editor extensions and custom inspectors are located in `Assets/__MAIN/Editor`.

## 3. AI Agent Directives

*   **Strict Adherence**: Strictly follow all conventions outlined in this document.
*   **No Comments**: **Do not add any inline (`//`) or block (`/* */`) comments to the code.** The code should be self-documenting through clear naming and structure. XML documentation is also forbidden.
*   **Existing Patterns**: Replicate and use existing architectural and coding patterns. Do not introduce new patterns or libraries without explicit instruction.
*   **File Location**: All new runtime C# scripts must be created within the appropriate subdirectory of `Assets/__MAIN/Source`.
*   **Conciseness**: Keep code concise and clear.

## 4. C# Coding Style & Conventions

### Naming Conventions

| Element                 | Convention                               | Example                  |
| ----------------------- | ---------------------------------------- | ------------------------ |
| Namespaces              | `__MAIN.Source.<Directory.Path>`         | `__MAIN.Source.Inventory`  |
| Classes, Structs, Enums | `PascalCase`                             | `SlotInventory`, `ItemData`  |
| Interfaces              | `IPascalCase`                            | `IInventory`, `IState`     |
| Public Fields/Properties| `PascalCase`                             | `Capacity`, `IsFull`       |
| Private Fields          | `_camelCase` (leading underscore)        | `_slots`, `_slotsCount`    |
| Methods                 | `PascalCase`                             | `TryAdd`, `RecalculateTotals`|
| Local Variables         | `camelCase`                              | `remainingStack`, `quantity` |
| Method Parameters       | `camelCase`                              | `stackToAdd`, `item`       |

### Variable Declarations

*   **Avoid `var`**: Do not use the `var` keyword. Always use explicit types for variable declarations to ensure clarity.
*   **Instantiation**: For object instantiation, use the explicit type on the left-hand side.
    ```csharp
    // Correct
    MyClass myObject = new();
    int myNumber = 10;

    // Incorrect
    var myObject = new MyClass();
    var myNumber = 10;
    ```

### Formatting

*   **Indentation**: Use 2 spaces for indentation. Do not use tabs.
*   **Braces**: Use the K&R style, where the opening brace is on the same line as the statement.

    ```csharp
    public class MyClass {
      private void MyMethod() {
        // code
      }
    }
    ```

*   **`using` Statements**: Place all `using` statements *inside* the namespace declaration.

    ```csharp
    namespace __MAIN.Source.MyModule {
      using System;
      using UnityEngine;

      // ... class definition
    }
    ```

*   **Fields**: Use `[SerializeField]` for private fields that need to be exposed to the Unity Inspector. Public fields should be avoided unless necessary for a public API.
*   **Readonly**: Use `readonly` for fields that are initialized only in the constructor or at declaration.

This guide is the source of truth for all development practices. Always refer to it before making changes.