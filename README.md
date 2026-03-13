# Vertigo Games Case Study: Lucky Wheel

A robust, scalable, and event-driven implementation of a Lucky Wheel game for Unity. This project demonstrates a clean separation of concerns using a custom **MVC (Model-View-Controller)** architecture and an **Event Bus** system.

## 🚀 Key Features

- **Infinite Level System**: Players progress through zones, with difficulty/rewards scaling based on zone quality.
- **Zone Milestones**: 
  - **Default Zones**: Standard rewards, contains the Bomb.
  - **Safe Zones (Every 5th)**: No Bomb, better rewards.
  - **Super Zones (Every 30th)**: Premium rewards, no Bomb.
- **Bomb Mechanic**: High-stakes gameplay where hitting a Bomb requires deciding to **Revive** (using currency) or **Give Up** (losing current bag items).
- **Withdraw System**: Players can bank their collected items at any time during Safe or Super zones.
- **Reactive UI**: Built with an `Observable` pattern to ensure the UI always reflects the underlying data model automatically.

## 🏗️ Architecture

The project follows a strict architectural pattern to ensure maintainability and testability:

### 1. MVC Pattern
Every UI component is split into three parts:
- **Model**: Holds the data and state (e.g., `WheelModel`, `BagModel`).
- **View**: Handles the visual representation and Unity-specific components (e.g., `WheelView`, `BagView`).
- **Controller**: Manages logic, listens to events, and updates the Model/View accordingly.

### 2. Event Bus
A global communication layer that allows different systems to talk to each other without direct coupling.
- **Example**: `WheelGame` raises `EAddItem`, and `BagController` catches it to update the player's inventory.

### 3. Application State Management
The project uses a State Machine to manage high-level application flows:
- **Preload**: Initializing services and loading data.
- **Menu**: Handling navigation and user entry.
- **Gameplay**: The active Lucky Wheel session.
- **Quit**: Cleanup and safe exit.

### 4. Observable Pattern
A custom wrapper for data types that allows the UI to "subscribe" to changes. When a value in the Model changes, the View updates automatically.

---

## 📂 Project Structure

```text
Assets/Project/Scripts/
├── ApplicationState/      # Application-level state management
├── Bootstrap/             # Entry point and initial setup
├── EventBus/              # Global event system and event definitions
├── Game/                  # Core gameplay logic (WheelGame.cs)
│   └── WheelGame/         # Data providers and quality processors
├── Managers/              # Global managers (Currency, Storage)
├── UI/                    # UI Systems (MVC based)
│   ├── Core/              # Base classes (SystemBase, ViewBase, etc.)
│   ├── Wheel/             # The spinning wheel system
│   ├── Bag/               # Item collection and display system
│   ├── ZoneIndicator/     # Level/Zone progress tracking
│   └── GamePlay/          # Global session stats (Currency display)
└── Utility/               # Helper extensions and common tools
```

---

## 🛠️ How It Works

1.  **Initialization**: `WheelGame` initializes the session with starting currency and sets the first zone to "Default".
2.  **The Spin**: When the user presses Spin, the `WheelController` triggers an event. `WheelGame` determines the result based on probability and quality.
3.  **The Result**:
    - **Item**: The `BagSystem` receives an `EAddItem` event and adds the item to the UI.
    - **Bomb**: The `RevivePanel` appears, pausing the game and offering choices.
4.  **Progression**: After a successful spin, the zone index increments, and the `ZoneIndicatorSystem` updates the UI to reflect progress toward the next Safe/Super zone.
5.  **Completion**: Upon **Withdraw**, the current session bag is cleared, and the player returns to the first zone.

---

## 💻 Tech Stack

- **Unity 2022+**
- **C#** (Modern patterns)
- **DOTween**: For smooth wheel rotations and UI transitions.
- **TextMeshPro**: For high-quality text rendering.
- **Addressables**: For efficient asset management (if applicable).

---

## 📝 Usage

To add a new UI system:
1.  Inherit from `SystemBase<Model, View, Controller>`.
2.  Define your `IModel` for data.
3.  Define your `ViewBase` for UI references.
4.  Implement logic in `ControllerBase`.
