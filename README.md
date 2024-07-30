This project is to produce a Tactical RPG Engine for the Unity game development tool suite.

Completion of this project is a long ways away, as there are a lot of boiler plate systems to write, connect, rewrite, and polish. However, it will be a good way for me to learn some C# after living in Python land at work for so long.


~ Willis Rogers, dev.


How to use with Unity:

There are a lot of systems that will need to be described and broken down to make this usable by someone who didn't write it. One day all of these will have sections, probably explained in a text document somewhere.



World Map
Dungeon Maps
Stages
Dynamic Menu Generator
Resource Creation
  - Items / Units / location data / etc.
Additive Scenes
- Inventory
- Unit Management
- Maps
- Shops
- Theater
Stage Actions
- Anything and every gameplay event that can be done in a stage
The Stage Director
- Queue manager for the stage actions
Stageplays
- scripts of actions to be carried out on a stage.
Stage Design
Path finding with multi-tile units
- Standard, Big, Very Big, and Epic Node Maps for matching unit sizes.
Data Persistence
The nightmare of serializing an inventory...
The Big 4 Managers
- PersistantDataManager, GameDataManager, SoundManager, SceneTransitionManager
The Middle Managers
- Databases, menu managers, lorekeeper. Ones kept alive after a game is loaded.
Keyboard & Gamepad controls (I guess i'll make it work with a mouse...)
