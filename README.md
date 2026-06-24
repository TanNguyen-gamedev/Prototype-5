# Prototype 5: Clicky Mouse

**Play the Game:** [Junior Programmer on Unity Play](https://play.unity.com/en/games/cc52519c-3a41-4a3b-a2c8-e76e0d26d5a2/junior-programmer)

## Gameplay Mechanic
A "Fruit Ninja" style clicking/swiping game. Targets are launched into the air, and the player must slice them before they fall out of bounds, while avoiding "bad" targets.

## Core Game Loop
1. **Title Screen**: Player selects a difficulty using `DifficultyButton`, which sets the spawn rate and starts the game.
2. **Launch**: `GameManager` tosses an array of targets into the air with randomized forces and torques.
3. **Slice**: Player clicks or drags across the screen to generate a `SliceTrail` and intersect target colliders.
4. **Score & Lives**: Slicing good targets increases score; slicing bad ones or letting good ones fall decreases lives. Game ends when lives hit 0.
5. **Pause & Audio**: Player can pause the game and adjust the volume using the `PauseMenu`.

## Dataflow
- **Input**: Mouse/Touch drag data is processed by `SliceInput`, updating a trail visual and performing SphereCasts/Collider overlaps.
- **Game Flow**: `DifficultyButton` triggers the start state via events. `GameManager` manages the active target loop.
- **UI & State**: `HUD`, `PauseMenu`, and `TitleScreen` listen to Event Channels (`IntEventChannelSO`, `BoolEventChannelSO`) to update the screen, completely decoupled from the `GameManager`.