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
        #region Constructor
        public Game()
        {
            CurrentScore = 0;

            // Create cars and add them to the list
            RedCar = new Car(CarType.Red);
            YellowCar = new Car(CarType.Yellow);
            Cars.Add(RedCar);
            Cars.Add(YellowCar);

            // Event handler for main game timer
            TmrGame.Tick += TmrGame_Tick;
        }
        #endregion

        #region In game collection methods
        /// <summary>
        /// No reward for user, destroys given object
        /// </summary>
        public void NoScore(FallingObject fallingObject)
        {
            fallingObject.Destroy();
        }

        /// <summary>
        /// Rewards player with a score, destroys given object
        /// </summary>
        public void Score(FallingObject fallingObject)
        {
            fallingObject.Destroy();
            CurrentScore++;
            Scored(this, EventArgs.Empty);
        }

        /// <summary>
        /// Pauses the game and brings end-game screen
        /// </summary>
        public void Fail(FallingObject fallingObject)
        {
            Pause();
            EndGame();
        }

        /// <summary>
        /// Player will be immune to bombs for 3 second 
        /// </summary>
        public void ShieldCollected(FallingObject fallingObject)
        {
            Current.IsShieldActive = true;
            Current.ShieldTimer = DateTime.UtcNow;
            fallingObject.Destroy();
        }

        /// <summary>
        /// Called when shield timer is over
        /// </summary>
        public void ShieldCompleted()
        {
            Current.IsShieldActive = false;
        }
        #endregion

        #region Game start, pause, restart, end methods
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
            Current.CurrentScore = 0;
            Scored(this, EventArgs.Empty);

            // Start timers
            Start();
        }

        /// <summary>
        /// Pops up end game dialog
        /// </summary>
        public void EndGame()
        {
            DialogResult dialogResult = MessageBox.Show("Your score is: " + Current.CurrentScore + "\nDo you want to play again?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            // Restarts the game
            if (dialogResult == DialogResult.Yes)
                Restart();

            // Ends the game
            else if (dialogResult == DialogResult.No)
                Application.Exit();
        }
        #endregion

        #region Game mechanics
        // Happens every milisecond when game is not paused
        private void TmrGame_Tick(object sender, EventArgs e)
        {
            // Falling object manipulations
            foreach (var fallingObject in FallingObjects)
            {
                // Make objects fall
                fallingObject.Top += fallingObject.Velocity;

                // Check collision status and call related method
                if (CollisionStatus(fallingObject) == CollisionType.GoodTouchesCar)
                    Score(fallingObject);
                else if (CollisionStatus(fallingObject) == CollisionType.BadTouchesGround)
                    NoScore(fallingObject);
                else if (CollisionStatus(fallingObject) == CollisionType.GoodTouchesGround)
                    Fail(fallingObject);
                else if (CollisionStatus(fallingObject) == CollisionType.BadTouchesCar)
                {
                    if (Current.IsShieldActive == false)
                        Fail(fallingObject);
                    else if (Current.IsShieldActive == true)
                        NoScore(fallingObject);
                }
                else if (CollisionStatus(fallingObject) == CollisionType.ShieldTouchesCar)
                    ShieldCollected(fallingObject);
                else if (CollisionStatus(fallingObject) == CollisionType.ShieldTouchesGround)
                    NoScore(fallingObject);
            }

            // Shield's stop condition(Lasts 5 seconds)
            if (Current.IsShieldActive && (DateTime.UtcNow - Current.ShieldTimer).TotalSeconds >= 5)
            {
                ShieldCompleted();
            }
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
            else if (fallingObject.Type == FallingObjectType.Shield && fallingObject.Visible == true)
            {
                if (fallingObject.IsTouchingToCar())
                    return CollisionType.ShieldTouchesCar;
                else if (fallingObject.IsTouchingToGround())
                    return CollisionType.ShieldTouchesGround;
            }
            return CollisionType.None;
        }
        #endregion

        #region Variables
        public static Game Current = new Game();
        public Car RedCar { get; set; }
        public Car YellowCar { get; set; }
        public List<FallingObject> FallingObjects = new List<FallingObject>();
        public List<Car> Cars = new List<Car>();
        public Random Random = new Random();
        public Timer TmrGame = new Timer() { Interval = 1 };
        public int CurrentScore { get; set; }
        public event EventHandler Scored;
        public bool IsShieldActive { get; set; }
        public DateTime ShieldTimer { get; set; }
        #endregion

        #region Constants
        // Remember vital positions
        public const int FirstLaneX = 346 / 8;
        public const int SecondLaneX = 3 * 346 / 8;
        public const int ThirdLaneX = 5 * 346 / 8;
        public const int FourthLaneX = 7 * 346 / 8;
        public const int GroundY = 498;
        #endregion
    }
}
