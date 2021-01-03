namespace Conways.Model
{
    public class ConwayModel
    {
        private static ConwayModel _instance;

        public static ConwayModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConwayModel();
                }
                return _instance;
            }
        }

        private ConwayModel()
        {

        }

        public void AttachTilesGrid(List<List<Tile>> tileGrid)
        {
            TileGrid = tileGrid;
        }

        public void NextGeneration()
        {
            Scan();
            Update();
        }

        private void Scan()
        {
            var rowsCount = TileGrid.Count;
            var columnsCount = TileGrid[0].Count;

            for (var rowIndex = 0; rowIndex < rowsCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++)
                {
                    var currentTile = TileGrid[rowIndex][columnIndex];
                    var livingNeighborCells = CountAdjacentOccupiedSeat(rowIndex, columnIndex, TileGrid);

                    if (currentTile.TileStatus == TileStatus.Alive)
                    {
                        if (!(livingNeighborCells == 2 || livingNeighborCells == 3))
                        {
                            currentTile.IsToggle = true;
                        }
                    }
                    else
                    {
                        if (livingNeighborCells == 3)
                        {
                            currentTile.IsToggle = true;
                        }
                    }
                    
                }
            }
        }

        private void Update()
        {
            var rowsCount = TileGrid.Count;
            var columnsCount = TileGrid[0].Count;

            for (var rowIndex = 0; rowIndex < rowsCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++)
                {
                    var currentTile = TileGrid[rowIndex][columnIndex];

                    if (!currentTile.IsToggle) continue;

                    currentTile.IsToggle = false;
                    currentTile.TileStatus = currentTile.TileStatus switch
                    {
                        TileStatus.Dead => TileStatus.Alive,
                        TileStatus.Alive => TileStatus.Dead,
                        _ => throw new ArgumentOutOfRangeException()
                    };

                }
            }

        }

        private List<List<Tile>> TileGrid { get; set; }

        private static int CountAdjacentOccupiedSeat(int currentRowNumber, int currentColumnNumber,
            IReadOnlyList<List<Tile>> seatLayout)
        {
            var adjacentTiles = GetAdjacentTiles(currentRowNumber, currentColumnNumber, seatLayout);

            var neighbourOccupiedSeats = adjacentTiles.Count(tile => tile.TileStatus == TileStatus.Alive);

            return neighbourOccupiedSeats;
        }

        private static IEnumerable<Tile> GetAdjacentTiles(int currentRowNumber, int currentColumnNumber,
            IReadOnlyList<List<Tile>> seatLayout)
        {
            var adjacentTiles = new List<Tile>();
            var rowsCount = seatLayout.Count;
            var columnsCount = seatLayout[0].Count;

            for (var dRow = (currentRowNumber > 0 ? -1 : 0); dRow <= (currentRowNumber < rowsCount - 1 ? 1 : 0); dRow++)
            {
                for (var dColumn = (currentColumnNumber > 0 ? -1 : 0);
                    dColumn <= (currentColumnNumber < columnsCount - 1 ? 1 : 0);
                    dColumn++)
                {
                    if (dRow != 0 || dColumn != 0)
                    {
                        adjacentTiles.Add(seatLayout[currentRowNumber + dRow][currentColumnNumber + dColumn]);
                    }
                }
            }

            return adjacentTiles;
        }

    }
}