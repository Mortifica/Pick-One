using Microsoft.Xna.Framework;
using Pick_One.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.BasicClasses
{
    public static class CollisionFactory
    {

        public static CollisionManager GenerateCollision(params Vector2[] vectors)
        {

            foreach (var vector in vectors)
            {
                //new Rectangle(vector.X, vector.Y, );
            }

            return null;
        }

    }
}
