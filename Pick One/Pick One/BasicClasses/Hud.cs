using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private MainGameLoop game;
        private Texture2D directionalArrow;

        public Hud(MainGameLoop gameLoop, SpriteFont spriteFont, Texture2D arrow)
        {
            game = gameLoop;
            font = spriteFont;
            directionalArrow = arrow;
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            var EndPosition = LevelManager.Instance.GetFinishingPosition();
            var PlayerPosition = LevelManager.Player.GetLocation();
            var helpHud = game.Camera.Focus.Location - new Vector2(200, 200);
            var arrowLocation = game.Camera.Focus.Location - new Vector2(400, 200);
            var rotation = 0f;
            spriteBatch.Draw(directionalArrow, arrowLocation, new Rectangle(0, 0, directionalArrow.Width, directionalArrow.Height), Color.White, rotation, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font,"Test Text",helpHud, Color.White);
        }
    }
}
