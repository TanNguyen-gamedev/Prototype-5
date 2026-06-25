# Prototype 5: Architecture & Lessons Learned

## Overview
Prototype 5 is a Fruit Ninja-style arcade game. The architectural focus is entirely on a pure MVP (Model-View-Presenter) / Humble Object pattern for the UI, decoupled game state via ScriptableObject Event Channels, and separating raw input logic from physical slicing detection.

## Architecture & Implementation Details

### 1. Data-Driven UI & State Management
- **Total Decoupling**: The `GameManager.cs` has absolutely zero hard references to UI components (`HUD.cs`, `PauseMenu.cs`, `TitleScreen.cs`), and the UI components have zero references to the `GameManager`.
- **Event Channels**: All cross-system communication is handled through ScriptableObject Event Channels:
  - `IntEventChannelSO` handles dynamic values like `_onScoreChange`, `_onLiveChange`, and `_onTargetHit`.
  - `BoolEventChannelSO` broadcasts states like `_onGameOver` and `_onPauseGame`.
  - `VoidEventChannelSO` triggers parameterless actions like `_onResumeGame` and `_onGameRestart`.
- **UI Logic**: UI scripts act strictly as Views. For example, `HUD.cs` listens for `_onScoreChange` and simply updates the TextMeshPro text, while its buttons raise events that the `GameManager` listens to.

### 2. Input Abstraction & Slicing (`SliceInput.cs`)
- **Input Parsing**: Mouse/Touch drag input is captured via the New Input System and passed into `SliceInput.cs`.
- **Math-Based Raycasting**: Instead of relying purely on screen-space 2D colliders, `SliceInput.cs` converts screen coordinates to 3D world rays (`Camera.main.ScreenPointToRay`).
- **Zero-Allocation Slicing**: To detect if the drag intersected a target, it calculates the direction and magnitude between the previous and current frame, then uses `Physics.SphereCastAll`. 
- **Separation of Concerns**: The visual trail effect is separated from the physical detection, ensuring slicing feels responsive even if the frame rate drops.

### 3. Difficulty Management (`TitleScreen.cs` & `GameManager.cs`)
- **Event-Driven Start**: The `DifficultyButton` script fires an `IntEventChannelSO` containing the chosen difficulty scalar. 
- **Spawning Logic**: `GameManager.cs` listens for this start event, calculates the new `_spawnRate`, and initiates the `SpawnTarget` Coroutine loop.

### 4. Target Physics (`Target.cs`)
- **Randomized Tosses**: When a target spawns, it immediately applies randomized upward force (`_rb.AddForce`) and spin (`_rb.AddTorque`) using `ForceMode.Impulse`. 
- **Self-Reporting**: When hit by the `SliceInput` SphereCast, the `Target` simply triggers its own `Explode()` method and raises either the `_onBombHit` or `_onTargetHit` event, allowing the `GameManager` to update the score or trigger a Game Over without manually checking every target.

## Lessons Learned
- **Physics vs. Visuals**: Separating the visual trail rendering from the actual `SphereCastAll` slicing logic proved critical. It makes the input feel much more responsive, as the physics engine handles intersection detection based on mathematical deltas rather than relying on a visual trail collider catching up to the mouse.
- **Scalable Architecture**: The Event Channel architecture proved incredibly scalable. Adding the Pause Menu and Volume Slider required zero modifications to the existing `GameManager` or `HUD` code; they simply listened to or fired the appropriate new Event Channels.