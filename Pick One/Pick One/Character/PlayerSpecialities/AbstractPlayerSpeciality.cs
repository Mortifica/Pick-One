using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pick_One.BasicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Character.PlayerSpecialities
{
    public abstract class AbstractPlayerSpeciality
    {
        public MovementContainer Movement { get; set; }
        public bool IsStretchable { get; set; }
        public bool IsClimbable { get; set; }
        public bool IsJumpable { get; set; }
        public Sprite StandingSprite { get; set; }
        public Sprite MovingLeft { get; set; }
        public Sprite MovingRight { get; set; }
        public Sprite Jump { get; set; }
        public Sprite Landing { get; set; }
        public Sprite MidJump { get; set; }
        public Sprite WallClimbUp { get; set; }
        public Sprite WallClimbDown { get; set; }
        public Sprite Falling { get; set; }
        public AbstractPlayerSpeciality NextTransform { get; set; }
        public AbstractPlayerSpeciality PrevTransform { get; set; }
        public PlayerState CurrentState { get; set; }

        public float Width
        {
            get; set;
        }
        public float Height { get; set; }

        internal void UpdateSprite()
        {
            switch (CurrentState)
            {
                case PlayerState.Standing:
                    StandingSprite.Update();
                    Width = StandingSprite.Texture.Width / StandingSprite.Columns;
                    Height = StandingSprite.Texture.Height;
                    break;
                case PlayerState.Jump:
                    Jump.Update();
                    Width = Jump.Texture.Width / Jump.Columns;
                    Height = Jump.Texture.Height;
                    break;
                case PlayerState.MovingLeft:
                    MovingLeft.Update();
                    Width = MovingLeft.Texture.Width / MovingLeft.Columns;
                    Height = MovingLeft.Texture.Height;
                    break;
                case PlayerState.MovingRight:
                    MovingRight.Update();
                    Width = MovingRight.Texture.Width / MovingRight.Columns;
                    Height = MovingRight.Texture.Height;
                    break;
                case PlayerState.WallClimbDown:
                    WallClimbDown.Update();
                    Width = WallClimbDown.Texture.Width / WallClimbDown.Columns;
                    Height = WallClimbDown.Texture.Height;
                    break;
                case PlayerState.WallClimbUp:
                    WallClimbUp.Update();
                    Width = WallClimbUp.Texture.Width / WallClimbUp.Columns;
                    Height = WallClimbUp.Texture.Height;
                    break;
                case PlayerState.Landing:
                    Landing.Update();
                    Width = Landing.Texture.Width / Landing.Columns;
                    Height = Landing.Texture.Height;
                    break;
                case PlayerState.MidJump:
                    MidJump.Update();
                    Width = MidJump.Texture.Width / MidJump.Columns;
                    Height = MidJump.Texture.Height;
                    break;
                case PlayerState.Falling:
                    Falling.Update();
                    Width = Falling.Texture.Width / Falling.Columns;
                    Height = Falling.Texture.Height;
                    break;
            }
        }
        internal void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            switch (CurrentState)
            {
                case PlayerState.Standing:
                    StandingSprite.Draw(spriteBatch, location);
                    break;
                case PlayerState.Jump:
                    Jump.Draw(spriteBatch, location);
                    break;
                case PlayerState.MovingLeft:
                    MovingLeft.Draw(spriteBatch, location);
                    break;
                case PlayerState.MovingRight:
                    MovingRight.Draw(spriteBatch, location);
                    break;
                case PlayerState.WallClimbDown:
                    WallClimbDown.Draw(spriteBatch, location);
                    break;
                case PlayerState.WallClimbUp:
                    WallClimbUp.Draw(spriteBatch, location);
                    break;
                case PlayerState.Landing:
                    Landing.Draw(spriteBatch, location);
                    break;
                case PlayerState.MidJump:
                    MidJump.Draw(spriteBatch, location);
                    break;
                case PlayerState.Falling:
                    Falling.Draw(spriteBatch, location);
                    break;
            }
        }

        internal void SetState(PlayerState currentState)
        {
            
            CurrentState = currentState;
            switch (CurrentState)
            {
                case PlayerState.Standing:
                    StandingSprite.currentFrame = 0;
                    break;
                case PlayerState.Jump:
                    Jump.currentFrame = 0;
                    break;
                case PlayerState.MovingLeft:
                    MovingLeft.currentFrame = 0;
                    break;
                case PlayerState.MovingRight:
                    MovingRight.currentFrame = 0;
                    break;
                case PlayerState.WallClimbDown:
                    WallClimbDown.currentFrame = 0;
                    break;
                case PlayerState.WallClimbUp:
                    WallClimbUp.currentFrame = 0;
                    break;
                case PlayerState.Landing:
                    Landing.currentFrame = 0;
                    break;
                case PlayerState.MidJump:
                    MidJump.currentFrame = 0;
                    break;
            }
        }
    }
}
