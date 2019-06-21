using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBrander.UI.Models
{
    public class Position
    {
        public enum Point
        {
            TopLeft,
            TopMiddle,
            TopRight,
            MiddleLeft,
            Middle,
            MiddleRight,
            BottomLeft,
            BottomMiddle,
            BottomRight
        }

        public static Point GetPoint(string str)
        {
            switch(str)
            {
                case "TopLeft":
                    return Point.TopLeft;
                case "TopMiddle":
                    return Point.TopMiddle;
                case "TopRight":
                    return Point.TopRight;
                case "MiddleLeft":
                    return Point.MiddleLeft;
                case "Middle":
                    return Point.Middle;
                case "MiddleRight":
                    return Point.MiddleRight;
                case "BottomLeft":
                    return Point.BottomLeft;
                case "BottomMiddle":
                    return Point.BottomMiddle;
                case "BottomRight":
                    return Point.BottomRight;
                default:
                    throw new ArgumentException($"Point {str} does not exist");
            }
        }
    }
}
