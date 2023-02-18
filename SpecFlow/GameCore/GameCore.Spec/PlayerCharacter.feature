Feature: PlayerCharacter
	In order to Play the game
	As a human player
	I want my character properties to be correctly represented

Background: 
 Given I'm a new player

Scenario: Taking no damage when hit doesnt affect health
	
	When I take 0 damage
	Then My health should now be 100

Scenario: Starting health is reduced when hit
	
	When I take 40 damage
	Then My health should now be 60


	@elf
Scenario: Elf characters gett additional 20 damage resistance using Table	
	And  I have the following attributes
	| attribute  | value |
	| Race       | Elf   |
	| Resistance | 10    |
	When I take 40 damage
	Then My health should now be 60


Scenario: Healers restore all Health
	Given My character class is set to Healer
	When I take 40 damage
	 And Cast a healing spell
	Then My health should now be 100

Scenario: Total Magic power
	Given I have the magical items
	| name   | value | power |
	| Ring   | 200   | 100   |
	| Amul   | 400   | 200   |
	| Gloves | 100   | 400   |
Then my total magical power should be 700

Scenario: Reading a restore health scroll when overtired has no effect
 Given I last slept 3 days ago
 When I take 40 damage
 And I read a restore health scroll
 Then My health should now be 60

Scenario: Weapons are worth money
 Given I have the following weapons
	| name   | value|
	| Sword  | 50   |
	| Pick   | 40   |
	| Knife  | 10   |
 Then My weapons should be worth 100

 @elf
Scenario: Elf race characters dont loose magical item power
Given I'm an Elf
And I have an Amulet with a power of 200
When I use a magical Amulet
Then The Amulet power should not be reduced



