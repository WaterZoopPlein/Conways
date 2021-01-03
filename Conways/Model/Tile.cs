namespace Conways.Model
{
    public class Tile
    {
        public Tile(TileStatus tileStatus)
        {
            TileStatus = tileStatus;
            IsToggle = false;
        }

        public override string ToString()
        {
            return TileStatus.ToString();
        }

        public TileStatus TileStatus { get; set; }
        public bool IsToggle { get; set; }
    }
}