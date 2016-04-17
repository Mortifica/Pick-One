using Microsoft.Xna.Framework;
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
        public bool IsJumping { get; set; }
        public float JumpTime { get; set; }
        public float InitJumpTime { get; set; }

        private Vector2 gravity = Vector2.Zero;

        public Vector2 Location
        {
            get
            {
                return new Vector2(PlayerLocation.XLocation, PlayerLocation.YLocation);
            }

            set
            {
                PlayerLocation.XLocation = value.X;
                PlayerLocation.YLocation = value.Y;
            }
        }

        private Vector2 MovementVector;
        private bool IsTouchingWall;
        public Player(Vector2 initialLocation, List<PlayerSpriteContainer> container)
        {
            InitJumpTime = 30;
            // CollisionManager = collisionManager;
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
            PlayerHitbox = new HitBox(PlayerLocation.XLocation, PlayerLocation.YLocation, CurrentPlayerSpeciality.Height, CurrentPlayerSpeciality.Width);
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
                    //  if (CurrentPlayerSpeciality.GetType() != typeof(Normal))
                    // {
                    speciality = NormalSpeciality;
                    // }
                    break;
                case Keys.D2:
                    // if (CurrentPlayerSpeciality.GetType() != typeof(Speed))
                    // {
                    speciality = SpeedSpeciality;
                    //}
                    break;
                case Keys.D3:
                    //if (CurrentPlayerSpeciality.GetType() != typeof(Stretch))
                    // {
                    speciality = StretchSpeciality;
                    // }
                    break;
                case Keys.D4:
                    //  if (CurrentPlayerSpeciality.GetType() != typeof(Vertical))
                    // {
                    speciality = VerticalSpeciality;

                    // }
                    break;
                case Keys.D5:
                    //   if (CurrentPlayerSpeciality.GetType() != typeof(WallClimb))
                    // {
                    speciality = WallClimbSpeciality;
                    //  }
                    break;
            }
            var heightDiff = speciality.Height - CurrentPlayerSpeciality.Height;
            if (heightDiff > 0)
            {
                if (GameManager.Instance.GetBlocksAt(PlayerLocation.XLocation, PlayerLocation.YLocation - heightDiff, heightDiff, 1).Count() > 0)
                {
                    PlayerLocation.YLocation += heightDiff;
                }
                else
                {
                    PlayerLocation.YLocation -= heightDiff;

                }
            }
            var widthDiff = speciality.Width - CurrentPlayerSpeciality.Width;
            if (widthDiff > 0)
            {
                if (GameManager.Instance.GetBlocksAt(PlayerLocation.XLocation + CurrentPlayerSpeciality.Width + widthDiff, PlayerLocation.YLocation, 1, widthDiff).Count() > 0)
                {
                    PlayerLocation.XLocation -= widthDiff;
                }
                //else
                //{
                //    if (GameManager.Instance.GetBlocksAt(PlayerLocation.XLocation - widthDiff, PlayerLocation.YLocation, 1, widthDiff).Count() > 0)
                //    {
                //        PlayerLocation.XLocation += widthDiff;
                //    }

                //}
            }
            CurrentPlayerSpeciality = speciality;
            CurrentPlayerSpeciality.CurrentState = CurrentState;
            PlayerHitbox.HitBoxRectangle.Width = (int)CurrentPlayerSpeciality.Width;
            PlayerHitbox.HitBoxRectangle.Height = (int)CurrentPlayerSpeciality.Height;

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
        public void Update(GameTime gameTime)
        {
            if (CurrentPlayerSpeciality != StretchSpeciality)
                ApplyGravity(gameTime);


            //move, Update Sprite Animation, Transform, Update Hitbox
            if (IsJumping)
            {
                JumpTime++;
                if (JumpTime < 50)
                {
                    if (JumpTime > InitJumpTime)
                        MovementVector.Y -= (CurrentPlayerSpeciality.Movement.UpwardMovement / (JumpTime - InitJumpTime));
                }
                else
                {
                    JumpTime = 0;
                    IsJumping = false;
                }
            }
            CheckMovement();
            ApplyMovement();

            UpdateSprite();
            PlayerHitbox.Update(PlayerLocation.XLocation, PlayerLocation.YLocation);

            //Clear Objects that need to for next update
            MovementVector.X = 0;
            MovementVector.Y = 0;
            IsTouchingWall = false;
        }

        private int gravityStrength = 0;
        private void ApplyGravity(GameTime gameTime)
        {
            if ((gameTime.TotalGameTime.Ticks % 10) == 0)
                if (gravityStrength < 3)
                    gravityStrength++;
            MovementVector.Y += gravityStrength; // Gravity
        }

        private void CheckMovement()
        {
            var newRectangle = new Rectangle(PlayerHitbox.HitBoxRectangle.X, PlayerHitbox.HitBoxRectangle.Y, PlayerHitbox.HitBoxRectangle.Width, PlayerHitbox.HitBoxRectangle.Height);
            var newXRectangle = new Rectangle(PlayerHitbox.HitBoxRectangle.X, PlayerHitbox.HitBoxRectangle.Y, PlayerHitbox.HitBoxRectangle.Width, PlayerHitbox.HitBoxRectangle.Height);
            var newYRectangle = new Rectangle(PlayerHitbox.HitBoxRectangle.X, PlayerHitbox.HitBoxRectangle.Y, PlayerHitbox.HitBoxRectangle.Width, PlayerHitbox.HitBoxRectangle.Height);

            newRectangle.X += (int)MovementVector.X;
            newRectangle.Y += (int)MovementVector.Y;

            newXRectangle.X += (int)MovementVector.X;

            newYRectangle.Y += (int)MovementVector.Y;

            var checkResults = GameManager.Instance.CheckCollision(newRectangle);
            var checkXResults = GameManager.Instance.CheckCollision(newXRectangle);
            var checkYResults = GameManager.Instance.CheckCollision(newYRectangle);

            if (!checkResults.Item1)
            {
                PlayerHitbox.HitBoxRectangle = newRectangle;
                MovementVector.X = newRectangle.X - PlayerLocation.XLocation;

                MovementVector.Y = newRectangle.Y - PlayerLocation.YLocation;
            }
            else if (!checkXResults.Item1)
            {
                PlayerHitbox.HitBoxRectangle = newXRectangle;
                MovementVector.X = newRectangle.X - PlayerLocation.XLocation;

                MovementVector.Y = 0;
                gravityStrength = 0;

            }
            else if (!checkYResults.Item1)
            {
                PlayerHitbox.HitBoxRectangle = newYRectangle;

                MovementVector.X = 0;

                MovementVector.Y = newRectangle.Y - PlayerLocation.YLocation;

            }
            else
            {
                MovementVector.X = 0;

                MovementVector.Y = 0;
            }

            foreach (var tile in checkResults.Item2)
            {
                if (tile.Type == Levels.Tile.TileTypes.EndPosition)
                {
                    // Move to the next level.
                    GameManager.Instance.EndLevel();
                    Location = GameManager.Instance.GetPlayerStartingLocation();
                }
            }

            //if (checkResults.Item1)//True if Hit something
            //{

            //    foreach(var item in checkResults.Item2)
            //    {





            //        if (MovementVector.X > 0)
            //        {
            //            if (newRectangle.X + CurrentPlayerSpeciality.Width > item.Rectangle.X)
            //            {
            //                IsTouchingWall = true;
            //                newRectangle.X = (int)PlayerLocation.XLocation + (item.Rectangle.X - ((int)PlayerLocation.XLocation + (int)CurrentPlayerSpeciality.Width));
            //                if (!newRectangle.Intersects(item.Rectangle))
            //                {
            //                    updated = true;
            //                }
            //            }
            //            //if (newRectangle.X + 32 > item.Rectangle.X)
            //            //{
            //            //    IsTouchingWall = true;

            //            //    newRectangle.X = item.Rectangle.X - 32;
            //            //}
            //        }
            //        if (!updated && MovementVector.X < 0 )
            //        {
            //            if (newRectangle.X < item.Rectangle.X + item.Rectangle.Width)
            //            {
            //                IsTouchingWall = true;
            //                newRectangle.X = ((int)item.Rectangle.X + (int)item.Rectangle.Width);
            //            }
            //            if (!newRectangle.Intersects(item.Rectangle))
            //            {
            //                updated = true;
            //            }
            //        }
            //        if (!updated && MovementVector.Y > 0)
            //        {
            //            if (newRectangle.Y + CurrentPlayerSpeciality.Height > item.Rectangle.Y )
            //            {
            //                newRectangle.Y = (int)PlayerLocation.YLocation + (item.Rectangle.Y - ((int)PlayerLocation.YLocation + (int)CurrentPlayerSpeciality.Height));
            //            }
            //            if (!newRectangle.Intersects(item.Rectangle))
            //            {
            //                updated = true;
            //            }
            //        }
            //        if (!updated && MovementVector.Y < 0)
            //        {
            //            if (newRectangle.Y < item.Rectangle.Y + item.Rectangle.Height)
            //            {
            //                newRectangle.Y = ((int)item.Rectangle.Y + (int)item.Rectangle.Height);
            //            }
            //            if (!newRectangle.Intersects(item.Rectangle))
            //            {
            //                updated = true;
            //            }
            //        }
            //        checkResults = CollisionManager.CheckCollision(newRectangle);
            //        if(checkResults.Item1)
            //        {
            //            break;
            //        }
            //        updated = false;
            //    }


            // //   }
            //    PlayerHitbox.HitBoxRectangle = newRectangle;

            //}
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
            if (IsJumping)
            {
                if (CurrentPlayerSpeciality.IsJumpable) //Jumping
                {
                    if (JumpTime < InitJumpTime) // InitJump
                    {
                        if (CurrentState != PlayerState.Jump)
                        {
                            CurrentState = PlayerState.Jump;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (CurrentState != PlayerState.MidJump)
                        {
                            CurrentState = PlayerState.MidJump;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

            }
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
                    if (MovementVector.Y < 0.0f) // WallClimbUp OR Jumping
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
                            //if (CurrentPlayerSpeciality.IsJumpable) //Jumping
                            //{
                            //    if (CurrentState != PlayerState.Jump)
                            //    {
                            //        CurrentState = PlayerState.Jump;
                            //        return true;
                            //    }
                            //}
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
            Console.WriteLine(CurrentState);
            //spriteBatch.DrawString(new SpriteFont(), "State:" + CurrentState, new Vector2(1, 1), Color.Black);
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
            bool blockLeft = GameManager.Instance.GetBlocksAt(PlayerLocation.XLocation - 1, PlayerLocation.YLocation, CurrentPlayerSpeciality.Height, 1).Count() > 0;
            bool blockRight = GameManager.Instance.GetBlocksAt(PlayerLocation.XLocation + CurrentPlayerSpeciality.Width + 1, PlayerLocation.YLocation, CurrentPlayerSpeciality.Height, 1).Count() > 0;

            switch (action.Key)
            {
                //case Keys.W:
                //    MoveVertically(-CurrentPlayerSpeciality.Movement.UpwardMovement);
                //    break;
                //case Keys.S:
                //    MoveVertically(CurrentPlayerSpeciality.Movement.DownwardMovement);
                //    break;
                case Keys.A:
                    MoveHorizontally(-CurrentPlayerSpeciality.Movement.LeftMovement);
                    if (IsClimbable() && (blockLeft))
                    {
                        MoveVertically(-CurrentPlayerSpeciality.Movement.UpwardMovement);
                    }
                    break;
                case Keys.D:
                    MoveHorizontally(CurrentPlayerSpeciality.Movement.RightMovement);
                    if (IsClimbable() && (blockRight))
                    {
                        MoveVertically(-CurrentPlayerSpeciality.Movement.UpwardMovement);
                    }
                    break;
            }
        }

        private void MoveHorizontally(float movement)
        {
            MovementVector.X += movement;
        }
        private void MoveVertically(float movement)
        {
            MovementVector.Y += movement - 2;
        }
        public void SetIsTouchingWall(bool isTouchingWall)
        {
            IsTouchingWall = isTouchingWall;
        }
        private void PlayerJump()
        {
            if (CurrentPlayerSpeciality.IsJumpable)
            {
                IsJumping = true;
            }
            //if (CurrentPlayerSpeciality.Movement.UpwardMovement > 0.0f)
            //{
            //    MovementVector.Y -= CurrentPlayerSpeciality.Movement.UpwardMovement;
            //}
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
