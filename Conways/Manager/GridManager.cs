using System;
using System.Collections.Generic;
using Conways.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Conways.Manager
{
    public class GridManager
    {
        private static List<List<Tile>> _grid;
        private Random random;
        private TileStatus _firstClickTileStatus;
        private static GridManager _instance;

        public static GridManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GridManager();
                }
                return _instance;
            }
        }

        private GridManager(int seed = 1)
        {
            random = new Random(seed);
        }

        public void Update(GameTime gameTime, bool isPausing)
        {
            var tileX = InputManager.Instance.GetMouseXPosition() < 0
                ? 0
                : InputManager.Instance.GetMouseXPosition() / TileTexture.Width <= GridWidth - 1
                    ? InputManager.Instance.GetMouseXPosition() / TileTexture.Width
                    : GridWidth - 1;

            var tileY = InputManager.Instance.GetMouseYPosition() < 0
                ? 0
                : InputManager.Instance.GetMouseYPosition() / TileTexture.Height <= GridHeight - 1
                    ? InputManager.Instance.GetMouseYPosition() / TileTexture.Height
                    : GridHeight - 1;

            if (InputManager.Instance.IsMouseLeftButtonClicked())
            {
                _firstClickTileStatus = Instance.GetTileStatus(tileY, tileX) == TileStatus.Alive
                    ? TileStatus.Dead
                    : TileStatus.Alive;
            }

            if (InputManager.Instance.IsMouseLeftButtonHeld())
            {
                Instance.SetTileStatus(tileY, tileX, _firstClickTileStatus);
            }

            if (InputManager.Instance.IsKeyHeld(Keys.Delete) && isPausing)
            {
                Instance.Clear();
            }

            if (InputManager.Instance.IsKeyPressed(Keys.R) && isPausing)
            {
                Instance.GenerateRandomGrid(0.5);
            }

            if (InputManager.Instance.IsKeyPressed(Keys.C) && isPausing)
            {
                Instance.GenerateCheckerboard();
            }

            if (InputManager.Instance.IsKeyPressed(Keys.W) && isPausing || gameTime.TotalGameTime.Milliseconds % 2 == 0 && !isPausing)
            {
                Instance.ConwayNextGeneration();
            }
        }

        public void CreateGrid(int graphicWidth, int graphicsHeight, Texture2D tileTexture2D)
        {
            GridWidth = graphicWidth / tileTexture2D.Width;
            GridHeight = graphicsHeight / tileTexture2D.Height;
            TileTexture = tileTexture2D;


            _grid = new List<List<Tile>>();
            for (var i = 0; i < GridHeight; i++)
            {
                _grid.Add(new List<Tile>());
                for (var j = 0; j < GridWidth; j++)
                {
                    _grid[i].Add(new Tile(TileStatus.Dead));
                }
            }
        }

        public TileStatus GetTileStatus(int row, int column)
        {
            return _grid[row][column].TileStatus;
        }

        public void SetTileStatus(int row, int column, TileStatus tileStatus)
        {
            _grid[row][column].TileStatus = tileStatus;
        }

        public void GenerateRandomGrid(double livingDeadRatio)
        {
            for (var i = 0; i < GridHeight; i++)
            {
                for (var j = 0; j < GridWidth; j++)
                {
                    _grid[i][j].TileStatus = random.NextDouble() < livingDeadRatio ? TileStatus.Alive : TileStatus.Dead;
                }
            }
        }

        public void GenerateCheckerboard()
        {
            for (var i = 0; i < GridHeight; i++)
            {
                for (var j = 0; j < GridWidth; j++)
                {
                    _grid[i][j].TileStatus = (i + j) % 2 == 0 ? TileStatus.Alive : TileStatus.Dead;
                }
            }
        }

        public void Clear()
        {
            for (var i = 0; i < GridHeight; i++)
            {
                for (var j = 0; j < GridWidth; j++)
                {
                    _grid[i][j].TileStatus = TileStatus.Dead;
                }
            }
        }

        public void ConwayNextGeneration()
        {
            ConwayModel.Instance.AttachTilesGrid(_grid);
            ConwayModel.Instance.NextGeneration();
        }
        
        public Texture2D TileTexture { get; set; }
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        
    }
}