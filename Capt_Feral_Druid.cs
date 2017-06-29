using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZzukBot.Engines.CustomClass;
// Original work by Emu, Redesigned for intelligent resting by Capt Morgan. 
// Set Drink to 20%, Eat at 1%
// turn loot on
// Go feral spec like this: http://db.vanillagaming.org/?talent#0zLV0oZE0McfbdtV

namespace CaptDruid
{
    public class CaptDruid : CustomClass
    {

		public int lowHealthP = 25;  // Point where we start healing
		public int lowManaP = 25;  // Percentage of mana reserved for Healing/adds/runners/silences etc, but will effect casting of DPS offensive abilities

		public string[] drinkNames = {
			"Refreshing Spring Water", 
			"Ice Cold Milk",
			"Melon Juice", 
			"Moonberry Juice",
            "Sweet Nectar", 
			"Morning Glory Dew", 
			"Conjured Purified Water", 
			"Conjured Spring Water", 
			"Conjured Mineral Water", 
			"Conjured Sparkling Water", 
			"Conjured Crystal Water",
			"Conjured Fresh Water"
		};
        
		private bool Shifted()
        {
            return (this.Player.GotBuff("Cat Form") || this.Player.GotBuff("Bear Form")
                || this.Player.GotBuff("Dire Bear Form") || this.Player.GotBuff("Aquatic Form")
                || this.Player.GotBuff("Travel Form"));
        }
		
		public void curePoison()
		{
			if (this.Player.GetSpellRank("Abolish Poison") != 0 && !this.Player.GotBuff("Abolish Poison"))
			{
				if (this.Player.GotDebuff("Deadly Poison") || this.Player.GotDebuff("Venom Sting") || this.Player.GotDebuff("Slowing Poison"))
				{
					if (Shifted()) RemoveShift();
					//else if (this.Player.CanUse("Abolish Poison")) 
					this.Player.Cast("Abolish Poison");
				}
			}
		}
		
        private void Stealth()
        {
            if(this.Player.GetSpellRank("Prowl") != 0 
                && this.Player.GotBuff("Cat Form")
                && this.Player.CanUse("Prowl"))
            {
                this.Player.CastWait("Prowl", 500);
            }
        }

        private void RemoveShift()
        {
            //Lua is bad
            if (this.Player.GotBuff("Cat Form"))
            {
                this.Player.DoString("CastSpellByName('Cat Form')");
            }
            else if (this.Player.GotBuff("Bear Form"))
            {
                this.Player.DoString("CastSpellByName('Bear Form')");
            }
            else if (this.Player.GotBuff("Dire Bear Form"))
            {
                this.Player.DoString("CastSpellByName('Dire Bear Form')");
            }
            else if (this.Player.GotBuff("Aquatic Form"))
            {
                this.Player.DoString("CastSpellByName('Aquatic Form')");
            }
            else if (this.Player.GotBuff("Travel Form"))
            {
                this.Player.DoString("CastSpellByName('Travel Form')");
            }

        }

        private bool TargetCanBleed()
        {
            return (this.Target.CreatureType != CreatureType.Elemental && this.Target.CreatureType != CreatureType.Mechanical);
        }

        private bool CanShift()
        {
            return (this.Player.GetSpellRank("Cat Form") != 0 || this.Player.GetSpellRank("Bear Form") != 0);
        }
		
        public void SelectHPotion()
        {
			if (this.Player.ManaPercent <= lowManaP)
			{
				if (this.Player.HealthPercent <= lowHealthP && this.Player.ItemCount("Major Healing Potion") != 0)
					this.Player.UseItem("Major Healing Potion");
				else if (this.Player.HealthPercent <= lowHealthP && this.Player.ItemCount("Superior Healing Potion") != 0)
					this.Player.UseItem("Superior Healing Potion");
				else if (this.Player.HealthPercent <= lowHealthP && this.Player.ItemCount("Greater Healing Potion") != 0)
					this.Player.UseItem("Greater Healing Potion");
				else if (this.Player.HealthPercent <= lowHealthP && this.Player.ItemCount("Healing Potion") != 0)
					this.Player.UseItem("Healing Potion");
				else if (this.Player.HealthPercent <= lowHealthP && this.Player.ItemCount("Discolored Healing Potion") != 0)
					this.Player.UseItem("Discolored Healing Potion");
				else if (this.Player.HealthPercent <= lowHealthP && this.Player.ItemCount("Lesser Healing Potion") != 0)
					this.Player.UseItem("Lesser Healing Potion");
				else if (this.Player.HealthPercent <= lowHealthP && this.Player.ItemCount("Minor Healing Potion") != 0)
					this.Player.UseItem("Minor Healing Potion");
			}
        }
		
