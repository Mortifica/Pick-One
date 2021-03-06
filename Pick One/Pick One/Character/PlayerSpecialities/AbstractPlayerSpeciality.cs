﻿using Microsoft.Xna.Framework;
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
        private PlayerSpriteContainer sprites;

        public AbstractPlayerSpeciality(PlayerSpriteContainer sprites, HitBox hitBox)
        {
            PlayerHitbox = hitBox;
            StandingSprite = sprites.StandingSprite;
            MovingLeft = sprites.MovingLeftSprite;
            MovingRight = sprites.MovingRightSprite;
            Jump = sprites.JumpingSprite;
            MidJump = sprites.MidJumpSprite;
            Landing = sprites.LandingSprite;
            Falling = sprites.FallingSprite;
            WallClimbRight = sprites.WallClimbRight;
            WallClimbLeft = sprites.WallClimbLeft;
            Poof = sprites.Poof;
        }
        public HitBox PlayerHitbox { get; set; }
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
        public Sprite WallClimbLeft { get; set; }
        public Sprite WallClimbRight { get; set; }
        public Sprite Falling { get; set; }
        public Sprite Poof { get; set; }
        public AbstractPlayerSpeciality NextTransform { get; set; }
        public AbstractPlayerSpeciality PrevTransform { get; set; }
        public PlayerState CurrentState { get; set; }
        public PlayerSpecialityEnum SpecialityName { get; set; }
        private int hitboxDiff = 3;
        public float Width
        {
            get; set;
        }
        public float Height { get; set; }
        public void UpdateHitBox(float x, float y)
        {
            PlayerHitbox.Update(x, y);
            switch (CurrentState)
            {
                case PlayerState.Standing:
                    Width = StandingSprite.Texture.Width / StandingSprite.Columns -1;
                    Height = StandingSprite.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.Jump:
                    Width = Jump.Texture.Width / Jump.Columns-1;
                    Height = Jump.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.MovingLeft:
                    Width = MovingLeft.Texture.Width / MovingLeft.Columns - 1;
                    Height = MovingLeft.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.MovingRight:
                    Width = MovingRight.Texture.Width / MovingRight.Columns - 1;
                    Height = MovingRight.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.WallClimbRight:
                    Width = WallClimbRight.Texture.Width / WallClimbRight.Columns - 1;
                    Height = WallClimbRight.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.WallClimbLeft:
                    Width = WallClimbLeft.Texture.Width / WallClimbLeft.Columns - 1;
                    Height = WallClimbLeft.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.Landing:
                    Width = Landing.Texture.Width / Landing.Columns - 1;
                    Height = Landing.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.MidJump:
                    Width = MidJump.Texture.Width / MidJump.Columns - 1;
                    Height = MidJump.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.Falling:
                    Width = Falling.Texture.Width / Falling.Columns - 1;
                    Height = Falling.Texture.Height - hitboxDiff;
                    break;
            }
            PlayerHitbox.HitBoxRectangle.Height = (int)Height;
            PlayerHitbox.HitBoxRectangle.Width = (int)Width;
        }
        internal void UpdateSprite()
        {
            switch (CurrentState)
            {
                case PlayerState.Standing:
                    StandingSprite.Update();
                    Width = StandingSprite.Texture.Width / StandingSprite.Columns - 1;
                    Height = StandingSprite.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.Jump:
                    Jump.Update();
                    Width = Jump.Texture.Width / Jump.Columns - 1;
                    Height = Jump.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.MovingLeft:
                    MovingLeft.Update();
                    Width = MovingLeft.Texture.Width / MovingLeft.Columns - 1;
                    Height = MovingLeft.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.MovingRight:
                    MovingRight.Update();
                    Width = MovingRight.Texture.Width / MovingRight.Columns - 1;
                    Height = MovingRight.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.WallClimbRight:
                    WallClimbRight.Update();
                    Width = WallClimbRight.Texture.Width / WallClimbRight.Columns - 1;
                    Height = WallClimbRight.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.WallClimbLeft:
                    WallClimbLeft.Update();
                    Width = WallClimbLeft.Texture.Width / WallClimbLeft.Columns - 1;
                    Height = WallClimbLeft.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.Landing:
                    Landing.Update();
                    Width = Landing.Texture.Width / Landing.Columns - 1;
                    Height = Landing.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.MidJump:
                    MidJump.Update();
                    Width = MidJump.Texture.Width / MidJump.Columns - 1;
                    Height = MidJump.Texture.Height - hitboxDiff;
                    break;
                case PlayerState.Falling:
                    Falling.Update();
                    Width = Falling.Texture.Width / Falling.Columns - 1;
                    Height = Falling.Texture.Height - hitboxDiff;
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
                case PlayerState.WallClimbRight:
                    WallClimbRight.Draw(spriteBatch, location);
                    break;
                case PlayerState.WallClimbLeft:
                    WallClimbLeft.Draw(spriteBatch, location);
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

        internal void DrawTransform(SpriteBatch spriteBatch, Vector2 location)
        {
            Poof.Draw(spriteBatch, location);
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
                case PlayerState.WallClimbRight:
                    WallClimbRight.currentFrame = 0;
                    break;
                case PlayerState.WallClimbLeft:
                    WallClimbLeft.currentFrame = 0;
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
