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
        public Timer TmrLeftObjectSpawner = new Timer() { Interval = 2000 };
        public Timer TmrRightObjectSpawner = new Timer() { Interval = 1800 };
        public Timer TmrObjectFaller = new Timer() { Interval = 5 };

        public Form1()
        {
            InitializeComponent();

            // Event handlers
            TmrLeftObjectSpawner.Tick += TmrLeftObjectSpawner_Tick;
            TmrRightObjectSpawner.Tick += TmrRightObjectSpawner_Tick;
            TmrObjectFaller.Tick += TmrObjectFaller_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set position of lines
            LblMiddle.Left = (ClientSize.Width - LblMiddle.Width) / 2;
            LblLeft.Left = (ClientSize.Width / 4) - (LblLeft.Width / 2);
            LblRight.Left = (ClientSize.Width / 4 * 3) - (LblRight.Width / 2);

            // Spawn Cars
            Controls.Add(Game.Current.RedCar);
            Controls.Add(Game.Current.YellowCar);

            // Start timers to spawn FallingObjects
            TmrLeftObjectSpawner.Start();
            TmrRightObjectSpawner.Start();

            // Start objects' falling
            TmrObjectFaller.Start();
        }

        // TODO: Keys will not be hardcoded, 
        // user will be able to set keys on their own
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.A)
            {
                Game.Current.RedCar.ChangeLane();
            }

            else if (e.KeyValue == (char)Keys.K)
            {
                Game.Current.YellowCar.ChangeLane();
            }
        }

        // Spawn falling objects at right big lane
        private void TmrRightObjectSpawner_Tick(object sender, EventArgs e)
        {
            FallingObject fallingObject = new FallingObject(FallingObject.RandomType(), FallingObject.RandomLane(FallingObjectBigLane.Right));
            this.Controls.Add(fallingObject);
        }

        // Spawn falling objects at left big lane
        private void TmrLeftObjectSpawner_Tick(object sender, EventArgs e)
        {
            FallingObject fallingObject = new FallingObject(FallingObject.RandomType(), FallingObject.RandomLane(FallingObjectBigLane.Left));
            this.Controls.Add(fallingObject);
        }

        // Makes objects fall
        private void TmrObjectFaller_Tick(object sender, EventArgs e)
        {
            foreach (var fallingObject in Game.Current.fallingObjects)
            {
                fallingObject.Top += fallingObject.Velocity;
            }
        }
    }
}
