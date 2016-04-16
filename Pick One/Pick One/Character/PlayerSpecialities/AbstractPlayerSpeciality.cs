using Pick_One.BasicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Character.PlayerSpecialities
{
    public abstract class AbstractPlayerSpeciality
    {
        public MovementContainer Movement { get; set; }
        public bool IsStretchable { get; set; }
        public bool IsClimbable { get; set; }
        public bool IsJumpable { get; set; }
        public abstract AbstractPlayerSpeciality GetPreviousTransform();
        public abstract AbstractPlayerSpeciality GetNextTransform();
        public Sprite StandingSprite { get; set; }
        public Sprite MovingLeft { get; set; }
        public Sprite MovingRight { get; set; }
        public Sprite Jump { get; set; }
        public Sprite WallClimbUp { get; set; }
        public Sprite WallClimbDown { get; set; }
        public Sprite Falling { get; set; }
    }
}
