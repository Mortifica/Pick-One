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
    public class Tile
    {
        #region Private Varibles

        private const int TILE_SIZE = 32;

        private Texture2D texture;
        private Sprite sprite;

        #endregion

        #region Properties

        public TileTypes Type
        {
            get;
        }

        private Vector2 location;
        public Vector2 Location
        {
            get { return location; }
        }

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

        #endregion

        #region Enum

        public enum TileTypes
        {
            Empty,
            StartPosition,
            EndPosition,
            Floor,
            Bounds,
        }

        #endregion

        #region Constructor

        public Tile(ContentManager content, TileTypes type, int x, int y)
        {
            Type = type;
            sprite = GetSprite(content, x, y);
        }

        #endregion

        #region Private Methods

        private Sprite GetSprite(ContentManager content, int x, int y)
        {
            if (Type == TileTypes.Floor)
            {
                texture = content.Load<Texture2D>(@"test_Ground_Texture");
                location = new Vector2(x * texture.Width, y * texture.Height);
                rectangle = new Rectangle(x * texture.Width, y * texture.Height, texture.Width, texture.Height);
            }
            else if (Type == TileTypes.StartPosition)
            {
                texture = content.Load<Texture2D>(@"test_Ground_Texture");
                location = new Vector2(x * texture.Width, y * texture.Height);
                rectangle = new Rectangle(x * texture.Width, y * texture.Height, texture.Width, texture.Height);
            }
            else if (Type == TileTypes.EndPosition)
            {
                texture = content.Load<Texture2D>(@"test_Finish_Texture");
                location = new Vector2(x * texture.Width, y * texture.Height);
                rectangle = new Rectangle(x * texture.Width, y * texture.Height, texture.Width, texture.Height);
            }
            else if (Type == TileTypes.Bounds)
            {
                texture = content.Load<Texture2D>(@"test_Finish_Texture");
                location = new Vector2(x * texture.Width, y * texture.Height);
                rectangle = new Rectangle(x * texture.Width, y * texture.Height, texture.Width, texture.Height);
            }

            return new Sprite(texture, 1, 1); // If we need to animate this can be nested into the if statement.
        }

        #endregion

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Location);
        }

        #endregion
    }
}
