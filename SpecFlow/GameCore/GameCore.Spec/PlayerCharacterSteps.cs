using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GameCore.Spec
{
    [Binding]
    public class PlayerCharacterSteps
    {
        private readonly PlayerCharacterStepsContext _context;

        public PlayerCharacterSteps(PlayerCharacterStepsContext context)
        {

            _context = context;
        }

       

        [When(@"I take (.*) damage")]
        [Scope(Tag="elf")]
        public void WhenITakeDamage(int damage)
        {
            _context.Player.Hit(damage);
        }

        [When(@"I take (.*) damage")]
        public void WhenITakeDamageAsAnElf(int damage)
        {
            _context.Player.Hit(damage);
        }

        [Then(@"My health should now be (.*)")]
        public void ThenMyHealthShouldNowBe(int expectedHealth)
        {
           Assert.AreEqual(expectedHealth, _context.Player.Health);
        }
        [Given(@"I have the following attributes")]
        public void GivenIHaveTheFollowingAttributes(Table table)
        {
            //var race = table.Rows.First(row => row["attribute"] == "Race")["value"];
            //var resistance = table.Rows.First(row => row["attribute"] == "Resistance")["value"];

            // var attributes = table.CreateInstance<PlayerAttributes>();

            dynamic attributes = table.CreateDynamicInstance();


            _context.Player.Race = attributes.Race;
            _context.Player.DamageResistance = attributes.Resistance;
        }

        [Given(@"I'm an Elf")]
        public void GivenImAnElf()
        {
            _context.Player.Race =  "Elf";
        }


        [Given(@"My character class is set to (.*)")]
        public void GivenMyCharacterClassIsSetToHealer(CharacterClass characterClass)
        {
            _context.Player.CharacterClass = characterClass;
        }

        [When(@"Cast a healing spell")]
        public void WhenCastAHealingSpell()
        {
            _context.Player.CastHealingSpell();
        }

        [Given(@"I have the magical items")]
        public void GivenIHaveTheFollowingMagicalItems(Table table)
        {
            //foreach (var row in table.Rows)
            //{
            //    var name = row["item"];
            //    var value = row["value"];
            //    var power = row["power"];

            //    _playerCharacter.MagicalItems.Add(new MagicalItem
            //    {
            //        Name = name,
            //        Power = int.Parse(power),
            //        Value = int.Parse(value)

            //    });
            //}

            //IEnumerable<MagicalItem> items = table.CreateSet<MagicalItem>();
            //_playerCharacter.MagicalItems.AddRange(items);

            IEnumerable<dynamic> items = table.CreateDynamicSet();

            foreach (var magicalItem in items)
            {

                _context.Player.MagicalItems.Add(new MagicalItem
                {
                    Name = magicalItem.name,
                    Power = magicalItem.power,
                    Value = magicalItem.value

                });
            }
        }

        [Then(@"my total magical power should be (.*)")]
        public void ThenMyTotalMagicalPowerShouldBe(int totalMagicalPower)
        {
            Assert.AreEqual(totalMagicalPower, _context.Player.MagicalPower);
        }

        [Given(@"I last slept (.* days ago)")]
        public void GivenILastSleptDaysAgo(DateTime lastSlept)
        {
            _context.Player.LastSleepTime = lastSlept;
        }

        [When(@"I read a restore health scroll")]
        public void WhenIReadARestoreHealthScroll()
        {
            _context.Player.ReadHealthScroll();
        }

        [Given(@"I have the following weapons")]
        public void GivenIHaveTheFollowingWeapons(IEnumerable<Weapon> weapons)
        {
            _context.Player.Weapons.AddRange(weapons);
        }

        [Then(@"My weapons should be worth (.*)")]
        public void ThenMyWeaponsShouldBeWorth(int value)
        {
            Assert.AreEqual(value, _context.Player.WeaponsValue);
        }

        [Given(@"I have an Amulet with a power of (.*)")]
        public void GivenIHaveAnAmuletWithAPowerOf(int power)
        {
            _context.Player.MagicalItems.Add(
                new MagicalItem
                {
                    Name = "Amulet",
                    Power = power
                });

            _context.StartingMagicalPower = power;
        }

        [When(@"I use a magical Amulet")]
        public void WhenIUseAMagicalAmulet()
        {
            _context.Player.UseMagicalItem("Amulet");
        }

        [Then(@"The Amulet power should not be reduced")]
        public void ThenTheAmuletPowerShouldNotBeReduced()
        {
            int expectedPower = _context.StartingMagicalPower;
            Assert.AreEqual(expectedPower, _context.Player.MagicalItems.First(item => item.Name == "Amulet").Power);
        }


    }
}