        public void SelectDrink()
        {
            if (this.Player.ItemCount("Morning Glory Dew") != 0)
                this.Player.Drink(drinkNames[5]);
            else if (this.Player.ItemCount("Sweet Nectar") != 0)
                this.Player.Drink(drinkNames[4]);
            else if (this.Player.ItemCount("Moonberry Juice") != 0)
                this.Player.Drink(drinkNames[3]);
            else if (this.Player.ItemCount("Melon Juice") != 0)
                this.Player.Drink(drinkNames[2]);
            else if (this.Player.ItemCount("Ice Cold Milk") != 0)
                this.Player.Drink(drinkNames[1]);
            else if (this.Player.ItemCount("Refreshing Spring Water") != 0)
                this.Player.Drink(drinkNames[0]);
            else if (this.Player.ItemCount("Conjured Purified Water") != 0)
                this.Player.Drink(drinkNames[6]);
            else if (this.Player.ItemCount("Conjured Spring Water") != 0)
                this.Player.Drink(drinkNames[7]);
            else if (this.Player.ItemCount("Conjured Mineral Water") != 0)
                this.Player.Drink(drinkNames[8]);
            else if (this.Player.ItemCount("Conjured Sparkling Water") != 0)
                this.Player.Drink(drinkNames[9]);
            else if (this.Player.ItemCount("Conjured Crystal Water") != 0)
                this.Player.Drink(drinkNames[10]);
			else if (this.Player.ItemCount("Conjured Fresh Water") != 0)
                this.Player.Drink(drinkNames[11]);
        }

        public void SelectMPotion()
        {
			if (this.Player.HealthPercent <= lowHealthP)
			{
				if (this.Player.ManaPercent <= lowManaP && this.Player.ItemCount("Major Mana Potion") != 0)
					this.Player.UseItem("Major Mana Potion");
				else if (this.Player.ManaPercent <= lowManaP && this.Player.ItemCount("Superior Mana Potion") != 0)
					this.Player.UseItem("Superior Mana Potion");
				else if (this.Player.ManaPercent <= lowManaP && this.Player.ItemCount("Greater Mana Potion") != 0)
					this.Player.UseItem("Greater Mana Potion");
				else if (this.Player.ManaPercent <= lowManaP && this.Player.ItemCount("Mana Potion") != 0)
					this.Player.UseItem("Mana Potion");
				else if (this.Player.ManaPercent <= lowManaP && this.Player.ItemCount("Lesser Mana Potion") != 0)
					this.Player.UseItem("Lesser Mana Potion");
				else if (this.Player.ManaPercent <= lowManaP && this.Player.ItemCount("Minor Healing Potion") != 0)
					this.Player.UseItem("Minor Mana Potion");
			}
        }
		
        private void Shift()
        {
            if (!Shifted())
			{
				if (this.Player.GetSpellRank("Cat Form") != 0)
				{
					this.Player.Cast("Cat Form");
				}
				else if (this.Player.GetSpellRank("Bear Form") != 0)
				{
					this.Player.Cast("Bear Form");
				}
			}
        }

        private bool ShouldWeBite()
        {
			if (this.Player.GetSpellRank("Ferocious Bite") != 0 && this.Target.HealthPercent <= 50 && this.Player.ComboPoints >= 5) return true;
			else if (this.Player.GetSpellRank("Ferocious Bite") != 0 && this.Target.HealthPercent <= 40 && this.Player.ComboPoints >= 4) return true;
			else if (this.Player.GetSpellRank("Ferocious Bite") != 0 && this.Target.HealthPercent <= 30 && this.Player.ComboPoints >= 3) return true;
			else if (this.Player.GetSpellRank("Ferocious Bite") != 0 && this.Target.HealthPercent <= 20 && this.Player.ComboPoints >= 2) return true;
			else if (this.Player.GetSpellRank("Ferocious Bite") != 0 && this.Target.HealthPercent <= 10 && this.Player.ComboPoints >= 1) return true;
            else return false;
        }

        private bool ShouldWeRip()
        {
			if (TargetCanBleed() && this.Player.GetSpellRank("Rip") != 0 && this.Target.HealthPercent > 60 && this.Player.ComboPoints >=3) return true;
            else return false;
        }
		
        public override byte DesignedForClass
        {
            get
            {
                return PlayerClass.Druid;
            }
        }

        public override string CustomClassName
        {
            get
            {
                return "CaptDruid";
            }
        }

