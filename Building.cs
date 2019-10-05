using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1_H
{
    abstract class Building
    {
        protected int x, y, health, maxHealth, speed;
        protected string faction, name;
        protected char symbol;
        

        public abstract void Destroy();
        public abstract override string ToString();
        public abstract Unit BuildingGen();
        public abstract string Save();


        public Building(int x, int y, int health, string faction, char symbol, int speed, string name)
        {
            this.x = x;
            this.y = y;
            this.health = health;
            maxHealth = health;
            this.faction = faction;
            this.symbol = symbol;
            this.speed = speed;
            this.name = name;
        }

        public abstract int X { get; }
        public abstract int Y { get; }
        public abstract int Health { get; set; }
        public abstract int MaxHealth { get; }
        public abstract string Faction { get; }
        public abstract char Symbol { get; }
        public abstract int Speed { get; }
        public abstract string Name { get; }
    }
}
