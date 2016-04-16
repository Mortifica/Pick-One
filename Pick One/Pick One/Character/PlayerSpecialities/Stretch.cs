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
        public Stretch()
        {
            Movement = new MovementContainer();
            Movement.UpwardMovement = 0.0f;
            Movement.DownwardMovement = 0.0f;
            Movement.LeftMovement = 0.5f;
            Movement.RightMovement = 0.5f;
            IsStretchable = true;
            IsClimbable = false;
        }

        public override AbstractPlayerSpeciality GetNextTransform()
        {
            return new Vertical();
        }

        public override AbstractPlayerSpeciality GetPreviousTransform()
        {
            return new Speed();
        }
    }
}
