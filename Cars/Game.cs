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
        public static Game Current = new Game();
        public Car RedCar { get; set; }
        public Car YellowCar { get; set; }
        public List<FallingObject> FallingObjects = new List<FallingObject>();
        public List<Car> Cars = new List<Car>();
        public Random Random = new Random();
        public Timer TmrGame = new Timer() { Interval = 1 };
        public int score { get; set; }
        public event EventHandler Scored;

        public Game()
        {
            score = 0;

            // Create cars and add them to the list
            RedCar = new Car(CarType.Red);
            YellowCar = new Car(CarType.Yellow);
            Cars.Add(RedCar);
            Cars.Add(YellowCar);

            // Event handler for main game timer
            TmrGame.Tick += TmrGame_Tick;
        }

        private void TmrGame_Tick(object sender, EventArgs e)
        {
            // Falling object manipulations
            foreach (var fallingObject in FallingObjects)
            {
                // Make objects fall
                fallingObject.Top += fallingObject.Velocity;

                // Check collision status and call related method
                switch (CollisionStatus(fallingObject))
                {
                    case CollisionType.GoodTouchesCar:
                    case CollisionType.BadTouchesGround:
                        Score(fallingObject);
                        break;
                    case CollisionType.GoodTouchesGround:
                    case CollisionType.BadTouchesCar:
                        Fail(fallingObject);
                        break;
                }
            }
        }

        /// <summary>
        /// Called when good touches car or bad touches ground
        /// </summary>
        public void Score(FallingObject fallingObject)
        {
            fallingObject.Destroy();
            score++;
            OnScored();
        }

        /// <summary>
        /// Called when good touches ground or bad touches car
        /// </summary>
        public void Fail(FallingObject fallingObject)
        {
            Pause();
            EndGame();
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void Start()
        {
            RandomizeSpawnTimers();

            TmrGame.Start();
            FrmGame.TmrLeftObjectSpawner.Start();
            FrmGame.TmrRightObjectSpawner.Start();
        }

        /// <summary>
        /// Pauses the game
        /// </summary>
        public void Pause()
        {
            TmrGame.Stop();
            FrmGame.TmrLeftObjectSpawner.Stop();
            FrmGame.TmrRightObjectSpawner.Stop();
        }

        /// <summary>
        /// Restarts the game
        /// </summary>
        public void Restart()
        {
            // Reset car positions
            foreach (var car in Cars)
            {
                car.ResetPositions(car);
            }

            // Destroy all falling objects
            foreach (var fallingObject in FallingObjects)
            {
                fallingObject.Destroy();
            }

            // Reset score and publish event for it
            Current.score = 0;
            OnScored();

            // Start timers
            Start();
        }

        /// <summary>
        /// Pops up end game dialog
        /// </summary>
        public void EndGame()
        {
            DialogResult dialogResult = MessageBox.Show("Your score is: " + Current.score + "\nDo you want to play again?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            // Restarts the game
            if (dialogResult == DialogResult.Yes)
                Restart();

            // Ends the game
            else if (dialogResult == DialogResult.No)
                Application.Exit();
        }

        /// <summary>
        /// Randomizes interval of both object spawner timers
        /// </summary>
        public void RandomizeSpawnTimers()
        {
            FrmGame.TmrLeftObjectSpawner.Interval = Current.Random.Next(1800, 2000);
            FrmGame.TmrRightObjectSpawner.Interval = Current.Random.Next(1800, 2000);
        }

        /// <summary>
        /// Destroys given object
        /// </summary>
        public static void DestroyObject(FallingObject fallingObject)
        {
            if (fallingObject.Image != null)
                fallingObject.Image.Dispose();

            if (fallingObject != null)
                fallingObject.Dispose();
        }

        /// <summary>
        /// Checks if there is any current collision in game
        /// </summary>
        /// <returns>Enum of type CollisionType</returns>
        public CollisionType CollisionStatus(FallingObject fallingObject)
        {
            if (fallingObject.Type == FallingObjectType.Good && fallingObject.Visible == true)
            {
                if (fallingObject.IsTouchingToCar())
                    return CollisionType.GoodTouchesCar;
                else if (fallingObject.IsTouchingToGround())
                    return CollisionType.GoodTouchesGround;
            }
            else if (fallingObject.Type == FallingObjectType.Bad && fallingObject.Visible == true)
            {
                if (fallingObject.IsTouchingToCar())
                    return CollisionType.BadTouchesCar;
                else if (fallingObject.IsTouchingToGround())
                    return CollisionType.BadTouchesGround;
            }
            return CollisionType.None;
        }

        protected virtual void OnScored()
        {
            Scored?.Invoke(this, EventArgs.Empty);
        }

        // Remember vital positions
        public static int FirstLaneX { get { return (346 / 8); } }
        public static int SecondLaneX { get { return 3 * (346 / 8); } }
        public static int ThirdLaneX { get { return 5 * (346 / 8); } }
        public static int FourthLaneX { get { return 7 * (346 / 8); } }
        public static int GroundY { get { return 498; } }
    }
}
