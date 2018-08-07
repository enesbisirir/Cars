using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    enum CarType
    {
        Red,
        Yellow
    }

    enum CarPosition
    {
        Left,
        MovingToRight,
        MovingToLeft,
        Right
    }

    enum FallingObjectType
    {
        Good,
        Bad
    }

    enum FallingObjectBigLane
    {
        Left,
        Right
    }

    enum CollisionType
    {
        GoodTouchesGround,
        GoodTouchesCar,
        BadTouchesGround,
        BadTouchesCar,
        None
    }
}
