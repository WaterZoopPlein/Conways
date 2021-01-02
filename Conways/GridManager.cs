using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Conways
{
    public class GridManager
    {
        private static List<List<Tile>> _grid;
        private Random random;

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
        
        public Texture2D TileTexture { get; set; }
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }

    }
}