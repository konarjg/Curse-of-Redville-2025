# Curse of Redville 2025 - Crafting System Development Guide

This document provides guidelines for AI agents contributing to the crafting system of the "Curse of Redville 2025" Unity project. Adhering to these conventions is crucial for maintaining code quality and consistency.

## 1. Crafting System Overview

The crafting system is a core mechanic in "Curse of Redville." It allows players to create items from recipes they have unlocked. The system is designed to be decoupled, with a clear separation of concerns between the data, logic, and presentation layers.

## 2. Core Architecture

The crafting system follows the project's event-driven architecture, with a structure similar to the existing `Inventory` system.

*   **Models**: Located in `Assets/__MAIN/Source/Crafting/Recipes`. These are scriptable objects that define the data for crafting recipes.
*   **Interfaces**: Located in `Assets/__MAIN/Source/Crafting`. These define the contracts for the crafting system's backend.
*   **Backend**: Located in `Assets/__MAIN/Source/Crafting`. These are `MonoBehaviour` classes that implement the crafting system's logic.
*   **Presenters**: Located in `Assets/__MAIN/Source/Crafting/Presenters`. These `MonoBehaviour` classes transform data from the backend into a format suitable for the UI.
*   **Views**: Located in `Assets/__MAIN/Source/Crafting/Views`. These `MonoBehaviour` classes are responsible for rendering the UI and handling user input.
*   **Events**: Located in `Assets/__MAIN/Source/Crafting/Events`. These scriptable objects are used to decouple the different layers of the crafting system.

## 3. AI Agent Directives

*   **Strict Adherence**: Strictly follow all conventions outlined in this document and the main `AGENTS.md` file.
*   **No Comments**: Do not add any comments to the code. The code should be self-documenting.
*   **Existing Patterns**: Replicate and use existing architectural and coding patterns.
*   **File Location**: All new runtime C# scripts must be created within the appropriate subdirectory of `Assets/__MAIN/Source/Crafting`.

## 4. Crafting System Components

### 4.1. `CraftingRecipe`

*   **Type**: `ScriptableObject`
*   **Location**: `Assets/__MAIN/Source/Crafting/Recipes`
*   **Fields**:
    *   `Name`: `string` - The name of the recipe.
    *   `Id`: `string` - A unique identifier for the recipe.
    *   `Description`: `string` - A description of the recipe.
    *   `Ingredients`: `List<ItemStack>` - A list of `ItemStack` objects required to craft the recipe.
    *   `Output`: `ItemStack` - The `ItemStack` produced by the recipe.
    *   `TimeToComplete`: `float` - The time in seconds required to craft the recipe.

### 4.2. `ICraftingRecipeCollection`

*   **Type**: `interface`
*   **Location**: `Assets/__MAIN/Source/Crafting`
*   **Properties**:
    *   `Items`: `IReadOnlyDictionary<string, CraftingRecipe>` - A read-only dictionary of all unlocked recipes, with the recipe ID as the key.
*   **Methods**:
    *   `TryGet(string searchQuery, out CraftingRecipe recipe)`: `bool` - Tries to get a recipe by its ID.
    *   `TryUnlock(string recipeId)`: `bool` - Tries to unlock a recipe.

### 4.3. `ICrafting`

*   **Type**: `interface`
*   **Location**: `Assets/__MAIN/Source/Crafting`
*   **Methods**:
    *   `Execute(CraftingRecipe recipe, IProgress<float> progress)`: `Task` - Executes a given recipe asynchronously, with progress reporting.

### 4.4. `Crafting`

*   **Type**: `MonoBehaviour`
*   **Location**: `Assets/__MAIN/Source/Crafting`
*   **Implements**: `ICrafting`
*   **Fields**:
    *   `_inventory`: `SlotInventory` - A reference to the player's inventory.
    *   `_craftingRecipeCollection`: `ICraftingRecipeCollection` - A reference to the collection of unlocked recipes.
*   **Methods**:
    *   `Execute(CraftingRecipe recipe, IProgress<float> progress)`: `Task` - (Not yet implemented)

### 4.5. `CraftingRecipeCollectionPresenter`

*   **Type**: `MonoBehaviour`
*   **Location**: `Assets/__MAIN/Source/Crafting/Presenters`
*   **Purpose**: Retrieves data from `ICraftingRecipeCollection` and transforms it into a format suitable for the UI. (Not yet implemented)

### 4.6. `CraftingPresenter`

*   **Type**: `MonoBehaviour`
*   **Location**: `Assets/__MAIN/Source/Crafting/Presenters`
*   **Purpose**: Transforms data related to executing a recipe from `ICrafting` to data usable by the UI (e.g., time elapsed to progress). (Not yet implemented)

### 4.7. `CraftingRecipeCollectionView`

*   **Type**: `MonoBehaviour`
*   **Location**: `Assets/__MAIN/Source/Crafting/Views`
*   **Purpose**: Updates a list of rendered recipes dynamically based on the presenter's data. (Not yet implemented)

### 4.8. `CraftingView`

*   **Type**: `MonoBehaviour`
*   **Location**: `Assets/__MAIN/Source/Crafting/Views`
*   **Purpose**: Updates the UI and handles drag and drop between the player's `SlotInventoryView` and the crafting system UI, and progress reporting. (Not yet implemented)

## 5. Event-Driven Architecture

The crafting system will use `GameEvent` scriptable objects to decouple the backend, presenters, and UI. The following events will be created:

*   **`CraftingRecipeUnlockedEvent`**: `GameEvent<string>` - Fired when a recipe is unlocked.
*   **`CraftingStartedEvent`**: `GameEvent<CraftingRecipe>` - Fired when a recipe starts crafting.
*   **`CraftingProgressEvent`**: `GameEvent<float>` - Fired to report crafting progress.
*   **`CraftingCompletedEvent`**: `GameEvent<CraftingRecipe>` - Fired when a recipe is completed.

This guide is the source of truth for all crafting system development. Always refer to it before making changes.
