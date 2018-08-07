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
        public FallingObjectType Type { get; set; }
        public FallingObjectBigLane BigLane { get; set; }

        public FallingObject(FallingObjectType objectType, int lane)
        {
            // Adds every object spawned to the list
            Game.Current.FallingObjects.Add(this);

            // Define properties of falling object
            Type = objectType;
            SmallLane = lane;
            Size = new Size(30, 30);
            SizeMode = PictureBoxSizeMode.Zoom;
            Velocity = 3;

            // Define type of object
            if (Type == FallingObjectType.Good)
            {
                Image = Properties.Resources.Star;
            }
            else if (Type == FallingObjectType.Bad)
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
        /// Checks if this object is touching to the ground
        /// </summary>
        /// <returns>True if touching, false if not</returns>
        public bool IsTouchingToGround()
        {
            if (this.Top >= Game.GroundY - (Height / 2))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if this object is touching to a car
        /// </summary>
        /// <returns>True if touching, false if not</returns>
        public bool IsTouchingToCar()
        {
            foreach (var car in Game.Current.Cars)
            {
            if (this.Bounds.IntersectsWith(car.Bounds))
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Disposes this object's image and itself
        /// </summary>
        public void Destroy()
        {
            if (this.Image != null)
                this.Image.Dispose();
                
            if (this != null)
                this.Dispose();
        }

        /// <summary>
        /// Returns an object type randomly
        /// </summary>
        /// <returns>Enum of FallingObjectType</returns>
        public static FallingObjectType RandomType()
        {
            int randomHolder = Game.Current.Random.Next(1, 3);
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
            int randomHolder = Game.Current.Random.Next(1, 3);
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