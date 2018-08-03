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

        public FallingObject(FallingObjectType objectType, int lane)
        {
            ObjectType = objectType;
            SmallLane = lane;
            Size = new Size(30, 30);
            SizeMode = PictureBoxSizeMode.Zoom;

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

        /// <summary>
        /// Returns an object type randomly
        /// </summary>
        /// <returns>Enum of FallingObjectType</returns>
        public static FallingObjectType RandomizedFallingObjectType()
        {
            int randomHolder = new Random().Next(1, 3);
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
        /// <param name="bigLane">Big lane of FallingObject</param>
        /// <returns>Integer between 1 and 4</returns>
        public static int RandomizedLane(FallingObjectBigLane bigLane)
        {
            int randomHolder = new Random().Next(1, 3);
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


