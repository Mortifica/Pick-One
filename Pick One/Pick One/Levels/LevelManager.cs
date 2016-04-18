using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Pick_One.BasicClasses;
using Pick_One.Character;
using Pick_One.Levels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Levels
{
    public sealed class LevelManager
    {
        private const string FOLDER_PATH = @"Levels\";

        private static readonly Lazy<LevelManager> lazy = new Lazy<LevelManager>(() => new LevelManager());

        private List<Tile> Level;

        private CollisionManager Collision;

        public static string CurrentLevel { get; set; }

        public static string NextLevel { get; set; }

        private static Stopwatch levelTimer;
        public static Stopwatch LevelTimer { get { return levelTimer; } }

        private static int levelTimeLimit = 10;
        public static int LevelTimeLimit { get { return levelTimeLimit; } }

        public static LevelManager Instance { get { return lazy.Value; } }

        public static ContentManager Content { set; get; }

        public static Player Player { set; get; }

        public static bool FinishedGame { get; set; } = false;

        private LevelManager()
        {
            levelTimer = new Stopwatch();
        }

        public void SetLevel(string levelname)
        {
            CurrentLevel = levelname;
            GetLevelInfo();
        }

        public void SetLevel(string levelname, string nextLevel)
        {
            FinishedGame = false;

            CurrentLevel = levelname;
            NextLevel = nextLevel;
            var texture = Content.Load<Texture2D>(FOLDER_PATH + levelname);
            Level = LevelFactory.GenerateLevel(Content, texture);
            Collision = new CollisionManager(Level);
        }

        public void EndLevel()
        {
            // Check if there's a next level, if not 
            if (string.IsNullOrEmpty(NextLevel))
            {
                // End the game or go back
                FinishedGame = true;
            }
            else
            {
                SoundContainer.Instance.Finish.Play();
                CurrentLevel = NextLevel;
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
        public Vector2 GetFinishingPosition()
        {
            if (Level != null)
                return Level.Single(tile => tile.Type == Tile.TileTypes.EndPosition).Location;//= Level.Single(tile => tile.).Location;
            else
                return new Vector2(0, 0);
        }
        //public Vector2 GetPlayerStartingLocation()
        //{
        //    Vector2 startingPlace = LevelManager.Instance.GetStartingPosition();
        //    startingPlace.Y -= 32;
        //    return startingPlace;
        //}

        private void GetLevelInfo()
        {
            if (CurrentLevel == "Level1")
            {
                SetLevel("Level1", "Level2");
                LevelMusic(SoundContainer.Instance.LevelTheme);
                Timer(5);
            }
            else if (CurrentLevel == "Level2")
            {
                // Next level
                SetLevel("Level2", "Level3");
                LevelMusic(SoundContainer.Instance.LevelTheme);
                Timer(30);
            }
            else if (CurrentLevel == "Level3")
            {
                // Next level
                SetLevel("Level3", "Level4");
                LevelMusic(SoundContainer.Instance.LevelTheme);
                Timer(30);
            }
            else if (CurrentLevel == "Level4")
            {
                // Next level
                SetLevel("Level4", "");
                LevelMusic(SoundContainer.Instance.LevelTheme);
                Timer(60);
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

        private void Timer(int timeLimit)
        {
            levelTimeLimit = timeLimit;
            LevelTimer.Reset();
            LevelTimer.Start();
        }

        private void LevelMusic(Song songName)
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(songName);
        }
    }
}
