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
        public WallClimb(PlayerSpriteContainer sprites) : base(sprites)
        {
            SpecialityName = PlayerSpecialityEnum.WallClimb;
            Movement = new MovementContainer();
            Movement.UpwardMovement = 2.0f;
            Movement.DownwardMovement = 2.0f;
            Movement.LeftMovement = 1.0f;
            Movement.RightMovement = 1.0f;
            Width = StandingSprite.Texture.Width / StandingSprite.Columns;
            Height = StandingSprite.Texture.Height;
            IsStretchable = false;
            IsClimbable = true;
        }
    }
}
