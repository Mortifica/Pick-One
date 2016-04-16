using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pick_One.BasicClasses;
using Pick_One.Character.PlayerSpecialities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Character
{
    public abstract class AbstractCharacter
    {
        protected AbstractPlayerSpeciality CurrentPlayerSpeciality { get; set; }
        protected Normal NormalSpeciality { get; set; }
        protected Speed SpeedSpeciality { get; set; }
        protected Stretch StretchSpeciality { get; set; }
        protected Vertical VerticalSpeciality { get; set; }
        protected WallClimb WallClimbSpeciality { get; set; }
        protected HitBox PlayerHitbox { get; set; }
        protected Location PlayerLocation { get; set; }
        public PlayerState CurrentState { get; set; }
        public CollisionManager CollisionManager { get; set; }
        public void Draw(float x, float y, SpriteBatch spriteBatch, int size)
        {
            CurrentPlayerSpeciality.StandingSprite.Draw(spriteBatch, new Vector2(x, y), size);
        }
    }
}
