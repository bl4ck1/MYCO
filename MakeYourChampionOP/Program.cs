﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace MakeYourChampionOP
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        public static Menu Config;

        private static void OnLoad(EventArgs args)
        {
            Config = new Menu("MAKE YOUR CHAMPION OP", "MAKE YOUR CHAMPION OP", true);
            {
                Config.AddItem(new MenuItem("inject", "Inject ?")).SetValue(false);
                Config.AddItem(new MenuItem("trigger.azir", "Trigger Azir Q Every Solider Auto Attack")).SetValue(false);
            }

            Config.AddToMainMenu();

            Game.PrintChat("If you wanna inject. Just set Inject to on and press f5. enjoy your op champion");
            Game.PrintChat("#FREEKARL");

            Hacks.UnsafeCasting = Config.Item("inject").GetValue<bool>();
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
        }


        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            if (ObjectManager.Player.ChampionName == "Lucian" && args.SData.Name.ToLower().Contains("attack")
                && Config.Item("inject").GetValue<bool>())
            {
                ObjectManager.Player.Spellbook.CastSpell(SpellSlot.E, Game.CursorPos, true);
            }

            if (ObjectManager.Player.ChampionName == "Azir")
            {
                if (Config.Item("inject").GetValue<bool>())
                {
                    if (Config.Item("trigger.azir").GetValue<bool>())//
                    {
                        if (args.SData.Name == ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).SData.Name
                            || args.SData.Name.ToLower().Contains("attack"))
                        {
                            var enemy = TargetSelector.GetTarget(825, TargetSelector.DamageType.Magical);
                            if (enemy != null)
                            {
                                ObjectManager.Player.Spellbook.CastSpell(SpellSlot.Q, enemy.Position, true);
                            }
                        }

                    }
                    else
                    {
                        if (args.SData.Name == ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).SData.Name)
                        {
                            var enemy = TargetSelector.GetTarget(825, TargetSelector.DamageType.Magical);
                            if (enemy != null)
                            {
                                ObjectManager.Player.Spellbook.CastSpell(SpellSlot.Q, enemy.Position, true);
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}

