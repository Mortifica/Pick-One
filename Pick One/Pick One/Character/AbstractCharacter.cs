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
        protected AbstractPlayerSpeciality PlayerSpeciality { get; set; }
        protected HitBox PlayerHitbox { get; set; }
        protected Location PlayerLocation { get; set; }
        public PlayerState CurrentState { get; set; }
        public void Draw(float x, float y, SpriteBatch spriteBatch)
        {
            PlayerSpeciality.StandingSprite.Draw(spriteBatch, new Vector2(x, y));
        }
    }
}
