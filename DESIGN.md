# Prototype 5: Architecture & Lessons Learned

## Architecture
- **Input Abstraction**: Extracted raw click/drag logic into `SliceInput` and `SliceTrail`, separating the visual trail from the physics-based slicing intersection logic.
- **Data-Driven UI & State**: All UI components (`HUD`, `PauseMenu`, `TitleScreen`) and game states (Score, Lives, Pause, Game Over) are strictly decoupled using `ScriptableObject`-based Event Channels.
- **Difficulty Management**: Difficulty is set via UI buttons triggering an event with an integer value, which the `GameManager` uses to scale the spawn rate.

## What We Learnt
- **UI Architecture**: Implemented a clean Humble Object/MVP structure for the UI. Menus and HUD elements do not manipulate the `GameManager` directly; they interface purely via Event Channels.
- **Physics vs. Visuals**: Separating the visual trail rendering from the actual `SphereCast` slicing logic makes the input feel much more responsive and robust.
