using Microsoft.Xna.Framework.Input;

namespace Conways
{
    public class InputManager
    {
        private static InputManager _instance;

        private MouseState _previousMouseState, _currentMouseState;
        private KeyboardState _previousKeyboardState, _currentKeyboardState;

        private InputManager()
        {

        }

        public void Update()
        {
            _previousMouseState = _currentMouseState;
            _previousKeyboardState = _currentKeyboardState;

            _currentKeyboardState = Keyboard.GetState();
            _currentMouseState = Mouse.GetState();

        }

        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputManager();
                }
                return _instance;
            }
        }

        public bool IsKeyPressed(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);
                
        }

        public bool IsKeyHeld(Keys key)
        {

            return _currentKeyboardState.IsKeyDown(key);

        }

        public bool IsMouseLeftButtonClicked()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed &&
                   _previousMouseState.LeftButton == ButtonState.Released;
        }

        public bool IsMouseLeftButtonHeld()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed;
        }

    }
}