# Game Design Document (GDD/GTD)

**[Jump Queen]**
By: Justin Pearson & Ben Walsh

## Table of Contents
1. [Introduction](#introduction)
   - 1.1 [Quick Pitch](#quick-pitch)
   - 1.2 [Development Schedule](#development-schedule)
2. [Game Overview](#game-overview)
   - 2.1 [Game Concept](#game-concept)
   - 2.2 [Audience](#audience)
   - 2.3 [Genre](#genre)
   - 2.4 [Setting](#setting)
   - 2.5 [World Structure](#world-structure)
   - 2.6 [Player](#player)
   - 2.7 [Core Loop](#core-loop)
   - 2.8 [Look & Feel](#look--feel)
3. [Gameplay](#gameplay)
   - 3.1 [Objectives](#objectives)
   - 3.2 [Progression](#progression)
   - 3.3 [Play Flow](#play-flow)
   - 3.4 [Difficulty](#difficulty)
4. [Mechanics](#mechanics)
   - 4.1 [Rules](#rules)
   - 4.3 [Physics](#physics)
   - 4.5 [Character Movement](#character-movement)
   - 4.6 [Player Interaction](#player-interaction)
     - 4.6.1 [Game Menus](#game-menus)
     - 4.6.2 [Saving](#saving)
     - 4.6.3 [Game Options](#game-options)
   - 4.7 [Assets](#assets)
5. [Graphics and Audio](#graphics-and-audio)
   - 5.1 [Visual System](#visual-system)
     - 5.1.1 [Player Camera](#player-camera)
     - 5.1.2 [Landscape](#landscape)
   - 5.2 [Interface](#interface)
   - 5.3 [Audio System](#audio-system)
     - 5.3.1 [Game Music](#game-music)
     - 5.3.2 [Audio Look & Feel](#audio-look--feel)
6. [Characters](#characters)
   - 6.1 [Main Character](#main-character)

## Introduction <a name="introduction"></a>
### Quick Pitch <a name="quick-pitch"></a>
Jump Queen will be a 2D climbing platformer, taking inspiration from the game Jump King. The player will try to move their way up the play area which will be in a castle to complete the game. The player can only move by jumping and can control the height and direction of their jump.

### Development Schedule <a name="development-schedule"></a>
![Development schedule image](JumpQueenDevSchedule.png)

## Game Overview <a name="game-overview"></a>
### Game Concept <a name="game-concept"></a>
The gameplay is about climbing the various obstacles that are in the player's way. If the player fails to make the jump they will fall back to previous areas they have already passed. As they continue to climb the areas will get progressively more difficult. The point of the game is to frustrate players when they fail but as they progressively get higher they enjoy that feeling of accomplishment.

### Audience <a name="audience"></a>
The age range of people who play these types of games is wide, younger people and older players can enjoy this style of game. The type of players who play these games are achievement based players who want to enjoy the difficulty of the game until they can finally beat it. When they do finally beat the game players will continue to play when they try and do things like speedruns.

### Genre <a name="genre"></a>
This genre would be a 2D platformer.

### Setting <a name="setting"></a>
This game would take place in a fantasy world with many different biomes that the player will have to traverse.

### World Structure <a name="world-structure"></a>
The player will move linearly through the vertical world by jumping through different obstacles that are in his way.

### Player <a name="player"></a>
The game is single player.

### Core Loop <a name="core-loop"></a>
The player can move left and right as well as jump at varying heights based on the player's input. If the player fails to make a jump they will then fall down to a previous level.

### Look & Feel <a name="look--feel"></a>
The look and feel of the game will closely resemble the game Jump King.

## Gameplay <a name="gameplay"></a>
### Objectives <a name="objectives"></a>
The objective of the player is to climb to the top of the game, which gets progressively harder the higher you get.

### Progression <a name="progression"></a>
The player will be presented with a screen with platforms, once they reach the top of that screen the view will shift to a new screen with new platforms to climb.

### Play Flow <a name="play-flow"></a>
The player will move their way up the game trying to get to the top. If the player misses a platform and falls, they will fall until they hit a platform, which can be any distance.

### Difficulty <a name="difficulty"></a>
Like Jump King, Jump Queen will have a steep learning curve and will be intentionally difficult.

## Mechanics <a name="mechanics"></a>
Most of the time, you will customize this section of the GDD to each of your games. For example, if your game has combat in it, you want to include a segment of “Combat” and one for “AI”, or if your game has a unique system for spawning, you’ll want to mention how it works.

### Rules <a name="rules"></a>
The player can climb and fall by jumping and win by reaching the top.

### Physics <a name="physics"></a>
The main physics that the game has is gravity, the character can jump over 3 times his height but the gravity should feel like it's normal, not low like they are jumping on the moon.

### Character Movement <a name="character-movement"></a>
A player can jump, run left, run right.

### Player Interaction <a name="player-interaction"></a>
The player can interact with the physical world around him like the various blocks that they would jump from and too.

#### Game Menus <a name="game-menus"></a>
The game menu will have a start button and a high score, as well as settings.

#### Saving <a name="saving"></a>
The game state will not be saved only the high score.

#### Game Options <a name="game-options"></a>
A player can change things like game controls as well as settings like music noise level.

### Assets <a name="assets"></a>
A list of the main assets that the game will use, split by type: “Player Model, Terrain assets, menu assets, sound assets,

## Graphics and Audio <a name="graphics-and-audio"></a>
### Visual System <a name="visual-system"></a>
The game will be a simple 2D system.

#### Player Camera <a name="player-camera"></a>
The camera will be fixed in the center of an area, only moving when the player moves to a new area.

#### Landscape <a name="landscape"></a>
The landscape of the game will be within a castle.

### Interface <a name="interface"></a>
The game will present the name of a new area when you reach it.

### Audio System <a name="audio-system"></a>
The game will have background music playing, as well as a jump and landing sound or the player. A long fall may also have a different sound effect.

#### Game Music <a name="game-music"></a>
The music will be whimsical and give the feel for the player that they are in the castle.

#### Audio Look & Feel <a name="audio-look--feel"></a>
The idea of the game's music will have the player feel like they are part of this adventure upwards. The sound effects of the player will be there to feel like the player is actually moving though the world.

## Characters <a name="characters"></a>
### Main Character <a name="main-character"></a>
The main character is the Jump Queen.
