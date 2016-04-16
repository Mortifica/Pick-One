using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pick_One.BasicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Levels
{
    class Tile
    {
        #region Private Varibles

        private const int TILE_SIZE = 32;

        private Type type;
        private Texture2D texture;
        private Sprite sprite;
        private Vector2 location;

        #endregion

        public enum Type
        {
            Empty,
            StartPosition,
            EndPosition,
            Floor
        }

        public Tile(ContentManager content, Type type, int x, int y)
        {
            this.type = type;
            location = GetVector(x, y);
            sprite = GetSprite(content, type);
        }

        private Sprite GetSprite(ContentManager content, Type type)
        {
            if (type == Type.Floor)
            {
                texture = content.Load<Texture2D>(@"test_Ground_Texture");
            }
            else if (type == Type.StartPosition)
            {
                texture = content.Load<Texture2D>(@"test_Ground_Texture");
            }
            else if (type == Type.EndPosition)
            {
                texture = content.Load<Texture2D>(@"test_Finish_Texture");
            }

            return new Sprite(texture, 1, 1); // If we need to animate this needs to change.
        }

        private Vector2 GetVector(int x, int y)
        {
            return new Vector2(x* TILE_SIZE, y* TILE_SIZE);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, location);
        }
    }
}
