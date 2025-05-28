Task feature list to do for Triheroes Mighty
0 - Pixify more features
    - Change Pixify serialization from using XNode to using a general scriptable object
        - new UI
        - new editor
    - add a way for child action to change decorator states
    - add decorator : selector, random, wait, condition
    - add orphan node for entities that don't need a gameobject
    - manage gameobject destruction
    - change all entity system from using list to using array

1 - Character basic modules:
    - Skin ( mesh, textures, animations )
    - Capsule Character Controller ( movement, gravity, ground data ) - DONE
    - Dimensions ( height, width, position ) - DONE
    - Actor ( role, stats, abilities )
    - navmesh character controller ( pathfinding, non AI pathfinding )
    - reactable
    - controller script ( Player Controller, NPC Controller , Enemy Controller )

2 - Movement behavior actions:
    - ground movement simple ( idle - walk )
    - ground movement complex ( idle - walk - run - sprint - brake - brake rotation ) - DONE
    - ground lateral movement ( walk - idle )
    - ground tired movement ( walk - idle )
    - ground crouch movement ( crouch walk - crouch idle )
    - falling movement ( fall - air move - landing ) - DONE
    - jump movement simple ( jump - jump land ) - DONE
    - jump movement complex ( jump - leading foot ) - DONE
    - dash movement ( dash - dash land )
    - swimming movement ( swim - idle )

3 - Note feedback from the previous public playtest:
    - interface should be more intuitive
    - stats are confusing
    - add more option for input
    - game is too hard

4 - raw features to categorize later:
    - add a way to create a custom character
    - cinematic system
    - lore
    - quest system
    - character strategy
    - psx1 shaders

5 - features not important but still requested by the public playtest:
    - add local multiplayer
    - add voice over

Current ETA for the project:
    - estimated time for demo : 14 weeks
    - estimated time for full game : 2 years