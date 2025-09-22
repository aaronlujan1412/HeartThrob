using Microsoft.Xna.Framework.Input;

namespace HeartThrobFramework.Managers
{
    public class InputManager
    {
        public KeyboardState CurrentKeyboardState { get; private set; }
        public KeyboardState PreviousKeyboardState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }
        public MouseState PreviousMouseState { get; private set; }

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
            PreviousMouseState = new MouseState(
                CurrentMouseState.X,
                CurrentMouseState.Y,
                CurrentMouseState.ScrollWheelValue,
                CurrentMouseState.LeftButton,
                CurrentMouseState.MiddleButton,
                CurrentMouseState.RightButton,
                CurrentMouseState.XButton1,
                CurrentMouseState.XButton2);
        }
    }
}
