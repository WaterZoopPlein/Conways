using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conways
{
    public class GridDrawer
    {
        private static GridDrawer _instance;

        public static GridDrawer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GridDrawer();
                }
                return _instance;
            }
        }

        public GridDrawer()
        {
            GridManagerInstance = GridManager.Instance;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < GridManagerInstance.GridHeight; i++)
            {
                for (var j = 0; j < GridManagerInstance.GridWidth; j++)
                {
                    var tilePosition = new Vector2(GridManagerInstance.TileTexture.Width * j, GridManagerInstance.TileTexture.Height * i);
                    var tileColor = GridManagerInstance.GetTileStatus(i, j) == TileStatus.Alive
                        ? Color.Lime
                        : new Color(64, 64, 64);
                    spriteBatch.Draw(GridManagerInstance.TileTexture, tilePosition, tileColor);
                }
            }
        }

        public GridManager GridManagerInstance { get; set; }
    }
}