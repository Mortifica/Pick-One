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

        public Hud(MainGameLoop gameLoop, SpriteFont spriteFont)
        {
            game = gameLoop;
            font = spriteFont;
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font,"Test Text",game.Camera.Focus.Location - new Vector2(200,200), Color.White);
        }
    }
}
