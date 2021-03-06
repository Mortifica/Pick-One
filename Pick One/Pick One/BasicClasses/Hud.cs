﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pick_One.Levels;
using Pick_One.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.BasicClasses
{
    public class Hud
    {
        private SpriteFont font;
        private SpriteFont timerFont;
        private MainGameLoop game;
        private GameState state;
        private Texture2D directionalArrow;
        private Texture2D hudBackground;

        public Hud(MainGameLoop gameLoop, GameState gameState, SpriteFont spriteFont, Texture2D arrow, Texture2D background)
        {
            game = gameLoop;
            state = gameState;
            font = spriteFont;
            directionalArrow = arrow;
            hudBackground = background;
            timerFont = game.timerFont;
        }

        public void Update(GameTime gameTime)
        {
            if(LevelManager.LevelTimer.Elapsed.Seconds > LevelManager.LevelTimeLimit)
            {
                SoundContainer.Instance.Death.Play();
                LevelManager.Instance.SetLevel(LevelManager.CurrentLevel);
                LevelManager.Player.RestartPlayer();
            }
            else if(LevelManager.FinishedGame)
            {
                state.NextState();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            
            var EndPosition = LevelManager.Instance.GetFinishingPosition();
            var PlayerPosition = LevelManager.Player.GetLocation();
            var windowWidth = game.GraphicsDevice.Viewport.Width;
            var windowHeight = game.GraphicsDevice.Viewport.Height;
            var helpHud = game.Camera.Focus.Location - new Vector2(windowWidth/2, windowHeight/2);
            var arrowLocation = helpHud + new Vector2(hudBackground.Width*3 + 50, 210);
            double dx = EndPosition.X - arrowLocation.X;
            double dy = EndPosition.Y - arrowLocation.Y;
            var rotation = Math.Atan2(dy,dx);

           
            spriteBatch.Draw(hudBackground, new Rectangle((int)helpHud.X, (int)helpHud.Y, hudBackground.Width* 4, hudBackground.Height* 4), new Rectangle(0, 0, hudBackground.Width , hudBackground.Height), Color.White);
            spriteBatch.Draw(directionalArrow, arrowLocation, new Rectangle(0, 0, directionalArrow.Width, directionalArrow.Height), Color.White, (float)rotation, new Vector2(directionalArrow.Width, directionalArrow.Height), 2f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font,game.Player.GetCurrentState().Item2.ToString(),helpHud + new Vector2(40,50), Color.Black);
            spriteBatch.DrawString(timerFont, (LevelManager.LevelTimeLimit - LevelManager.LevelTimer.Elapsed.Seconds).ToString() , helpHud + new Vector2(hudBackground.Width * 2, -20), Color.Green);
        }
    }
}
