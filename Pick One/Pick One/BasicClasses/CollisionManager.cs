using Microsoft.Xna.Framework;
using Pick_One.Levels;
using System;
using System.Collections.Generic;

namespace Pick_One.BasicClasses
{
    public class CollisionManager
    {
        private List<Tile> collisionObjects;

        public CollisionManager(List<Tile> tiles)
        {
            collisionObjects = tiles;
        }

        public Tuple<bool, List<Tile>> CheckCollision(Rectangle obj)
        {
            List<Tile> collisionRetangles = new List<Tile>();
            foreach (var rec in collisionObjects)
            {
                if (rec.Rectangle.Intersects(obj))
                {
                    collisionRetangles.Add(rec);
                }
            }

            return new Tuple<bool, List<Tile>>(collisionRetangles.Count > 0 ? true : false, collisionRetangles);
        }

        public void Add(Tile rectangle)
        {
            collisionObjects.Add(rectangle);
        }
        
        public void Remove(Tile rectangle)
        {
            collisionObjects.Remove(rectangle);
        }


    }
}
