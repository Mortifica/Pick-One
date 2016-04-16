using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Pick_One.BasicClasses
{
    public class CollisionManager
    {
        private List<Rectangle> collisionObjects;

        public CollisionManager(params Rectangle[] rectangles)
        {
            collisionObjects = new List<Rectangle>(rectangles);
        }

        public Tuple<bool, List<Rectangle>> CheckCollision(Rectangle obj)
        {
            List<Rectangle> collisionRetangles = new List<Rectangle>();
            foreach (var rec in collisionObjects)
            {
                if (rec.Intersects(obj))
                {
                    collisionRetangles.Add(rec);
                }
            }

            return new Tuple<bool, List<Rectangle>>(collisionRetangles.Count > 0 ? true : false, collisionRetangles);
        }

        public void Add(Rectangle rectangle)
        {
            collisionObjects.Add(rectangle);
        }
        
        public void Remove(Rectangle rectangle)
        {
            collisionObjects.Remove(rectangle);
        }


    }
}
