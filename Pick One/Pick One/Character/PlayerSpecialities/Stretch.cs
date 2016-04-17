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
        public Stretch(PlayerSpriteContainer sprites) : base(sprites)
        {
            
            Movement = new MovementContainer();
            Movement.UpwardMovement = 0.0f;
            Movement.DownwardMovement = 0.0f;
            Movement.LeftMovement = 1.0f;
            Movement.RightMovement = 1.0f;
            Width = StandingSprite.Texture.Width / StandingSprite.Columns;
            Height = StandingSprite.Texture.Height;
            IsStretchable = true;
            IsClimbable = false;
        }
    }
}
