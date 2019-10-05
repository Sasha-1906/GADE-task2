using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1_H
{
    abstract class Unit
    {
        protected int x, y, health, maxHealth, speed, attack, attackRange;
        protected string faction;
        protected char symbol;
        protected bool isAttacking = false;
        protected bool isDestroyed = false;
        protected static Random rnd = new Random();
        protected string name;
        
        public Unit(int x, int y, int health, int speed, int attack, int attackRange, string faction, char symbol, string name)
        {
            this.x = x;
            this.y = y;
            this.health = health;
            maxHealth = health;
            this.speed = speed;
            this.attack = attack;
            this.attackRange = attackRange;
            this.faction = faction;
            this.symbol = symbol;
            this.name = name;
        }

        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int Health { get; set; }
        public abstract int MaxHealth { get; }
        public abstract string Faction { get; set; }
        public abstract char Symbol { get; set; }
        public abstract bool IsDestroyed { get; }
        public abstract string Name { get; }
        public abstract int Speed { get; }

        public abstract void Move(Unit closestUnit);
        public abstract void Attack(Unit otherUnit);
        public abstract void RunAway();
        public abstract bool IsInRange(Unit otherUnit);
        public abstract Unit GetClosestUnit(Unit[] units);
        public abstract void Destroy();

        protected double GetDistance(Unit otherUnit)
        {
            double xDistance = otherUnit.X - X;
            double yDistance = otherUnit.Y - Y;
            return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
        }

        public override string ToString()
        {
            return "Name: "+ name + "\n"+ "Postion: " + x + "," + y + "\n" + "Health: " + health + " / " + maxHealth + "\n" + "Faction: " + faction + " (" + symbol + ")\n";
        }

        public abstract string Save();
    }
}
