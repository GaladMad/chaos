﻿using Chaos.Engine;

namespace Chaos.Model
{
    public class Monster : GameObject
    {

        public Monster()
        {
        }

        public Monster(Player owner)
        {
            Owner = owner;
            Caption = Owner.Name + "'s " + Name;
        }

        public Monster MonsterFromTemplate(MonsterTemplate template, Player owner)
        {
            Monster monsterFromTemplate = new Monster();
            monsterFromTemplate.Sprite = template.sprite;
            monsterFromTemplate.Caption = owner.Name + " " + template.Name;
            monsterFromTemplate.canAttack = template.canAttack;
            monsterFromTemplate.isUndead = template.isUndead;
            monsterFromTemplate.Name = template.Name;
            monsterFromTemplate.Attack = template.Attack;
            monsterFromTemplate.Defense = template.Defense;
            monsterFromTemplate.Health = template.Health;
            monsterFromTemplate.MagicResistance = template.MagicResistance;
            monsterFromTemplate.Moves = template.Moves;
            monsterFromTemplate.MovesRemaining = monsterFromTemplate.Moves;

            return monsterFromTemplate;
        }

        public string Name { get; set; }

        public int Health { get; set; }

        public int Moves { get; set; }

        public int MovesRemaining { get; set; }

        public int MagicResistance { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public bool canAttack { get; set; }
        public bool isUndead { get; set; }

        #region Fields

        #endregion
    }
}