        public override void PreFight()
        {
			this.Player.Attack();
            if (CanShift())
            {
                if (!Shifted())
                {
                    if (this.Player.GetSpellRank("Faerie Fire") != 0 && this.Player.CanUse("Faerie Fire") && !this.Target.GotDebuff("Faerie Fire") && this.Target.DistanceToPlayer <= 30)
                    {
						this.Player.Cast("Faerie Fire");
                    }
                    else if (this.Player.GetSpellRank("Moonfire") != 0 && this.Player.CanUse("Moonfire") && !this.Target.GotDebuff("Moonfire") && this.Target.DistanceToPlayer <= 30)
                    {
                        this.Player.Cast("Moonfire");
                    }
                }
                else
                {
                    if (this.Player.GetSpellRank("Faerie Fire (Feral)") != 0 &&  this.Player.CanUse("Faerie Fire (Feral)") && !this.Target.GotDebuff("Faerie Fire (Feral)") && this.Target.DistanceToPlayer <= 30)
                    {
						this.Player.DoString("CastSpellByName('Faerie Fire (Feral)()');");
						return;
                    }
                }
            }
            else
            {
                this.SetCombatDistance(30);
				this.Player.Attack();
				
                if (this.Player.GetSpellRank("Moonfire") != 0 && this.Player.CanUse("Moonfire") && !this.Target.GotDebuff("Moonfire") && this.Target.DistanceToPlayer <= 30)
                {
                    this.Player.Cast("Moonfire");
                }
                else if (this.Target.DistanceToPlayer <= 30 && this.Player.ManaPercent >= lowManaP)
                {
                    this.Player.Cast("Wrath");
                }
				else 
				{
					this.SetCombatDistance(4);
				}
            }
        }

        public override void Fight()
        {
            this.Player.Attack();
			if (this.Player.GotDebuff("Dazed")) this.Player.Backup(5);
            //Shift in/out to heal /dd
            if (Shifted())
            {
                this.Player.Attack();
				this.SetCombatDistance(5);
                if (this.Player.HealthPercent < lowHealthP && this.Player.ManaPercent >= lowManaP && !this.Player.GotBuff("Regrowth"))
                {
                    RemoveShift();
                    return;
                }
                else if (this.Player.GotBuff("Cat Form"))
                {
                    
                    if (this.Player.GetSpellRank("Faerie Fire (Feral)") != 0 && this.Player.CanUse("Faerie Fire (Feral)") && TargetCanBleed() && !this.Target.GotDebuff("Faerie Fire (Feral)"))
                    {
						this.Player.DoString("CastSpellByName('Faerie Fire (Feral)()');");
                        return;
                    }
					if (this.Player.GetSpellRank("Tiger's Fury") != 0 && this.Player.Energy >= 80 && !this.Player.GotBuff("Tiger's Fury"))
					{
						this.Player.Cast("Tiger's Fury");
						return;
					}
					else if (ShouldWeBite() && this.Player.Energy >= 35)
					{
						this.Player.Cast("Ferocious Bite");
					}
					else if (!this.Target.GotDebuff("Rip") && ShouldWeRip() && this.Player.Energy >= 30)
					{
						this.Player.Cast("Rip");
					}
					else if (this.Player.GetSpellRank("Rake") != 0 && TargetCanBleed() && this.Player.Energy >= 35 && this.Target.HealthPercent >= 15 && !this.Target.GotDebuff("Rake"))
					{
						this.Player.Cast("Rake");
					}
					else if (this.Player.Energy >= 40) this.Player.Cast("Claw");
				}
			
                
                else if (this.Player.GotBuff("Dire Bear Form"))
                {
                    if (this.Player.GetSpellRank("Bash") != 0 && this.Player.Rage >= 10 && this.Player.CanUse("Bash"))
                    {
                        this.Player.Cast("Bash");
                        return;
                    }
                    else if (this.Player.GetSpellRank("Maul") != 0 && this.Player.Rage >= 15 && this.Player.CanUse("Maul"))
                    {
						this.Player.Cast("Maul");
                        return;
                    }
                    //Player.Rage dump
                    if (this.Player.GetSpellRank("Swipe") != 0)
                    {
                        if (this.Player.Rage >= 40 && this.Player.CanUse("Swipe"))
                        {
                            this.Player.Cast("Swipe");
                            return;
                        }
                    }
                }
            }
            else
            {
				
				SelectHPotion();
				SelectMPotion();
				
                if (this.Player.GetSpellRank("Innervate") != 0 && this.Player.CanUse("Innervate") && this.Player.ManaPercent <= 30)
                {
                    this.Player.Cast("Innervate");
                }
                else if (this.Player.HealthPercent <= lowHealthP && this.Player.CanUse("Regrowth") && !this.Player.GotBuff("Regrowth"))
                {
                    if (this.Player.CanUse("War Stomp")) this.Player.Cast("War Stomp");
					this.Player.Cast("Regrowth");
                    return;
                }											
                else if (this.Player.GetSpellRank("Cat Form") != 0 || this.Player.GetSpellRank("Bear Form") != 0)
                {
                    Shift();
                }
                else if (this.Player.ManaPercent >= lowManaP && this.Player.GetSpellRank("Moonfire") != 0 && !this.Target.GotDebuff("Moonfire"))
				{
					this.SetCombatDistance(30);
					this.Player.Cast("Moonfire");
					return;
				}
				else if (this.Player.ManaPercent >= lowManaP)
				{
					this.SetCombatDistance(30);
					this.Player.Cast("Wrath");
				}
				else
				{
					//Melee Them
					this.SetCombatDistance(4);
				}
            }
        }
    
