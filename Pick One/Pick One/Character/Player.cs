﻿using Microsoft.Xna.Framework;
using Pick_One.BasicClasses;
using Pick_One.Character.PlayerSpecialities;
using Pick_One.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Pick_One.Camera;

namespace Pick_One.Character
{
    public class Player : AbstractCharacter, IInputSubscriber, ICameraFocus
    {
        private List<Keys> KeysForMovement { get; set; }
        private List<Keys> KeysForTransform { get; set; }

        public Vector2 Location
        {
            get
            {
                return new Vector2(PlayerLocation.XLocation, PlayerLocation.YLocation);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private Vector2 MovementVector;
        private bool IsTouchingWall;
        public Player(Vector2 initialLocation, List<PlayerSpriteContainer> container, CollisionManager collisionManager)
        {
            CollisionManager = collisionManager;
            MovementVector = new Vector2();
            PlayerLocation = new Location();
            PlayerLocation.XLocation = initialLocation.X;
            PlayerLocation.YLocation = initialLocation.Y;
            NormalSpeciality = new Normal(container[0]);
            SpeedSpeciality = new Speed(container[1]);
            StretchSpeciality = new Stretch(container[2]);
            VerticalSpeciality = new Vertical(container[3]);
            WallClimbSpeciality = new WallClimb(container[4]);
            NormalSpeciality.NextTransform = SpeedSpeciality;
            NormalSpeciality.PrevTransform = WallClimbSpeciality;
            CurrentPlayerSpeciality = NormalSpeciality;
            IsTouchingWall = false;
            PlayerHitbox = new HitBox(PlayerLocation.XLocation, PlayerLocation.YLocation, 32, 32);
            CurrentState = PlayerState.Standing;

            KeysForMovement = new List<Keys>
            {
                Keys.A,
                Keys.S,
                Keys.W,
                Keys.D,
                Keys.Space
            };
            KeysForTransform = new List<Keys>
            {
                Keys.Left,
                Keys.Right,
                Keys.D1,
                Keys.D2,
                Keys.D3,
                Keys.D4,
                Keys.D5
            };

        }
        public void Transform(Keys key)
        {
            AbstractPlayerSpeciality speciality = null;
            switch (key)
            {
                case Keys.Left:
                    speciality = CurrentPlayerSpeciality.PrevTransform;
                    break;
                case Keys.Right:
                    speciality = CurrentPlayerSpeciality.NextTransform;
                    break;
                case Keys.D1:
                    if (CurrentPlayerSpeciality.GetType() != typeof(Normal))
                    {
                        speciality = NormalSpeciality;
                    }
                    break;
                case Keys.D2:
                    if (CurrentPlayerSpeciality.GetType() != typeof(Speed))
                    {
                        speciality = SpeedSpeciality;
                    }
                    break;
                case Keys.D3:
                    if (CurrentPlayerSpeciality.GetType() != typeof(Stretch))
                    {
                        speciality = StretchSpeciality;
                    }
                    break;
                case Keys.D4:
                    if (CurrentPlayerSpeciality.GetType() != typeof(Vertical))
                    {
                        speciality = VerticalSpeciality;
                    }
                    break;
                case Keys.D5:
                    if (CurrentPlayerSpeciality.GetType() != typeof(WallClimb))
                    {
                        speciality = WallClimbSpeciality;
                    }
                    break;
            }
            CurrentPlayerSpeciality = speciality;
        }
        public MovementContainer GetMovement()
        {
            return CurrentPlayerSpeciality.Movement;
        }
        public bool IsStretchable()
        {
            return CurrentPlayerSpeciality.IsStretchable;
        }
        public bool IsClimbable()
        {
            return CurrentPlayerSpeciality.IsClimbable;
        }
        public HitBox GetHitbox()
        {
            return PlayerHitbox;
        }
        public bool CheckHitbox()
        {
            //Call CollisionManager
            return false;
        }
        public Location GetLocation()
        {
            return PlayerLocation;
        }
        public void Update()
        {
            //move, Update Sprite Animation, Transform, Update Hitbox

            CheckMovement();
            ApplyMovement();

            UpdateSprite();
            PlayerHitbox.Update(PlayerLocation.XLocation, PlayerLocation.YLocation);


            //Clear Objects that need to for next update
            MovementVector.X = 0;
            MovementVector.Y = 0;
        }

        private void CheckMovement()
        {
            var newRectangle = new Rectangle(PlayerHitbox.HitBoxRectangle.X, PlayerHitbox.HitBoxRectangle.Y, PlayerHitbox.HitBoxRectangle.Width, PlayerHitbox.HitBoxRectangle.Height);

            newRectangle.X += (int)MovementVector.X;
            newRectangle.Y += (int)MovementVector.Y;
            var checkResults = CollisionManager.CheckCollision(newRectangle);
            if (checkResults.Item1)//True if Hit something
            {
                foreach(var item in checkResults.Item2)
                {
                    if (MovementVector.X > 0)
                    {
                        if (newRectangle.X < item.Rectangle.X)
                        {
                            IsTouchingWall = true;
                            newRectangle.X = item.Rectangle.X - 32;
                        }
                    }
                    if (MovementVector.X < 0)
                    {
                        if (item.Rectangle.X < newRectangle.X)
                        {
                            IsTouchingWall = true;
                            newRectangle.X = item.Rectangle.X + 32;
                        }
                    }
                    if(item.Rectangle.Y > newRectangle.Y)
                    {
                        newRectangle.Y = item.Rectangle.Y - 32;
                    }
                    if (item.Rectangle.Y < newRectangle.Y)
                    {
                        newRectangle.Y = item.Rectangle.Y + 32;
                    }
                }
                MovementVector.X = PlayerHitbox.HitBoxRectangle.X - newRectangle.X;
                MovementVector.Y = PlayerHitbox.HitBoxRectangle.Y - newRectangle.Y;
                PlayerHitbox.HitBoxRectangle = newRectangle;
            }
        }

        private void UpdateSprite()
        {
            bool didTransition = TransitionState();
            if (!didTransition)
            {
                CurrentPlayerSpeciality.UpdateSprite();
            }
            else
            {
                CurrentPlayerSpeciality.SetState(CurrentState);
            }
        }

        private bool TransitionState()
        {
            if (MovementVector.X == 0.0f)
            {
                if (MovementVector.Y == 0.0f) //Standing
                {
                    if (CurrentState != PlayerState.Standing)
                    {
                        CurrentState = PlayerState.Standing;
                        return true;
                    }
                }
                else
                {
                    if (MovementVector.Y > 0.0f) // WallClimbUp OR Jumping
                    {
                        if (CurrentPlayerSpeciality.IsClimbable) //ClimbingUp
                        {
                            if (CurrentState != PlayerState.WallClimbUp)
                            {
                                CurrentState = PlayerState.WallClimbUp;
                                return true;
                            }
                        }
                        else
                        {
                            if (CurrentPlayerSpeciality.IsJumpable) //Jumping
                            {
                                if (CurrentState != PlayerState.Jump)
                                {
                                    CurrentState = PlayerState.Jump;
                                    return true;
                                }
                            }
                        }
                    }
                    else //Moving Down
                    {
                        if (CurrentPlayerSpeciality.IsClimbable) //ClimbingDown
                        {
                            if (CurrentState != PlayerState.WallClimbDown)
                            {
                                CurrentState = PlayerState.WallClimbDown;
                                return true;
                            }
                        }
                        else
                        {
                            if (CurrentState != PlayerState.Falling) // Falling
                            {
                                CurrentState = PlayerState.Falling;
                                return true;
                            }
                        }
                    }
                }

            }
            else
            {
                if (MovementVector.X > 0)//moving right
                {
                    if (CurrentState != PlayerState.MovingRight)
                    {
                        CurrentState = PlayerState.MovingRight;
                        return true;
                    }
                }
                else//moving left
                {
                    if (CurrentState != PlayerState.MovingLeft)
                    {
                        CurrentState = PlayerState.MovingLeft;
                        return true;
                    }
                }
            }
            return false;
        }

        private void ApplyMovement()
        {
            PlayerLocation.XLocation += MovementVector.X;
            PlayerLocation.YLocation += MovementVector.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentPlayerSpeciality.Draw(spriteBatch, new Vector2(PlayerLocation.XLocation, PlayerLocation.YLocation));
        }

        public void NotifyOfChange(List<KeyAction> actions, GameTime gameTime)
        {
            foreach (var action in actions)
            {
                if (KeysForMovement.Contains(action.Key) || KeysForTransform.Contains(action.Key))
                {
                    if (action.WasPressed || action.IsBeingHeld)
                    {
                        if (KeysForTransform.Contains(action.Key) && action.WasPressed)
                        {
                            Transform(action.Key);
                        }
                        else {
                            if (KeysForMovement.Contains(action.Key))
                            {
                                if (action.Key == Keys.Space && action.WasPressed)
                                {
                                    PlayerJump();
                                }
                                ProccessMovement(action);
                            }
                        }
                    }
                }
            }
        }

        private void ProccessMovement(KeyAction action)
        {
            switch (action.Key)
            {
                case Keys.W:
                    MoveVertically(CurrentPlayerSpeciality.Movement.UpwardMovement);
                    break;
                case Keys.S:
                    MoveVertically(-CurrentPlayerSpeciality.Movement.DownwardMovement);
                    break;
                case Keys.A:
                    MoveHorizontally(-CurrentPlayerSpeciality.Movement.LeftMovement);
                    break;
                case Keys.D:
                    MoveHorizontally(CurrentPlayerSpeciality.Movement.RightMovement);
                    break;
            }
        }

        private void MoveHorizontally(float movement)
        {
            MovementVector.X += movement;
        }
        private void MoveVertically(float movement)
        {
            if (IsTouchingWall)
                MovementVector.Y += movement;
        }
        public void SetIsTouchingWall(bool isTouchingWall)
        {
            IsTouchingWall = isTouchingWall;
        }
        private void PlayerJump()
        {
            if (CurrentPlayerSpeciality.Movement.UpwardMovement > 0.0f)
            {
                MovementVector.X += CurrentPlayerSpeciality.Movement.UpwardMovement;
            }
        }

        internal List<Keys> GetWatchedKeys()
        {
            var keysToReturn = new List<Keys>();
            keysToReturn.AddRange(KeysForMovement);
            keysToReturn.AddRange(KeysForTransform);
            return keysToReturn;
        }
    }
}
