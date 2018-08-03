using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cars
{
    class Game
    {
        public Car RedCar { get; set; }
        public Car YellowCar { get; set; }

        public Game()
        {
            RedCar = new Car(CarType.Red);
            YellowCar = new Car(CarType.Yellow);
        }

        // Remember vital positions
        public static int FirstLaneX
        {
            get
            {
                return (346 / 8);
            }
        }
        public static int SecondLaneX
        {
            get
            {
                return 3 * (346 / 8);
            }
        }
        public static int ThirdLaneX
        {
            get
            {
                return 5 * (346 / 8);
            }
        }
        public static int FourthLaneX
        {
            get
            {
                return 7 * (346 / 8);
            }
        }
        public static int GroundY
        {
            get
            {
                return 498;
            }
        }
    }
}
