# Crafting System Architecture

This document outlines the architecture for a comprehensive crafting system in the game. The system is designed to be modular, scalable, and easily integrated with the UI.

## Core Components

### 1. `CraftingRecipe` (Scriptable Object)

- **Purpose:** Defines a single crafting recipe.
- **Fields:**
    - `recipeName`: The name of the recipe.
    - `id`: A unique identifier for the recipe.
    - `description`: A brief description of the recipe.
    - `ingredients`: A list of `ItemStack` required to craft the item.
    - `output`: The `ItemStack` produced upon successful crafting.
    - `timeToComplete`: The time in seconds required to complete the crafting process.

### 2. `ICraftingRecipeCollection` (Interface)

- **Purpose:** Manages the collection of unlocked crafting recipes for a player.
- **Methods:**
    - `IReadOnlyDictionary<string, CraftingRecipe> UnlockedRecipes`: Provides read-only access to the unlocked recipes.
    - `bool TryGet(string recipeId, out CraftingRecipe recipe)`: Safely retrieves a recipe by its ID.
    - `bool TryUnlock(string recipeId)`: Attempts to unlock a new recipe.

### 3. `ICrafting` (Interface)

- **Purpose:** Exposes the functionality to execute a crafting recipe.
- **Methods:**
    - `Task<bool> Craft(CraftingRecipe recipe, IProgress<float> progress)`: Asynchronously executes a given recipe, reporting progress from 0.0 to 1.0. Returns `true` if crafting is successful, `false` otherwise.

### 4. `Crafting` (MonoBehaviour)

- **Purpose:** The main MonoBehaviour that orchestrates the crafting process.
- **Dependencies:**
    - `SlotInventory`: A reference to the player's inventory to manage ingredients.
    - `ICraftingRecipeCollection`: A reference to the player's unlocked recipes.
- **Implements:** `ICrafting`

### 5. `CraftingRecipeCollectionPresenter` (MonoBehaviour)

- **Purpose:** Acts as a bridge between the `ICraftingRecipeCollection` and the UI. It retrieves recipe data and formats it for display.
- **Dependencies:**
    - `ICraftingRecipeCollection`: The data source for recipes.

### 6. `CraftingPresenter` (MonoBehaviour)

- **Purpose:** Transforms data related to the crafting execution (e.g., time elapsed) into a format suitable for the UI.
- **Dependencies:**
    - `ICrafting`: The data source for crafting progress.

### 7. `CraftingRecipeCollectionView` (MonoBehaviour)

- **Purpose:** Renders the list of available crafting recipes and updates it dynamically based on data from the `CraftingRecipeCollectionPresenter`.

### 8. `CraftingView` (MonoBehaviour)

- **Purpose:** The main UI view for the crafting system. It handles user interactions, such as drag-and-drop of ingredients, and displays crafting progress.

## Event-Driven Architecture

To decouple the backend logic from the UI, the system will use a series of `GameEvent` Scriptable Objects to create event buses. This will allow presenters to listen for changes in the backend and update the UI accordingly, without direct dependencies.

### Events

- `OnRecipeUnlocked`: Fired when a new recipe is unlocked.
- `OnCraftingStarted`: Fired when a crafting process begins.
- `OnCraftingProgress`: Fired to update the crafting progress.
- `OnCraftingCompleted`: Fired when a crafting process is completed or fails.
