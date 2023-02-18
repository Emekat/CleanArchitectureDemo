using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
   public class PlayerCharacter
    {
        public int Health { get; private set; } = 100;
        public int DamageResistance { get; set; }
        public string Race { get; set; }
        public bool isDead { get; private set; }


        public int MagicalPower
        {
            get { return MagicalItems.Sum(x => x.Power); }
        }

        public List<MagicalItem> MagicalItems { get; set; } = new List<MagicalItem>();
        public List<Weapon> Weapons { get; set; } = new List<Weapon>();
        public DateTime LastSleepTime { get; set; }
        public int WeaponsValue
        {
            get { return Weapons.Sum(x => x.Value); }
        }
        
       
        public CharacterClass CharacterClass { get; set; }
      
        public void Hit(int damage)
        {
            var raceDamageResistance = 0;
            if (Race == "Elf")
            {
                raceDamageResistance = 20;
            }

            var totalDamageTaken = Math.Max(damage - raceDamageResistance - DamageResistance, 0);
            Health -= damage;
            if (Health <= 0)
            {
                isDead = true;
            }
        }
        public void CastHealingSpell()
        {
            if (CharacterClass == CharacterClass.Healer)
            {
                Health = 100;
            }
            else
            {
                Health += 10;
            }

           
        }

        public void ReadHealthScroll()
        {
            var daysSinceLastSleep = DateTime.Now.Subtract(LastSleepTime).Days;
            if (daysSinceLastSleep <= 2)
            {
                Health = 100;
            }
          
        }

        public void UseMagicalItem(string itemName)
        {
            var powerReduction = 10;
            if (Race == "Elf")
            {
                powerReduction = 0;
            }

            var itemToReduce = MagicalItems.First(item => item.Name == itemName);
            itemToReduce.Power -= powerReduction;
        }
    }
}
