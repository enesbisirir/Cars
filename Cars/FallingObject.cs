using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cars
{
    class FallingObject : PictureBox
    {
        public int Velocity { get; set; }
        public int SmallLane { get; set; }
        public FallingObjectType ObjectType { get; set; }
        public FallingObjectBigLane BigLane { get; set; }
        public Timer TmrIntersectionChecker = new Timer() { Interval = 10 };

        public FallingObject(FallingObjectType objectType, int lane)
        {
            // Event handler of intersection checker timer
            TmrIntersectionChecker.Tick += TmrIntersectionChecker_Tick;
            TmrIntersectionChecker.Start();

            // Define properties of falling object
            ObjectType = objectType;
            SmallLane = lane;
            Size = new Size(30, 30);
            SizeMode = PictureBoxSizeMode.Zoom;
            Velocity = 3;

            // Define type of object
            if (ObjectType == FallingObjectType.Good)
            {
                Image = Properties.Resources.Star;
            }
            else if (ObjectType == FallingObjectType.Bad)
            {
                Image = Properties.Resources.Bomb;
            }

            // Define big and small lanes of object
            switch (SmallLane)
            {
                case 1:
                    Location = new Point(Game.FirstLaneX - (Width / 2), 10);
                    BigLane = FallingObjectBigLane.Left;
                    break;
                case 2:
                    Location = new Point(Game.SecondLaneX - (Width / 2), 10);
                    BigLane = FallingObjectBigLane.Left;
                    break;
                case 3:
                    Location = new Point(Game.ThirdLaneX - (Width / 2), 10);
                    BigLane = FallingObjectBigLane.Right;
                    break;
                case 4:
                    Location = new Point(Game.FourthLaneX - (Width / 2), 10);
                    BigLane = FallingObjectBigLane.Right;
                    break;
            }
        }

        // TODO: Solve memory leak
        private void TmrIntersectionChecker_Tick(object sender, EventArgs e)
        {
            if (this.Top >= 150)
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// Returns an object type randomly
        /// </summary>
        /// <returns>Enum of FallingObjectType</returns>
        public static FallingObjectType RandomType()
        {
            int randomHolder = GameManager.Current.Random.Next(1, 3);
            if (randomHolder == 1)
            {
                return FallingObjectType.Good;
            }
            else
            {
                return FallingObjectType.Bad;
            }
        }

        /// <summary>
        /// Returns a small lane randomly
        /// </summary>
        /// <param name="bigLane">Big lane of falling object</param>
        /// <returns>Integer between 1 and 4</returns>
        public static int RandomLane(FallingObjectBigLane bigLane)
        {
            int randomHolder = GameManager.Current.Random.Next(1, 3);
            if (bigLane == FallingObjectBigLane.Left)
            {
                return randomHolder;
            }
            else
            {
                return randomHolder + 2;
            }
        }
    }
}