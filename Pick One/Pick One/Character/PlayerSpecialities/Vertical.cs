using Pick_One.BasicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Character.PlayerSpecialities
{
    public class Vertical : AbstractPlayerSpeciality
    {

        public Vertical(PlayerSpriteContainer sprites)
        {
            StandingSprite = sprites.StandingSprite;
            MovingLeft = sprites.MovingLeftSprite;
            MovingRight = sprites.MovingRightSprite;
            Jump = sprites.StandingSprite;
            WallClimbDown = sprites.WallClimbDownSprite;
            WallClimbUp = sprites.WallClimbUpSprite;
            Movement = new MovementContainer();
            Movement.UpwardMovement = 1.0f;
            Movement.DownwardMovement = 1.0f;
            Movement.LeftMovement = 0.5f;
            Movement.RightMovement = 0.5f;
            IsStretchable = false;
            IsClimbable = false;
            IsJumpable = true;
        }
    }
}
