using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_1_H
{
    class Map
    {
        int mapSize = 20;
        Random rnd = new Random();
        int numUnits, numBuildings;
        public Unit[] units;
        Building[] buildings;
        string[,] maparoo;
        string[] factions = { "Blue-T", "Orange-T" };

        public Map(int numUnits, int numBuildings)
        {
            this.numUnits = numUnits;
            this.numBuildings = numBuildings;
            Reset();
        }

        public Unit[] Units
        {
            get { return units; }
        }

        public Building[] Buildings
        {
            get { return buildings; }
        }

        public int Size
        {
            get { return mapSize; }
        }

        public string GetMapDisplay()
        {
            string mapString = "";
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    mapString += maparoo[x, y];
                }
                mapString += "\n";
            }
            return mapString;
        }

        public void Reset()
        {
            maparoo = new string[mapSize, mapSize];
            units = new Unit[numUnits];
            buildings = new Building[numBuildings];
            InitializeUnits();
            UpdateMap();
        }

        public void UpdateMap()
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    maparoo[x, y] = "~~~";//populates map with ~
                }
            }

            for (int i = 0; i < units.Length; i++)//runs through unit array and changes ~ to a unit
            {
                maparoo[units[i].X, units[i].Y] = units[i].Faction[0] + "/" + units[i].Symbol;
            }

            for (int i = 0; i < buildings.Length; i++)
            {
                maparoo[buildings[i].X, buildings[i].Y] = buildings[i].Faction[0] + "/" + buildings[i].Symbol;
            }
        }

        private void InitializeUnits()//makes random units
        {
            for (int i = 0; i < units.Length; i++)
            {
                int x = rnd.Next(0, mapSize);
                int y = rnd.Next(0, mapSize);
                int factionIndex = rnd.Next(0, 2);
                int unitType = rnd.Next(0, 2);

                while (maparoo[x, y] != null)
                {
                    x = rnd.Next(0, mapSize);
                    y = rnd.Next(0, mapSize);
                }

                if (unitType == 0)
                {
                    units[i] = new MeleeUnit(x, y, factions[factionIndex]);
                }
                else
                {
                    units[i] = new RangedUnit(x, y, factions[factionIndex]);
                }
                maparoo[x, y] = units[i].Faction[0] + "/" + units[i].Symbol;
            }

            for (int i = 0; i < buildings.Length; i++)
            {
                int x = rnd.Next(0, mapSize);
                int y = rnd.Next(0, mapSize);
                int factionIndex = rnd.Next(0, 2);
                int unitType = rnd.Next(0, 2);
                

                while (maparoo[x, y] != null)
                {
                    x = rnd.Next(0, mapSize);
                    y = rnd.Next(0, mapSize);
                }

                if (unitType == 0)//construct factory building 
                {
                    string mOrR;
                    int rndMR = rnd.Next(0, 2);

                    if (rndMR == 0)
                    {
                        mOrR = "RangedUnit";
                    }
                    else
                    {
                        mOrR = "MeleeUnit";
                    }

                    buildings[i] = new FactoryBuilding(x, y, factions[factionIndex], mOrR);
                }
                else//construct resource buildings
                {
                    buildings[i] = new ResourceBuilding(x, y, factions[factionIndex]);
                }
                maparoo[x, y] = buildings[i].Faction[0] + "/" + buildings[i].Symbol;
            }
        }

       public void LoadUnits()
       {
            FileStream file = new FileStream("units.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);

            string unitInfo;//reads first line
            string[] infoField;
            unitInfo = reader.ReadLine();
            units = new Unit[0];

            while(unitInfo !=null)
            {
                Array.Resize(ref units, units.Length + 1);
                infoField = unitInfo.Split(',');

                if (infoField [4] == Convert.ToString('R'))
                {
                    units[units.Length - 1] = new RangedUnit(0,0, "");
                }
                else
                {
                    units[units.Length - 1] = new MeleeUnit(0, 0, "");
                }
                units[units.Length - 1].X = Convert.ToInt32(infoField[0]);
                units[units.Length - 1].Y = Convert.ToInt32(infoField[1]);
                units[units.Length - 1].Health = Convert.ToInt32(infoField[2]);
                units[units.Length - 1].Faction = (infoField[3]);
                units[units.Length - 1].Symbol = Convert.ToChar(infoField[4]);
                unitInfo = reader.ReadLine();

            }
            reader.Close();
            file.Close();
            UpdateMap();
       }


        public void LoadBuilding()
        {
            FileStream file = new FileStream("buildings.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);

            string buildingInfo;//reads first line
            string[] infoField;
            buildingInfo = reader.ReadLine();
            buildings = new Building[0];

            while (buildingInfo != null)
            {
                Array.Resize(ref buildings, buildings.Length + 1);
                infoField = buildingInfo.Split(',');

                if (infoField[4] == "$")
                {
                    buildings[buildings.Length - 1] = new FactoryBuilding(Convert.ToInt32(infoField[0]), Convert.ToInt32(infoField[1]), infoField[3], "");
                }
                else
                {
                    buildings[buildings.Length - 1] = new ResourceBuilding(Convert.ToInt32(infoField[0]), Convert.ToInt32(infoField[1]), infoField[3]);
                }
                buildings[buildings.Length - 1].Health = Convert.ToInt32(infoField[2]);
                buildingInfo = reader.ReadLine();

            }
            reader.Close();
            file.Close();
            UpdateMap();
        }
    }
}
