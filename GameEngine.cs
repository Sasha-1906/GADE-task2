using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_1_H
{
    class GameEngine
    {
        Map maparoo;
        bool isGameOver = false;
        string winningFaction = "";
        int round = 0;

        public GameEngine()
        {
            maparoo = new Map(30, 10);
        }

        public bool IsGameOver
        {
            get { return isGameOver; }
        }

        public string WinningFaction
        {
            get { return winningFaction; }
        }

        public int Round
        {
            get { return round; }
        }

        public string GetMapDisplay()
        {
            return maparoo.GetMapDisplay();
        }

        public string GetUnitInfo()
        {
            string unitInfo = "";
            for (int i = 0; i < maparoo.Units.Length; i++)
            {
                unitInfo += maparoo.Units[i] + "\n";
            }

            for (int i = 0; i < maparoo.Buildings.Length; i++)
            {
                unitInfo += maparoo.Buildings[i] + "\n";
            }

            return unitInfo;
        }

        public void Reset()
        {
            maparoo.Reset();
            isGameOver = false;
            round = 0;
        }

        public void GameLoop()
        {
            for (int i = 0; i < maparoo.Units.Length; i++)//runs through every unit
            {
                if (maparoo.Units[i].IsDestroyed || round%maparoo.units[i].Speed != 0)
                {
                    continue;
                }

                Unit closestUnit = maparoo.Units[i].GetClosestUnit(maparoo.Units);
                if (closestUnit == null)
                {
                    isGameOver = true;
                    winningFaction = maparoo.Units[i].Faction;
                    maparoo.UpdateMap();
                    return;
                }

                double healthPercentage = maparoo.Units[i].Health / maparoo.Units[i].MaxHealth;
                if (healthPercentage <= 0.25)
                {
                    maparoo.Units[i].RunAway();
                }
                else if (maparoo.Units[i].IsInRange(closestUnit))
                {
                    maparoo.Units[i].Attack(closestUnit);
                }
                else
                {
                    maparoo.Units[i].Move(closestUnit);
                }
                StayInBounds(maparoo.Units[i], maparoo.Size);
            }

            for (int i = 0; i < maparoo.Buildings.Length; i++)
            {
                if (round%maparoo.Buildings[i].Speed == 0)//logic for what buildings do
                {
                    Unit u = maparoo.Buildings[i].BuildingGen();//calls if it is its turn

                    if (u != null)
                    {
                        Array.Resize(ref maparoo.units, maparoo.Units.Length + 1);
                        maparoo.units[maparoo.Units.Length - 1] = u;
                    }
                }
            }

            maparoo.UpdateMap();
            round++;
        }

        private void StayInBounds(Unit unit, int size)//moves units that moved off te map back onto the map
        {
            if (unit.X < 0)//if out of the map on the left then move to the first left collum
            {
                unit.X = 0;
            }
            else if (unit.X >= size)// on the right
            {
                unit.X = size - 1;
            }

            if (unit.Y < 0)// to the top
            {
                unit.Y = 0;
            }
            else if (unit.Y >= size)//to the bottom
            {
                unit.Y = size - 1;
            }
        }

        public void SaveUnits()
        {
            FileStream outFile = new FileStream("units.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);

            for (int i = 0; i < maparoo.Units.Length; i++)
            {
                writer.WriteLine(maparoo.Units[i].Save());
            }
            writer.Close();
            outFile.Close();
        }

        public void SaveBuildings()
        {
            FileStream outFile = new FileStream("buildings.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);

            for (int i = 0; i < maparoo.Buildings.Length; i++)
            {
                writer.WriteLine(maparoo.Buildings[i].Save());
            }
            writer.Close();
            outFile.Close();
        }

        public void LoadGame()
        {
            maparoo.LoadBuilding();
            maparoo.LoadUnits();
        }
    }
}
