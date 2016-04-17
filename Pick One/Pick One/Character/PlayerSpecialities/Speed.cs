using Pick_One.BasicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Character.PlayerSpecialities
{
    public class Speed : AbstractPlayerSpeciality
    {
        public Speed(PlayerSpriteContainer sprites, HitBox hitBox) : base(sprites, hitBox)
        {
            SpecialityName = PlayerSpecialityEnum.Speed;
            Movement = new MovementContainer();
            Movement.UpwardMovement = 0.0f;
            Movement.DownwardMovement = 0.0f;
            Movement.LeftMovement = 3.0f;
            Movement.RightMovement = 3.0f;
            IsStretchable = false;
            IsClimbable = false;
        }
    }
}
