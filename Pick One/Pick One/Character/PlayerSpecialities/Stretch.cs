using Pick_One.BasicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Character.PlayerSpecialities
{
    public class Stretch : AbstractPlayerSpeciality
    {
        public Stretch(PlayerSpriteContainer sprites, HitBox hitBox) : base(sprites, hitBox)
        {
            SpecialityName = PlayerSpecialityEnum.Stretch;
            Movement = new MovementContainer();
            Movement.UpwardMovement = 0.0f;
            Movement.DownwardMovement = 0.0f;
            Movement.LeftMovement = 1.0f;
            Movement.RightMovement = 1.0f;
            IsStretchable = true;
            IsClimbable = false;
        }
    }
}
