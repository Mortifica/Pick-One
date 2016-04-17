using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.BasicClasses
{
    public class HitBox
    {
        public Rectangle HitBoxRectangle;
        public bool IsPassthroughableFromTop { get; set; }
        public bool IsPassthroughableFromBottom { get; set; }
        public HitBox(float x, float y, float height, float width)
        {
            HitBoxRectangle = new Rectangle((int)x,(int)y,(int)width -2,(int)height -2);
        }
        internal void Update(float xLocation, float yLocation)
        {
            HitBoxRectangle.X = (int)xLocation;
            HitBoxRectangle.Y = (int)yLocation;
        }
    }
}
