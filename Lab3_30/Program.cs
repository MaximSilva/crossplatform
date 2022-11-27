namespace Lab3_30
{
    /// <summary>
    /// Стан корабля
    /// </summary>
    enum ShipState
    {
        Whole,      //цілий корабель
        Broken,     //підбитий корабель
        Destroyed   //знищений корабель
    }

    public class Program
    {
        public static string InputFilePath = @"..\..\input.txt";
        public static string OutputFilePath = @"..\..\output.txt";
        private static ShipState currentShipState = ShipState.Whole;
        static void Main(string[] args)
        {
            FileInfo outputFileInfo = new FileInfo(OutputFilePath);
            var inputData = File.ReadLines(InputFilePath);
            var n_m = inputData.First().Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
            var game = inputData.Skip(1).Select(row => row.ToCharArray()).ToArray();

            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                if (n_m[0] < 1 || n_m[0] > 1000 || n_m[1] < 1 || n_m[1] > 1000)
                {
                    streamWriter.WriteLine("Out of range exception!");
                }
                else
                {
                    streamWriter.WriteLine(GetShipCountStat(game));
                }
            }
        }

        /// <summary>
        /// Отримати статистику по цілим/підбитим/знищеним кораблям
        /// </summary>
        /// <param name="game">Ігрове поле</param>
        private static string GetShipCountStat(char[][] game)
        {
            int wholeShipsCount = 0, brokenShipsCount = 0, destroyedShipsCount = 0;
            for(int i = 0; i < game.Length; i++)
            {
                for(int j = 0; j < game[i].Length; j++)
                {
                    if ("SX".Contains(game[i][j]))
                    {
                        currentShipState = game[i][j] == 'S' ? ShipState.Whole : ShipState.Destroyed;
                        clearShipInGame((i, j), game);
                        switch (currentShipState)
                        {
                            case ShipState.Whole:
                                wholeShipsCount++;
                                break;
                            case ShipState.Destroyed:
                                destroyedShipsCount++;
                                break;
                            case ShipState.Broken:
                                brokenShipsCount++;
                                break;
                        }
                    }
                }
            }
            return $"{wholeShipsCount} {brokenShipsCount} {destroyedShipsCount}";

        }

        /// <summary>
        /// Заповнити усі елементи групи іншим значенням (було значення 'B' - стане 'G')
        /// </summary>
        /// <param name="pos">Поточна позиція в групі</param>
        /// <param name="game">Ігрове поле</param>
        private static void clearShipInGame((int row, int col) pos, char[][] game)
        {
            if (currentShipState != ShipState.Broken)
            {
                if ( (game[pos.row][pos.col] == 'S' && currentShipState == ShipState.Destroyed) || (game[pos.row][pos.col] == 'X' && currentShipState == ShipState.Whole) )
                {
                    currentShipState= ShipState.Broken;
                }
            }

            game[pos.row][pos.col] = 'G';
            if (pos.row != 0 && "SX".Contains(game[pos.row - 1][pos.col]))
            {
                clearShipInGame((pos.row - 1, pos.col), game);
            }
            if (pos.col != 0 && "SX".Contains(game[pos.row][pos.col - 1]))
            {
                clearShipInGame((pos.row, pos.col - 1), game);
            }
            if (pos.row != game.Length - 1 && "SX".Contains(game[pos.row + 1][pos.col]))
            {
                clearShipInGame((pos.row + 1, pos.col), game);
            }
            if (pos.col != game.Length - 1 && "SX".Contains(game[pos.row][pos.col + 1]))
            {
                clearShipInGame((pos.row, pos.col + 1), game);
            }
        }

    }
}