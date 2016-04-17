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

        public Vertical(PlayerSpriteContainer sprites, HitBox hitBox) : base(sprites, hitBox)
        {
            SpecialityName = PlayerSpecialityEnum.Vertical;
            Movement = new MovementContainer();
            Movement.UpwardMovement = 20.0f;
            Movement.DownwardMovement = 0.0f;
            Movement.LeftMovement = 0.0f;
            Movement.RightMovement = 0.0f;
            Width = StandingSprite.Texture.Width / StandingSprite.Columns;
            Height = StandingSprite.Texture.Height;
            IsStretchable = false;
            IsClimbable = false;
            IsJumpable = true;
        }
    }
}
