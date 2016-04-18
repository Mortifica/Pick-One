using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pick_One.Input;

namespace Pick_One.BasicClasses
{
    public abstract class GameState
    {
        protected KeyboardListener Listener { get; set; }

        protected MainGameLoop game { get; set; }
        public float XOffset { get; set; }//used by the camera to adjust the corner of the ViewPort Bounds
        public float YOffset { get; set; }//used by the camera to adjust the corner of the ViewPort Bounds
        public GameState(MainGameLoop game, float xOffset = 0, float yOffset = 0)
        {
            XOffset = xOffset;
            YOffset = yOffset;
            this.game = game;
        }
        public abstract void NextState();
        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
