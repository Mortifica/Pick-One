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
        public Speed(PlayerSpriteContainer sprites) : base(sprites)
        {
            
            Movement = new MovementContainer();
            Movement.UpwardMovement = 0.0f;
            Movement.DownwardMovement = 0.0f;
            Movement.LeftMovement = 3.0f;
            Movement.RightMovement = 3.0f;
            Width = StandingSprite.Texture.Width / StandingSprite.Columns;
            Height = StandingSprite.Texture.Height;
            IsStretchable = false;
            IsClimbable = false;
        }
    }
}
