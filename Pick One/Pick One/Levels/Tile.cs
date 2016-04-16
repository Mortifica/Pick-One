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

        private Texture2D texture;
        private Sprite sprite;

        #endregion

        #region Properties

        public TileTypes Type
        {
            get;
        }

        public Vector2 Location
        {
            get;
        }

        public Rectangle Rectangle
        {
            get;
        }

        #endregion

        #region Enum

        public enum TileTypes
        {
            Empty,
            StartPosition,
            EndPosition,
            Floor
        }

        #endregion

        #region Constructor

        public Tile(ContentManager content, TileTypes type, int x, int y)
        {
            Type = type;
            Location = GetVector(x, y);
            sprite = GetSprite(content);
            Rectangle = new Rectangle(x * TILE_SIZE, y * TILE_SIZE, sprite.Texture.Width, sprite.Texture.Height);
        }

        #endregion

        #region Private Methods

        private Sprite GetSprite(ContentManager content)
        {
            if (Type == TileTypes.Floor)
            {
                texture = content.Load<Texture2D>(@"test_Ground_Texture");
            }
            else if (Type == TileTypes.StartPosition)
            {
                texture = content.Load<Texture2D>(@"test_Ground_Texture");
            }
            else if (Type == TileTypes.EndPosition)
            {
                texture = content.Load<Texture2D>(@"test_Finish_Texture");
            }

            return new Sprite(texture, 1, 1); // If we need to animate this needs to change.
        }

        private Vector2 GetVector(int x, int y)
        {
            return new Vector2(x * TILE_SIZE, y * TILE_SIZE);
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
