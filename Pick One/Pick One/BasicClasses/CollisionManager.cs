using Microsoft.Xna.Framework;
using Pick_One.Levels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pick_One.BasicClasses
{
    public class CollisionManager
    {
        private List<Tile> CollisionObjects;

        public CollisionManager(List<Tile> tiles)
        {
            CollisionObjects = tiles;
        }

        public Tuple<bool, List<Tile>> CheckCollision(Rectangle obj)
        {
            List<Tile> collisionRetangles = new List<Tile>();
            foreach (var rec in CollisionObjects)
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
            CollisionObjects.Add(rectangle);
        }
        
        public void Remove(Tile rectangle)
        {
            CollisionObjects.Remove(rectangle);
        }

        public IEnumerable<Tile> GetBlocksAt(float x, float y, float height, float width)
        {
            return CollisionObjects.Where(rect => rect.Rectangle.Intersects(new Rectangle((int)x, (int)y, (int)width, (int)height)));
        }


    }
}
