using Pick_One.BasicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Character.PlayerSpecialities
{
    public class WallClimb : AbstractPlayerSpeciality
    {
        public WallClimb(PlayerSpriteContainer sprites, HitBox hitBox) : base(sprites, hitBox)
        {
            SpecialityName = PlayerSpecialityEnum.WallClimb;
            Movement = new MovementContainer();
            Movement.UpwardMovement = 2.0f;
            Movement.DownwardMovement = 2.0f;
            Movement.LeftMovement = 1.0f;
            Movement.RightMovement = 1.0f;
            IsStretchable = false;
            IsClimbable = true;
        }
    }
}
