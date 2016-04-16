using Microsoft.Xna.Framework.Input;

namespace Pick_One.Input
{
    public class KeyboardChangeState
    {
        private KeyboardState PreviousState { get; set; }
        private KeyboardState CurrentState { get; set; }

        public void SetState(KeyboardState keyboardState)
        {
            PreviousState = CurrentState;
            CurrentState = keyboardState;
        }

        public bool WasKeyPressed(Keys key)
        {
            if (PreviousState == null) return false;
            if (PreviousState.IsKeyUp(key) && CurrentState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }
        public bool WasKeyReleased(Keys key)
        {
            if (PreviousState == null) return false;
            if (PreviousState.IsKeyDown(key) && CurrentState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }
        public bool IsKeyBeingHeld(Keys key)
        {
            if (PreviousState == null) return false;
            if (PreviousState.IsKeyDown(key) && CurrentState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

    }
}
