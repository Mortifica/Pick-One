using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pick_One.Levels;
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
        private TimeSpan timer;
        private int levelTime = 30;

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
            timer += gameTime.ElapsedGameTime;
            if(timer.Seconds > levelTime)
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
            var helpHud = game.Camera.Focus.Location - new Vector2(windowWidth/2 - 10, windowHeight/2 - 10);
            var arrowLocation = game.Camera.Focus.Location - new Vector2(0, windowHeight/3);
            double dx = EndPosition.X - arrowLocation.X;
            double dy = EndPosition.Y - arrowLocation.Y;
            var rotation = Math.Atan2(dy,dx);

            spriteBatch.Draw(directionalArrow, arrowLocation, new Rectangle(0, 0, directionalArrow.Width, directionalArrow.Height), Color.White, (float)rotation, new Vector2(directionalArrow.Width, directionalArrow.Height), 2f, SpriteEffects.None, 0f);
            spriteBatch.Draw(hudBackground, new Rectangle((int)helpHud.X, (int)helpHud.Y, directionalArrow.Width * 2, directionalArrow.Height), new Rectangle((int)helpHud.X, (int)helpHud.Y, directionalArrow.Width , directionalArrow.Height), Color.White);
            spriteBatch.DrawString(font,game.Player.GetCurrentState().Item2.ToString(),helpHud + new Vector2(10,10), Color.Black);
            spriteBatch.DrawString(timerFont, (levelTime - timer.Seconds).ToString() , arrowLocation + new Vector2(0,directionalArrow.Width + 10), Color.White);
        }
    }
}
