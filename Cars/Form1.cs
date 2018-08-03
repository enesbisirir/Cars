using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cars
{
    public partial class Form1 : Form
    {
        // TODO: Randomized intervals
        private Timer tmrLeftObjectSpawner = new Timer() { Interval = 2000 };
        private Timer tmrRightObjectSpawner = new Timer() { Interval = 1800 };

        public Form1()
        {
            InitializeComponent();

            // Event handlers of object spawner timers
            tmrLeftObjectSpawner.Tick += TmrLeftObjectSpawner_Tick;
            tmrRightObjectSpawner.Tick += TmrRightObjectSpawner_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set position of lines
            LblMiddle.Left = (ClientSize.Width - LblMiddle.Width) / 2;
            LblLeft.Left = (ClientSize.Width / 4) - (LblLeft.Width / 2);
            LblRight.Left = (ClientSize.Width / 4 * 3) - (LblRight.Width / 2);

            // Spawn Cars
            Controls.Add(GameManager.Current.RedCar);
            Controls.Add(GameManager.Current.YellowCar);

            // Start timers to spawn FallingObjects
            tmrLeftObjectSpawner.Start();
            tmrRightObjectSpawner.Start();

            // TODO: Start objects' falling
        }

        // TODO: Keys will not be hardcoded, 
        // user will be able to set keys on their own
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.A)
            {
                GameManager.Current.RedCar.ChangeLane();
            }

            else if (e.KeyValue == (char)Keys.K)
            {
                GameManager.Current.YellowCar.ChangeLane();
            }
        }

        // Spawn right falling objects
        private void TmrRightObjectSpawner_Tick(object sender, EventArgs e)
        {
            FallingObject fallingObject = new FallingObject(FallingObject.RandomizedFallingObjectType(), FallingObject.RandomizedLane(FallingObjectBigLane.Right));
            this.Controls.Add(fallingObject);
        }

        // Spawn left falling objects
        private void TmrLeftObjectSpawner_Tick(object sender, EventArgs e)
        {
            FallingObject fallingObject = new FallingObject(FallingObject.RandomizedFallingObjectType(), FallingObject.RandomizedLane(FallingObjectBigLane.Left));
            this.Controls.Add(fallingObject);
        }
    }
}
