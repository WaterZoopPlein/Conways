using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Conways
{
    public class Game1 : Game
    {
        private bool _isPausing;

        private Color _firstClickColor = Color.Lime;
        private Color[] _tileColors;
        private List<List<Color>> _tileColorList;

        private Texture2D _tile;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseState _oldMouseState;
        private KeyboardState _oldKeyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 400;
            _graphics.PreferredBackBufferHeight = 300;
            _graphics.ApplyChanges();

            // TODO: Add your initialization logic here
            _isPausing = false;

            _tileColors = new[]
            {
                Color.Lime, new Color(64, 64, 64), new Color(64, 64, 64), new Color(64, 64, 64)
            };


            _tileColorList = DrawTilesUtility.GenerateRandomTiles(_graphics, _tileColors);

            _oldMouseState = Mouse.GetState();
            _oldKeyboardState = Keyboard.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _tile = Content.Load<Texture2D>("tile");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var newKeyboardState = Keyboard.GetState();
            var newMouseState = Mouse.GetState();

            if (newMouseState.LeftButton == ButtonState.Pressed)
            {
                var tileX = newMouseState.X < 0
                    ? 0
                    : newMouseState.X >= _graphics.PreferredBackBufferWidth
                        ? _graphics.PreferredBackBufferWidth / _tile.Width - 1
                        : newMouseState.X / _tile.Width;

                var tileY = newMouseState.Y < 0
                    ? 0
                    : newMouseState.Y >= _graphics.PreferredBackBufferHeight
                        ? _graphics.PreferredBackBufferHeight / _tile.Height - 1
                        : newMouseState.Y / _tile.Height;

                if (_oldMouseState.LeftButton == ButtonState.Released)
                {
                    _firstClickColor = _tileColorList[tileY][tileX] == new Color(64, 64, 64)
                        ? Color.Lime
                        : new Color(64, 64, 64);
                }

                _tileColorList[tileY][tileX] = _firstClickColor;
            }

            if (newKeyboardState.IsKeyDown(Keys.Delete) && _isPausing)
            {
                _tileColorList = DrawTilesUtility.ResetTiles(_graphics);
            }

            if (newKeyboardState.IsKeyDown(Keys.Space) && !_oldKeyboardState.IsKeyDown(Keys.Space))
            {
                _isPausing = !_isPausing;
            }

            if (newKeyboardState.IsKeyDown(Keys.R) && !_oldKeyboardState.IsKeyDown(Keys.R) && _isPausing)
            {
                _tileColorList = DrawTilesUtility.GenerateRandomTiles(_graphics, _tileColors);
            }

            if (newKeyboardState.IsKeyDown(Keys.C) && !_oldKeyboardState.IsKeyDown(Keys.C) && _isPausing)
            {
                _tileColorList = DrawTilesUtility.Checkerboard(_graphics);
            }

            _oldKeyboardState = newKeyboardState;
            _oldMouseState = newMouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            if (gameTime.TotalGameTime.Milliseconds % 1000 == 0 && !_isPausing)
            {
                _tileColorList = DrawTilesUtility.GenerateRandomTiles(_graphics, _tileColors);
            }


            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            for (var i = 0; i < _graphics.PreferredBackBufferHeight / _tile.Height; i++)
            {
                for (var j = 0; j < _graphics.PreferredBackBufferWidth / _tile.Width; j++)
                {
                    var tilePosition = new Vector2(_tile.Width * j, _tile.Height * i);
                    _spriteBatch.Draw(_tile, tilePosition, null, _tileColorList[i][j], 0f, default, Vector2.One,
                        SpriteEffects.None, 0f);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
