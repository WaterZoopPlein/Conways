using Conways.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Conways
{
    public class ConwayGameRunner : Game
    {
        private bool _isPausing;

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
            _isPausing = true;

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

            if (InputManager.Instance.IsKeyPressed(Keys.Space))
            {
                _isPausing = !_isPausing;
            }
            
            GridManager.Instance.Update(gameTime, _isPausing);

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
