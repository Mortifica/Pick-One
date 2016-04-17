using Pick_One.BasicClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Character
{
    public class PlayerSpriteContainer
    {
        public Sprite StandingSprite { get; set; }
        public Sprite JumpingSprite { get; set; }
        public Sprite MovingLeftSprite { get; set; }
        public Sprite MovingRightSprite { get; set; }
        public Sprite WallClimbUpSprite { get; set; }
        public Sprite WallClimbDownSprite { get; set; }
        public Sprite FallingSprite { get; set; }
        public Sprite LandingSprite { get; set; }
        public Sprite MidJumpSprite { get; set; }
    }
}
