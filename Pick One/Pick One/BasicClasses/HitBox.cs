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
        public Rectangle HitBoxRectangle { get; set; }
        public bool IsPassthroughableFromTop { get; set; }
        public bool IsPassthroughableFromBottom { get; set; }

        internal void Update(float xLocation, float yLocation)
        {
            throw new NotImplementedException();
        }
    }
}
