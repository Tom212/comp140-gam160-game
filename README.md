# comp140-gam160-game
Repository for Assignment 1 of COMP140-GAM160


# TOM DANIELS - ALT CONTROLLER GAME PROPOSAL
  
## Part A
  
  For my alt controller assessment I plan to develop a wave defence game where the player controls a tank which must defend an objective from waves of enemy forces. Aside from the core mechanics of the game it's important to note the added element of an alternative controller and its effect on gameplay. The player controls their vehicle using a bespoke, alternative controller. Using two wheels, they may incrementally control the slew and elevation of their turret. Having to manage these separately shall make target acquisition tactile but difficult. Movement also utilizes the controller with two levers controlling the forward movement of the left and right tank tracks. 

Aside from these points the format of gameplay is fairly simple, with the challenge and depth mostly stemming from the operation of the tank. Within the game there are enemy tanks, the player’s tank, a staging area for the spawning of enemies and a HQ zone which belongs to the player.

The player starts the game within their HQ, a zone which they must defend. They can move freely around the level. At game start enemies begin to spawn at the staging area, these are enemy tanks which move along a guided path to the enemy HQ. The enemies spawn in waves, their amount and delay are variable to difficulty. During movement they will attempt to fire at the player if they see them with limited accuracy.

The player has limited health points which are deducted when struck by enemy fire. The HQ also has health points which are lost when an enemy enters the HQ zone. If either of these health points reach zero the player has lost. To win the player must eliminate all waves of enemy tanks. This describes the only gamemode in the game, Defence. Before starting a game the player may choose from one of four difficulties, these affect the number of enemies in each wave, the rate at which waves arrive and finally the move speed of the enemies. 

Besides managing the health of their tank and HQ the player must also collect ammunition from around the map, starting with a small amount which they expend each time their fire. This will force the player to navigate the map.

The movement of the player's tank is difficult to manage, when combined with the tasks of managing ammunition and defending their base it shall produce a challenging experience. Two large wheels control the movement of the turret, one will raise/lower the barrel of the tank. The other will rotate the turret when turned. Two levers control movement, when the left is in full forward the tank shall rotate left. The same applies for the right lever which will turn the tank right. When both are in the forward position the tank moves straight forward. 

Visually the game is 3D with minimalistic graphics for a clean look. The player’s tank is controlled from a 3rd person view point which is held behind the tank. There will be a control that will allow the player to switch to a gunview camera which is from the perspective of the barrel of the tank. 

Aside from these points of gameplay the application shall have a main menu for managing the selection of difficulty and starting a game as well as the option to quit. There will also be a pause menu available to the player during gameplay.

## Part B

My controller shall consist of four main pieces. Two of these shall be wheels, each will control an axis of movement for the player’s turret. One incrementing the elevation of the turret’s weapon up and down while another rotates the body of the turret changing the direction it faces. Besides these wheels there will be two levers, each control throttle which affects movement. Separately they apply forward movement to one track of the vehicle, tracks positioned on the left or right. The levers will have a range of motion from a vertical position to about 20 degrees forward and back. When fully forward the associated track will receive 100% throttle, when at full rear position it will supply 0% throttle. I would like to implement reverse throttle at full rear, with 0% being at a center position instead, but this shall be a stretch goal. The effect produced by this input will be that one lever forward and one back will turn the vehicle in the opposite direction of the forward lever, while both forward will create straight travel forwards. 

The design of the controller shall be as follows. All pieces will be fitted to a rectangular board intended to be placed on a desk in front of a player, its length running horizontally to the player. Estimated dimensions of this board would be 50x20cm. Situated in the center of the board would be the two wheels which control the turret. They would be facing upwards, sat atop two square boxes which house their mechanisms. These would be sat side by side, equally offset from the center of the board. To the ends of the boards will be the two throttle levers which stand vertically with their axis moving forward and back from the player. These mechanisms will be attached to an arduino which is also based on the board keeping everything centralized and easily transported. 

To achieve functionality for the two wheels I’ve taken inspiration from two examples I’ve found of similar wheel based controllers in my research. Referenced below, both examples I found used potentiometers to measure input. To implement these components the wheel structures which will be made of plastic will be connected to a rod, likely made from a pipe. These will then extend and connect to potentiometers which will be housed inside wooden or cardboard boxes. They will be fixed to the bottom of the boxes, the rods extending from the top of each box through holes down to the potentiometers. The wheel will then sit atop of the box housing thus being accessible to the player who can then turn them changing received input at the potentiometers. It will be important that movement of the wheel is free to rotate 360 degrees and not be limited between two rotations.

In similar fashion the two levers will be plastic joystick style poles which will bend forward and backwards, I will need to experiment with ways of creating the desired arc like movement as I cannot find examples of this being done in this context. I believe that I may be able to use a potentiometer with a slider element to track the forward and back movement of the levers. It will be important that the game is able to track the controller’s exact position within its range of forward and back as its input needs to be directly fixed to its physical position. This isn’t important for the wheels as they will simply increment turret rotation.

### Bibliography/Research Materials

Inventor, D. (no date) DIY homemade steering wheel USB PC with Viberation. Available at: http://www.instructables.com/id/DIY-Homemade-Steering-Wheel-USB-PC-with-Viberation/?ALLSTEPS (Accessed: 1 March 2017).

Willekens, P. and Marsman, M. (2015) Blind car simulator. Available at: http://shakethatbutton.com/blind-car-simulator/ (Accessed: 1 March 2017).


