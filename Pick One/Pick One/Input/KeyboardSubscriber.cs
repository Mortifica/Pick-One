using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Pick_One.Input
{
    public class KeyboardSubscriber
    {
        public IInputSubscriber Subscriber { get; set; }
        //the keys that a subscriber wants to be notifed about
        public List<Keys> WatchedKeys { get; set; }
        public bool IsPaused { get; set; }
        
    }
}
