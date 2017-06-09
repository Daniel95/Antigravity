# Antigravity
A game without gravity, you move automatically and can adjust your path by using a grappling hook, jumping or changing your direction.
This is a mobile game that playable with one hand. It contains several levels, some require you to be agile and dodge obstacles, while other are puzzles.

Play link: http://14411.hosts.ma-cloud.nl/antigravity/

This project has recently been remade with the IoCPlus framework.

Interesting Scripts:

HookContext: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/Weapon/Hook/HookContext.cs
A context is used to pair events and commands to each other.
The HookContext in particulair controls the behaviour of the hook, the weapon of the player.

CharacterVelocity: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/Character/Velocity/CharacterVelocityView.cs
Used by all characters in the game to easily control their velocity.

MoveTowards: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/Tools/MoveTowards/MoveTowardsView.cs
A scripts used by a lot of components to move from one place to another.

CollisionDirection: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/HitDetection/Collisions/CollisionHitDetectionView.cs
Used by the player to detect the direction of collisions.

Field generator: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/Legacy/UI/LevelSelect/FieldGenerator.cs
Used by the levelselect screen to generate a grid of levels.