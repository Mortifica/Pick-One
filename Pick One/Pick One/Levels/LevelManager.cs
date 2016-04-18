﻿using Microsoft.Xna.Framework;
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
        private string NextLevel;

        private CollisionManager Collision;

        private static Stopwatch levelTimer;
        public static Stopwatch LevelTimer { get { return levelTimer; } }

        private static int levelTimeLimit = 10;
        public static int LevelTimeLimit { get { return levelTimeLimit; } }

        public static LevelManager Instance { get { return lazy.Value; } }

        public static ContentManager Content { set; get; }

        public static Player Player { set; get; }

        private LevelManager()
        {
            levelTimer = new Stopwatch();
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
        public Vector2 GetFinishingPosition()
        {
            if (Level != null)
                return Level.Single(tile => tile.Type == Tile.TileTypes.EndPosition).Location;//= Level.Single(tile => tile.).Location;
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
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = (float)0.25;
            if (NextLevel == "Level1")
            {
                SetLevel(FOLDER_PATH + "Level1", "Level2");
                MediaPlayer.Stop();
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(SoundContainer.Instance.Tutorial);
                Timer(30);
            }
            else if (NextLevel == "Level2")
            {
                // Next level
                SetLevel(FOLDER_PATH + "Level2","Level3");
                MediaPlayer.Stop();
                MediaPlayer.Play(SoundContainer.Instance.LevelTheme);
                Timer(30);
            }
            else if (NextLevel == "Level3")
            {
                // Next level
                SetLevel(FOLDER_PATH + "Level3", "Level4");
                MediaPlayer.Stop();
                MediaPlayer.Play(SoundContainer.Instance.LevelTheme);
                Timer(30);
            }
            else if (NextLevel == "Level4")
            {
                // Next level
                SetLevel(FOLDER_PATH + "Level4", "Level1");
                MediaPlayer.Stop();
                MediaPlayer.Play(SoundContainer.Instance.LevelTheme);
                Timer(30);
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
    }
}
