using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_1_H
{
    public partial class Form1 : Form
    {
        GameEngine engine;
        Timer timer;
        GameState gameState = GameState.PAUSED;

        public Form1()
        {
            InitializeComponent();

            engine = new GameEngine();
            lblMap.Text = engine.GetMapDisplay();
            txtInfo.Text = engine.GetUnitInfo();
            lblCurrentRound.Text = "Round: " + engine.Round;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            engine.GameLoop();
            UpdateUI();

            if (engine.IsGameOver)
            {
                timer.Stop();
                lblMap.Text = engine.WinningFaction + " WON!\n" + lblMap.Text;
                gameState = GameState.ENDED;
                btnStartPause.Text = "Restart";
            }
        }

        private void UpdateUI()
        {
            lblMap.Text = engine.GetMapDisplay();
            txtInfo.Text = engine.GetUnitInfo();
            lblCurrentRound.Text = "Round: " + engine.Round;
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnStartPause_Click(object sender, EventArgs e)
        {
            if (gameState == GameState.RUNNING)
            {
                timer.Stop();
                gameState = GameState.PAUSED;
                btnStartPause.Text = "START";
            }
            else
            {
                if (gameState == GameState.ENDED)
                {
                    engine.Reset();
                }

                timer.Start();
                gameState = GameState.RUNNING;
                btnStartPause.Text = "PAUSED";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            engine.SaveUnits();
            engine.SaveBuildings();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            engine.LoadGame();
            UpdateUI();
        }
    }

    public enum GameState
    {
        RUNNING,
        PAUSED,
        ENDED
    }
}
