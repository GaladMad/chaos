﻿using System.Collections;
using Chaos.Engine;

namespace Chaos.Model
{
    public class Spell : GameObject
    {
        public bool CanCastOnNothing { get; set; }
        public bool CanCastOnMonster { get; set; }
        public int EffectPower { get; set; }
        public string EffectLabel { get; set; }

        public Spell DeepCopy(Spell template)
        {
            Spell spell = new Spell();
            spell.CanCastOnMonster = template.CanCastOnMonster;
            spell.CanCastOnNothing = template.CanCastOnNothing;
            spell.EffectLabel = template.EffectLabel;
            spell.EffectPower = template.EffectPower;
            spell.Owner = template.Owner;
            spell.Sprite = template.Sprite;
            return spell;
        }
    }
}