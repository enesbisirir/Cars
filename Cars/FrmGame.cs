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
    public partial class FrmGame : Form
    {
        public static Timer TmrLeftObjectSpawner = new Timer();
        public static Timer TmrRightObjectSpawner = new Timer();

        public FrmGame()
        {
            InitializeComponent();

            // Event handlers
            TmrLeftObjectSpawner.Tick += TmrLeftObjectSpawner_Tick;
            TmrRightObjectSpawner.Tick += TmrRightObjectSpawner_Tick;
            Game.Current.Scored += OnScored;
        }

        private void FrmGame_Load(object sender, EventArgs e)
        {
            // Set position of lines
            LblMiddle.Left = (ClientSize.Width - LblMiddle.Width) / 2;
            LblLeft.Left = (ClientSize.Width / 4) - (LblLeft.Width / 2);
            LblRight.Left = (ClientSize.Width / 4 * 3) - (LblRight.Width / 2);

            // Spawn Cars
            foreach (var car in Game.Current.Cars)
            {
                Controls.Add(car);
            }

            // Start game
            Game.Current.Start();
        }

        // TODO: Keys will not be hardcoded, 
        // user will be able to set keys on their own
        private void FrmGame_KeyDown(object sender, KeyEventArgs e)
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

        // Update UI when scored
        private void OnScored(object sender, EventArgs e)
        {
            LblScore.Text = Game.Current.CurrentScore.ToString();
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
    }
}
