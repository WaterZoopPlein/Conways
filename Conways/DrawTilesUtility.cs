using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Conways
{
    public class DrawTilesUtility
    {
        public static List<List<Color>> GenerateRandomTiles(GraphicsDeviceManager graphics, Color[] tileColors)
        {
            var tileColorList = new List<List<Color>>();

            var rand = new Random();

            for (var i = 0; i < graphics.PreferredBackBufferHeight / 20; i++)
            {
                tileColorList.Add(new List<Color>());
                for (var j = 0; j < graphics.PreferredBackBufferWidth / 20; j++)
                {
                    var colorsIndex = rand.Next(tileColors.Length);
                    tileColorList[i].Add(tileColors[colorsIndex]);
                }
            }

            return tileColorList;
        }

        public static List<List<Color>> ResetTiles(GraphicsDeviceManager graphics)
        {
            var tileColorList = new List<List<Color>>();

            for (var i = 0; i < graphics.PreferredBackBufferHeight / 20; i++)
            {
                tileColorList.Add(new List<Color>());
                for (var j = 0; j < graphics.PreferredBackBufferWidth / 20; j++)
                {
                    tileColorList[i].Add(new Color(64, 64, 64));
                }
            }

            return tileColorList;
        }

        public static List<List<Color>> Checkerboard(GraphicsDeviceManager graphics)
        {
            var tileColorList = new List<List<Color>>();

            for (var i = 0; i < graphics.PreferredBackBufferHeight / 20; i++)
            {
                tileColorList.Add(new List<Color>());
                for (var j = 0; j < graphics.PreferredBackBufferWidth / 20; j++)
                {
                    tileColorList[i].Add((i + j) % 2 == 0 ? Color.Lime : new Color(64, 64, 64));
                }
            }

            return tileColorList;
        }
    }
}