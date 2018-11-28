# Tower of Hanoi
The 'Tower of Hanoi' problem, made in Unity.

##### Content of this README:
* Overview of architecture
* Noteworthy in-game effects
* Tests and TDD
* ADR
* Image compression
* Photoshop

### Overview of architecture
As later mentioned in the TDD-section, the application is divided into two domains. Roughly, they are split by game logic and input handling.

Each domain has an event-type of its own:
* **InputEvents** are events emitted from the user. This is typically user interaction. The InputEvents triggers checks in the GameInterface which validates towards the rulesets, and then emits..
* **GameInterfaceEvents** are events that trigger behaviours for typically graphics, sound and logging.

The cool part about this, is that it's quite easy building in an event-system, or statistics collector, by just implementing the GameInterfaceEventListener interface and registering it in the editor!

### Noteworthy in-game effects
Seeing as this is event-driven, the corresponding in-game effects are handled by doing whatever-you-want in the graphics handler implementing the GameInterfaceEventListener. Some small but nifty features of this app is:
* A piece with other pieces on top won't be movable
* Dropping a piece outside of a dropzone returns the piece
* Dropping the piece on a dropzone with a smaller piece as the top piece, the current piece gets sad!

### Tests and TDD
Being a huge fan of TDD, I decided to test-drive this application. The first step I like doing is isolating domains - and for this application, I drew the line between the game user interface, and the ruleset for the game itself. I decided to start with the latter.

Starting off in true TDD fashion, the first part was driven from a failing end-to-end test for the TowerApplication. It quickly drove the need for the towers - and the resulting tower pieces. The unit tests were added as I went on.

With the rules in place, the same way was done for the game user interface. Tests where input would touch a piece, and something would happen.


Somewhere along the way, I realized that having the application event-driven made graphics and sound handling that much easier. Clear interfaces that define when to do what just fixed it all.

To run the tests, just open the TestRunner in Unity and enjoy the green-light show.

### ADR - Architecture Design Records
This is a nifty little thing to backtrack important crossroads in the design process. In this case, there was only one - and that was how I would allow the pieces to be placed on the pins.

### Image compression
The background image was quite big - and needed compression. After reducing the bit-depth and general compression we saved around 87% of the size. The .uncomp.png serves as a demonstration of this.

### Photoshop
Images were generated and altered in photoshop, and the particle system graphics were done by hand.

##### Disclaimers
I did not create the original art, other than the particles (smoke puffs and stars).