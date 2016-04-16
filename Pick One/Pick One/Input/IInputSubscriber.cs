using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pick_One.Input
{
    public interface IInputSubscriber
    {
        void NotifyOfChange(List<KeyAction> actions, GameTime gameTime);
    }
}
