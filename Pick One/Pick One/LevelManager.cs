using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pick_One.BasicClasses;
using Pick_One.Character;
using Pick_One.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One
{
    public sealed class LevelManager
    {
        private static readonly Lazy<LevelManager> lazy = new Lazy<LevelManager>(() => new LevelManager());

        private static List<Tile> Level;
        private static string NextLevel;

        private static CollisionManager Collision;


        public static LevelManager Instance { get { return lazy.Value; } }

        public static ContentManager Content { set; get; }

        public static Player Player { set; get; }

        private LevelManager()
        {
        }

        public void SetLevel(string levelname)
        {
            NextLevel = levelname;
            GetLevelInfo();
        }

        public void SetLevel(string levelname, string nextLevel)
        {
            NextLevel = nextLevel;
            var texture = Content.Load<Texture2D>(levelname);
            Level = LevelFactory.GenerateLevel(Content, texture);
            Collision = new CollisionManager(Level);
        }

        public void EndLevel()
        {
            // Check if there's a next level, if not 
            if (string.IsNullOrEmpty(NextLevel))
            {
                // End the game or go back
            }
            else
            {
                GetLevelInfo();
            }
        }

        public Vector2 GetStartingPosition()
        {
            if (Level != null)
                return Level.Single(tile => tile.Type == Tile.TileTypes.StartPosition).Location;//= Level.Single(tile => tile.).Location;
            else
                return new Vector2(0, 0);
        }

        public Vector2 GetPlayerStartingLocation()
        {
            Vector2 startingPlace = LevelManager.Instance.GetStartingPosition();
            startingPlace.Y -= 32;
            return startingPlace;
        }

        private void GetLevelInfo()
        {
            if (NextLevel == "TestLevel")
            {
                SetLevel("TestLevel", "LargeTestLevel1");
            }
            else if (NextLevel == "LargeTestLevel1")
            {
                // Next level
                SetLevel("LargeTestLevel1", "LargeTestLevel1");
            }
            else if (NextLevel == "TestLevel3")
            {
                // Next level
                SetLevel("TestLevel3", "TestLevel4");
            }
            else if (NextLevel == "TestLevel4")
            {
                SetLevel("TestLevel4", ""); // Last level
            }
            else
            {

            }
        }

        public void DrawLevel(SpriteBatch spriteBatch)
        {
            Level.ForEach(x => { x.Draw(spriteBatch); });
        }

        public Tuple<bool, List<Tile>> CheckCollision(Rectangle rectangle)
        {
            return Collision.CheckCollision(rectangle);
        }

        public IEnumerable<Tile> GetBlocksAt(float x, float y, float height, float width)
        {
            return Collision.GetBlocksAt(x, y, height, width);
        }
    }
}
