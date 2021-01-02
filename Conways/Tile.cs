namespace Conways
{
    public class Tile
    {
        public Tile(TileStatus tileStatus)
        {
            TileStatus = tileStatus;
        }

        public override string ToString()
        {
            return TileStatus.ToString();
        }

        public TileStatus TileStatus { get; set; }

    }
}