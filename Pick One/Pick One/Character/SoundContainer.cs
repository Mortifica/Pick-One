using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pick_One.Character
{
    public sealed class SoundContainer
    {
        private static readonly Lazy<SoundContainer> lazy = new Lazy<SoundContainer>(() => new SoundContainer());

        public static SoundContainer Instance { get { return lazy.Value; } }

        public SoundEffect Death { get; set; }
        public SoundEffect CountDown { get; set; }
        public SoundEffect Go { get; set; }
        public SoundEffect Finish { get; set; }
        public SoundEffect Hover { get; set; }
        public SoundEffect Squish1 { get; set; }
        public SoundEffect Squish2 { get; set; }
        //public SoundEffect Squish3 { get; set; }
        public SoundEffect Jump { get; set; }
        public SoundEffect Poof { get; set; }
        public SoundEffect MenuSelect { get; set; }
        public SoundEffect Select { get; set; }


        public Song Tutorial { get; set; }


        public Song LevelTheme { get; set; }


        private SoundContainer()
        {
        }

    }
}
