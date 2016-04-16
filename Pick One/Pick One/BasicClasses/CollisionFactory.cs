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

        public static CollisionManager GenerateCollision(params Rectangle[] rectangles)
        {
            CollisionManager collisionManager = new CollisionManager();
            foreach (var rectangle in rectangles)
            {
                //collisionManager.Add(rectangle);
            }
            return collisionManager;
        }

    }
}
