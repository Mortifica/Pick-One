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
        public Normal()
        {
            Movement = new MovementContainer();
            Movement.UpwardMovement = 0.0f;
            Movement.DownwardMovement = 0.0f;
            Movement.LeftMovement = 1.0f;
            Movement.RightMovement = 1.0f;
            IsStretchable = false;
            IsClimbable = false;
        }

        public override AbstractPlayerSpeciality GetNextTransform()
        {
            return new Speed();
        }

        public override AbstractPlayerSpeciality GetPreviousTransform()
        {
            return new WallClimb();
        }
    }
}
