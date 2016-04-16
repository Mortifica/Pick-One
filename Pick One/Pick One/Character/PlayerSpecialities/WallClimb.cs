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
        public WallClimb()
        {
            Movement = new MovementContainer();
            Movement.UpwardMovement = 1.0f;
            Movement.DownwardMovement = 1.0f;
            Movement.LeftMovement = 0.5f;
            Movement.RightMovement = 0.5f;
            IsStretchable = false;
            IsClimbable = true;
        }

        public override AbstractPlayerSpeciality GetNextTransform()
        {
            return new Normal();
        }

        public override AbstractPlayerSpeciality GetPreviousTransform()
        {
            return new Vertical();
        }
    }
}
