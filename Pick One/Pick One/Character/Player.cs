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

namespace Pick_One.Character
{
    public class Player : AbstractCharacter, IInputSubscriber
    {
        private List<Keys> KeysForMovement { get; set; }
        private List<Keys> KeysForTransform { get; set; }
        private Vector2 MovementVector;
        private bool IsTouchingWall;
        public Player()
        {
            MovementVector = new Vector2();
            PlayerSpeciality = new Normal();
            IsTouchingWall = false;
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
                Keys.D4
            };

        }
        public void Transform(Keys key)
        {
            AbstractPlayerSpeciality speciality = null;
            switch (key)
            {
                case Keys.Left:
                    speciality = PlayerSpeciality.GetPreviousTransform();
                    break;
                case Keys.Right:
                    speciality = PlayerSpeciality.GetNextTransform();
                    break;
                case Keys.D1:
                    if (PlayerSpeciality.GetType() != typeof(Normal))
                    {
                        speciality = new Normal();
                    }
                    break;
                case Keys.D2:
                    if (PlayerSpeciality.GetType() != typeof(Speed))
                    {
                        speciality = new Speed();
                    }
                    break;
                case Keys.D3:
                    if (PlayerSpeciality.GetType() != typeof(Stretch))
                    {
                        speciality = new Stretch();
                    }
                    break;
                case Keys.D4:
                    if (PlayerSpeciality.GetType() != typeof(Vertical))
                    {
                        speciality = new Vertical();
                    }
                    break;
                case Keys.D5:
                    if (PlayerSpeciality.GetType() != typeof(WallClimb))
                    {
                        speciality = new WallClimb();
                    }
                    break;
            }
            PlayerSpeciality = speciality;
        }
        public MovementContainer GetMovement()
        {
            return PlayerSpeciality.Movement;
        }
        public bool IsStretchable()
        {
            return PlayerSpeciality.IsStretchable;
        }
        public bool IsClimbable()
        {
            return PlayerSpeciality.IsClimbable;
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
            ApplyMovement();
            UpdateSprite();
            PlayerHitbox.Update(PlayerLocation.XLocation, PlayerLocation.YLocation);


            //Clear Objects that need to for next update
            MovementVector.X = 0;
            MovementVector.Y = 0;
        }

        private void UpdateSprite()
        {
            bool didTransition = TransitionState();
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
                        if (PlayerSpeciality.IsClimbable) //ClimbingUp
                        {
                            if (CurrentState != PlayerState.WallClimbUp)
                            {
                                CurrentState = PlayerState.WallClimbUp;
                                return true;
                            }
                        }
                        else
                        {
                            if (PlayerSpeciality.IsJumpable) //Jumping
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
                        if (PlayerSpeciality.IsClimbable) //ClimbingDown
                        {
                            CurrentState = PlayerState.WallClimbDown;
                            return true;
                        }
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
            base.Draw(PlayerLocation.XLocation, PlayerLocation.YLocation, spriteBatch);
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
                    MoveVertically(PlayerSpeciality.Movement.UpwardMovement);
                    break;
                case Keys.S:
                    MoveVertically(-PlayerSpeciality.Movement.DownwardMovement);
                    break;
                case Keys.A:
                    MoveHorizontally(-PlayerSpeciality.Movement.LeftMovement);
                    break;
                case Keys.D:
                    MoveHorizontally(PlayerSpeciality.Movement.RightMovement);
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
            if (PlayerSpeciality.Movement.UpwardMovement > 0.0f)
            {
                MovementVector.X += PlayerSpeciality.Movement.UpwardMovement;
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