        public override void Rest()
        {
			// GCD Check
			if (!this.Player.CanUse("Healing Touch")) return;
			if (Shifted())
			{
				RemoveShift();
				return;
			}
			else
			{					
				this.Player.DoString("DoEmote('Sit')");
				//this.Player.Drink();
				SelectDrink();
			}
        }

        public override bool Buff() // Intelligent Healing and looting
        {	
			curePoison();
			if ((this.Player.IsCasting == "Healing Touch" || this.Player.IsCasting == "Regrowth") && this.Player.HealthPercent < 75)
			{
                return false;
			}	
			else if (this.Player.HealthPercent < 30 && this.Player.CanUse("Healing Touch"))
            {
				if (Shifted())
                {
                    RemoveShift();
                    return false;
                }
                this.Player.Cast("Healing Touch");
				return false;
            }
			else if (this.Player.HealthPercent < 60 && this.Player.CanUse("Regrowth") && !this.Player.GotBuff("Regrowth"))
            {
				if (Shifted())
                {
                    RemoveShift();
                    return false;
                }
                this.Player.Cast("Regrowth");
				return false;
            }
			//else if (Player.NeedToLoot()) return true;
			else if (this.Player.HealthPercent < 80 && this.Player.GetSpellRank("Rejuvenation") != 0 && !this.Player.GotBuff("Rejuvenation"))
            {
				if (Shifted())
                {
                    RemoveShift();
                    return true;
                }
                this.Player.Cast("Rejuvenation");
				return true;
			}
			else if (this.Player.GetSpellRank("Innervate") != 0 && this.Player.CanUse("Innervate") && this.Player.ManaPercent <= 45 && !Shifted())
			{
				this.Player.Cast("Innervate");
				return false;
			}
			else if (this.Player.GetSpellRank("Thorns") != 0 && !this.Player.GotBuff("Thorns") && !Shifted())
            {
				this.Player.Cast("Thorns");
				return false;
            }
			else if (this.Player.GetSpellRank("Omen of Clarity") != 0 && !this.Player.GotBuff("Omen of Clarity") && !Shifted())
            {
				this.Player.Cast("Omen of Clarity");
				return false;
            }			
            else if (this.Player.GetSpellRank("Mark of the Wild") != 0 && !this.Player.GotBuff("Mark of the Wild") && !Shifted())
            {
				this.Player.Cast("Mark of the Wild");
				return false;
            }
			else if (!Shifted() && this.Player.ManaPercent > 85 && this.Player.GetSpellRank("Rejuvenation") != 0 && !this.Player.GotBuff("Rejuvenation"))
			{
				this.Player.Cast("Rejuvenation");
				return true;
			}
			/*else if (!Shifted() && this.Player.ItemCount("Elixir of Greater Agility") > 0 && !this.Player.GotBuff("Greater Agility")) 
			{
				this.Player.UseItem("Elixir of Greater Agility");
				return false;
			}*/
			else if (!Shifted() && this.Player.ManaPercent > 50 && this.Player.HealthPercent > 80 && (this.Player.GetSpellRank("Cat Form") != 0 || this.Player.GetSpellRank("Bear Form") != 0))
			{
				Shift();
				return false;
			}
			else if (this.Player.GotBuff("Cat Form") && this.Player.GetSpellRank("Dash") != 0 && this.Player.CanUse("Dash"))
			{
				this.Player.Cast("Dash");
				return false;
			}
            else return true;
        }

    }
}
