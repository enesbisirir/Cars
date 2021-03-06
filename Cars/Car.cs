﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Cars
{
    class Car : PictureBox, IDisposable
    {
        public CarPosition CarPosition { get; set; }
        public CarType CarType { get; set; }
        public int Velocity { get; set; }
        private Timer tmrLaneChanger = new Timer() { Interval = 10 };

        public Car(CarType carType)
        {
            // Event handler of lane changer timer
            tmrLaneChanger.Tick += tmrLaneChanger_Tick;

            // Define properties of car
            Size = new Size(35, 70);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Velocity = 2;
            CarType = carType;

            ResetPositions(this);
        }

        public void ChangeLane()
        {
            // Define car's direction
            if (CarPosition == CarPosition.Left || CarPosition == CarPosition.MovingToLeft)
            {
                CarPosition = CarPosition.MovingToRight;
            }
            else if (CarPosition == CarPosition.Right || CarPosition == CarPosition.MovingToRight)
            {
                CarPosition = CarPosition.MovingToLeft;
            }

            tmrLaneChanger.Start();
        }

        public void ResetPositions(Car car)
        {
            if (car.CarType == CarType.Red)
            {
                Image = Properties.Resources.RedCar;
                Location = new Point(Game.FirstLaneX - (Width / 2), Game.GroundY - Height - 10);
                CarPosition = CarPosition.Left;
            }

            else if (car.CarType == CarType.Yellow)
            {
                Image = Properties.Resources.YellowCar;
                Location = new Point(Game.FourthLaneX - (Width / 2), Game.GroundY - Height - 10);
                CarPosition = CarPosition.Right;
            }
        }

        private void tmrLaneChanger_Tick(object sender, EventArgs e)
        {
            // Move red car to right
            if (CarType == CarType.Red && CarPosition == CarPosition.MovingToRight)
            {
                this.Left += Velocity;
                // Stop condition
                if (this.Left >= Game.SecondLaneX - (Width / 2))
                {
                    CarPosition = CarPosition.Right;
                    tmrLaneChanger.Stop();
                }
            }

            // Move red car to left
            else if (CarType == CarType.Red && CarPosition == CarPosition.MovingToLeft)
            {
                this.Left -= Velocity;
                // Stop condition
                if (this.Left <= Game.FirstLaneX - (Width / 2))
                {
                    CarPosition = CarPosition.Left;
                    tmrLaneChanger.Stop();
                }
            }

            // Move yellow car to left
            else if (CarType == CarType.Yellow && CarPosition == CarPosition.MovingToLeft)
            {

                this.Left -= Velocity;
                // Stop condition
                if (this.Left <= Game.ThirdLaneX - (Width / 2))
                {
                    CarPosition = CarPosition.Left;
                    tmrLaneChanger.Stop();
                }
            }

            // Move yellow car to right
            else if (CarType == CarType.Yellow && CarPosition == CarPosition.MovingToRight)
            {
                this.Left += Velocity;
                // Stop condition
                if (this.Left >= Game.FourthLaneX - (Width / 2))
                {
                    CarPosition = CarPosition.Right;
                    tmrLaneChanger.Stop();
                }
            }
        }
    }
}