using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1_H
{
    class FactoryBuilding : Building
    {
        string unitType;
        int sponX;
        int sponY;

        public FactoryBuilding(int x, int y, string faction, string unitName) : base(x, y, 150, faction, '$', 5, "Factory")
        {
            unitType = unitName;

            sponX = x;
            if (y == 19)
            {
                sponY = y - 1;
            }
            else
            {
                sponY = y + 1;
            }
        }

        public override int X { get { return x; }  }
        public override int Y { get { return y; }  }
        public override int Health { get { return health; } set { health = value; } }
        public override int MaxHealth { get { return maxHealth; } }
        public override string Faction { get { return faction; } }
        public override char Symbol { get { return symbol; } }
        public override int Speed { get { return speed; } }
        public override string Name { get { return name; } }

        public override void Destroy()
        {
            symbol = '#';
        }

        public override string ToString()
        {
            return "Postion: " + x + "," + y + "\n" + "Health: " + health + " / " + maxHealth + "\n" + "Faction: " + faction + " (" + symbol + ")\n";
        }

        public Unit Spawn()
        {
            Unit tempU;


            if (unitType == "RangedUnit")//makes ranged unit
            {
                tempU = new RangedUnit(sponX, sponY, faction);
            }
            else
            {
                tempU = new MeleeUnit(sponX, sponY, faction);
            }

            return tempU;
        }
        public override Unit BuildingGen()
        {
            Unit u = Spawn();
            return u;

        }

        public override string Save()
        {
            string buildingSave = x + "," + y + "," + health + "," + faction + "," + '$';
            return buildingSave;
        }
    }
}
