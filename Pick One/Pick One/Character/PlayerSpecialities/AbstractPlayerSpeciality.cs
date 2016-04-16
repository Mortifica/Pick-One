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
        public Sprite WallClimbUp { get; set; }
        public Sprite WallClimbDown { get; set; }
        public Sprite Falling { get; set; }
        public AbstractPlayerSpeciality NextTransform { get; set; }
        public AbstractPlayerSpeciality PrevTransform { get; set; }
        public PlayerState CurrentState { get; set; }

        public float Width { get; set; }
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
                    Width = Jump.Texture.Width / StandingSprite.Columns;
                    Height = Jump.Texture.Height;
                    break;
                case PlayerState.MovingLeft:
                    MovingLeft.Update();
                    Width = MovingLeft.Texture.Width / StandingSprite.Columns;
                    Height = MovingLeft.Texture.Height;
                    break;
                case PlayerState.MovingRight:
                    MovingRight.Update();
                    Width = MovingRight.Texture.Width / StandingSprite.Columns;
                    Height = MovingRight.Texture.Height;
                    break;
                case PlayerState.WallClimbDown:
                    WallClimbDown.Update();
                    Width = WallClimbDown.Texture.Width / StandingSprite.Columns;
                    Height = WallClimbDown.Texture.Height;
                    break;
                case PlayerState.WallClimbUp:
                    WallClimbUp.Update();
                    Width = WallClimbUp.Texture.Width / StandingSprite.Columns;
                    Height = WallClimbUp.Texture.Height;
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
            }
        }

        internal void SetState(PlayerState currentState)
        {
            CurrentState = currentState;
        }
    }
}
