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

        internal void UpdateSprite()
        {
            switch (CurrentState)
            {
                case PlayerState.Standing:
                    StandingSprite.Update();
                    break;
                case PlayerState.Jump:
                    Jump.Update();
                    break;
                case PlayerState.MovingLeft:
                    MovingLeft.Update();
                    break;
                case PlayerState.MovingRight:
                    MovingRight.Update();
                    break;
                case PlayerState.WallClimbDown:
                    WallClimbDown.Update();
                    break;
                case PlayerState.WallClimbUp:
                    WallClimbUp.Update();
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
            CurrentState = CurrentState;
        }
    }
}
