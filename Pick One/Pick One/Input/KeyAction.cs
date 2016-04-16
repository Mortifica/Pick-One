using Microsoft.Xna.Framework.Input;

namespace Pick_One.Input
{
    public class KeyAction
    {
        //Key defines what Key the actions are being taken aginst
        public Keys Key { get; set; }

        //if the Key was previously not pressed and now is
        private bool _wasPressed;
        public bool WasPressed
        {
            get
            {
                return _wasPressed;
            }

            set
            {
                _wasPressed = value;
                HasNoAction = false;
            }

        }
        //if the key was previously pressed and is now not
        private bool _wasReleased;
        public bool WasReleased
        {
            get
            {
                return _wasReleased;
            }

            set
            {
                _wasReleased = value;
                HasNoAction = false;
            }

        }
        //if the key was previously pressed and still is
        private bool _isBeingHeld;
        public bool IsBeingHeld
        {
            get
            {
                return _isBeingHeld;
            }

            set
            {
                _isBeingHeld = value;
                HasNoAction = false;
            }

        }
        //if the key was previouslu not pressed and still is
        public bool HasNoAction { get; set; }

        public KeyAction(Keys key)
        {
            WasPressed = false;
            WasReleased = false;
            IsBeingHeld = false;
            HasNoAction = true;
        }

    }
}
