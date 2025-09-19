using Microsoft.Xna.Framework.Input;

namespace HeartThrobFramework.Managers
{
    public class InputManager
    {
        public KeyboardState CurrentKeyboardState { get; private set; }
        public KeyboardState PreviousKeyboardState { get; private set; }
        public MouseState MouseState { get; private set; }
        public MouseState PreviousMouseState { get; private set; }

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            PreviousMouseState = MouseState;
            MouseState = Mouse.GetState();
        }
    }
}
