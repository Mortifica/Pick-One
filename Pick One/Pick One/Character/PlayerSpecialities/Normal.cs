using Microsoft.Xna.Framework.Graphics;
using Pick_One.BasicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Character.PlayerSpecialities
{
    public class Normal : AbstractPlayerSpeciality
    {
        public Normal(PlayerSpriteContainer sprites) : base(sprites)
        {
            SpecialityName = PlayerSpecialityEnum.Normal;
             Movement = new MovementContainer();
            Movement.UpwardMovement = 0.0f;
            Movement.DownwardMovement = 0.0f;
            Movement.LeftMovement = 2.0f;
            Movement.RightMovement = 2.0f;
            Width = StandingSprite.Texture.Width / StandingSprite.Columns;
            Height = StandingSprite.Texture.Height;
            IsStretchable = false;
            IsClimbable = false;
        }
        
    }
}
