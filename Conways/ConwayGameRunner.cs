using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Conways
{
    public class ConwayGameRunner : Game
    {
        private bool _isPausing;

        private TileStatus _firstClickTileStatus = TileStatus.Alive;

        private Texture2D _tile;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public ConwayGameRunner()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = GraphicsManager.Instance.GraphicsWidth;
            _graphics.PreferredBackBufferHeight = GraphicsManager.Instance.GraphicsHeight;
            _graphics.ApplyChanges();

            // TODO: Add your initialization logic here
            _isPausing = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _tile = Content.Load<Texture2D>("tile");

            GridManager.Instance.CreateGrid(GraphicsManager.Instance.GraphicsWidth, GraphicsManager.Instance.GraphicsHeight, _tile);
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Instance.Update();

            if (InputManager.Instance.IsKeyPressed(Keys.Escape))
                Exit();

            var newMouseState = Mouse.GetState();

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

            if (InputManager.Instance.IsMouseLeftButtonClicked())
            {
                _firstClickTileStatus = GridManager.Instance.GetTileStatus(tileY, tileX) == TileStatus.Alive
                    ? TileStatus.Dead
                    : TileStatus.Alive;
            }

            if (InputManager.Instance.IsMouseLeftButtonHeld())
            {
                GridManager.Instance.SetTileStatus(tileY, tileX, _firstClickTileStatus);
            }

            if (InputManager.Instance.IsKeyHeld(Keys.Delete) && _isPausing)
            {
                GridManager.Instance.Clear();
            }

            if (InputManager.Instance.IsKeyPressed(Keys.Space))
            {
                _isPausing = !_isPausing;
            }

            if (InputManager.Instance.IsKeyPressed(Keys.R) && _isPausing)
            {
                GridManager.Instance.GenerateRandomGrid(0.25);
            }

            if (InputManager.Instance.IsKeyPressed(Keys.C) && _isPausing)
            {
                GridManager.Instance.GenerateCheckerboard();
            }

            if (gameTime.TotalGameTime.Milliseconds % 1000 == 0 && !_isPausing)
            {
                GridManager.Instance.GenerateRandomGrid(0.25);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            GridDrawer.Instance.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
