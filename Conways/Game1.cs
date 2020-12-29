using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Conways
{
    public class Game1 : Game
    {
        private Color[] _tileColors;
        private Random _rand;

        private Texture2D _tile;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<List<Color>> _tileColorList;

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
            _tileColors = new[]
            {
                Color.Lime, new Color(64, 64, 64), new Color(64, 64, 64), new Color(64, 64, 64)
            };
            _rand = new Random(727);

            _tileColorList = new List<List<Color>>();
            for (var i = 0; i < _graphics.PreferredBackBufferHeight / 20; i++)
            {
                _tileColorList.Add(new List<Color>());
                for (var j = 0; j < _graphics.PreferredBackBufferWidth / 20; j++)
                {
                    var colorsIndex = _rand.Next(_tileColors.Length);
                    _tileColorList[i].Add(_tileColors[colorsIndex]);
                }
            }

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            if (gameTime.TotalGameTime.Milliseconds % 1000 == 0)
            {
                _tileColorList.Clear();
                for (var i = 0; i < _graphics.PreferredBackBufferHeight / 20; i++)
                {
                    _tileColorList.Add(new List<Color>());
                    for (var j = 0; j < _graphics.PreferredBackBufferWidth / 20; j++)
                    {
                        var colorsIndex = _rand.Next(_tileColors.Length);
                        _tileColorList[i].Add(_tileColors[colorsIndex]);
                    }
                }
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
