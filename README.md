# Antigravity
A game without gravity, you move automatically and can adjust your path by using a grappling hook, jumping or changing your direction.
This is a mobile game that playable with one hand. It contains several levels, some require you to be agile and dodge obstacles, while other are puzzles.

Play link: http://14411.hosts.ma-cloud.nl/antigravity/

I am currently busy converting this game to the IoCPlus framework, you can view my progress on the fix-game-basics branch.

Interesting Scripts:

Field generator: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/UI/LevelSelect/FieldGenerator.cs
Used by the levelselect screen.

CollisionDirection: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/CharacterControl/CollisionDirection.cs
Used by the player to detect the direction of collisions.

ControlDirection: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/CharacterControl/ControlDirection.cs
Decides which direction the the character goes to when colliding.

PointsFollowerSmooth: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/WayPoints/PointFollowerSmooth.cs
Used by the moving obstacles.

TriggerBase: https://github.com/Daniel95/Antigravity/blob/master/Assets/Scripts/Enviroment/Triggers/TriggerBase.cs
A base scripts to setup triggers. Triggers are used to scripts certain actions, like a tutorial or other events.
