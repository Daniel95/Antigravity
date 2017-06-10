# Antigravity
A game without gravity, you move automatically and can adjust your path by using a grappling hook, jumping or changing your direction.
This is a mobile game that playable with one hand. It contains several levels, some require you to be agile and dodge obstacles, while other are puzzles.

Play link: http://14411.hosts.ma-cloud.nl/antigravity/

This project has recently been remade with the IoCPlus framework.

Interesting Scripts:

CanvasUIView: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/UI/Canvas/CanvasUIView.cs
This canvas script instantiates CanvasLayers, using reflection, as children so other scripts can add UI elements to different Layers.
CanvasUIView also keeps track of all items in the layers and can retrieve and remove them.

ScreenShake: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/Camera/ScreenShake/ScreenShake.cs
A screenshake script that can be used by other scripts to create a custom screenshake for each situation.

HookContext: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/Weapon/Hook/HookContext.cs
A context is used to pair events and commands to each other.
The HookContext in particulair controls the behaviour of the hook, the weapon of the player.

CharacterVelocity: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/Character/Velocity/CharacterVelocityView.cs
Used by all characters in the game to easily control their velocity.

CollisionDirection: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/Character/SurroundingDirection/CollisionDirection/CharacterCollisionDirectionView.cs
Used by the player to detect the direction of collisions.

FieldGenerator : https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/Legacy/UI/LevelSelect/FieldGenerator.cs
Used by the levelselect screen to generate a grid of levels.