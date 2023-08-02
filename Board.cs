using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBattle
{
    public class Board
    {
        //Board size
        static int rows = 10;
        static int cols = 10;

        //Matrix bidimensional to represents the board
        private char[,] _board = new char[rows, cols];

        private Ship[] playerShips = new Ship[5];
        private Ship[] computerShips = new Ship[5];

        //Cells type
        private const char _emptyCell = '~';
        private const char _shipCell = 'O';
        private const char _hitCell = 'X';
        private const char _missCell = 'M';

        private Random random = new Random();   

        //Generate Board
        public Board() 
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    _board[row, col] = _emptyCell;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                playerShips[i] = new Ship(4);
                computerShips[i] = new Ship(4);
            }
        }

        private List<(int, int)> targets = new List<(int, int)>();

        public bool HasTargets()
        {
            return targets.Count > 0;
        }

        public (int, int) GetNextTarget()
        {
            // Obtenha o próximo alvo a ser atacado (alvos na vertical e horizontal do último acerto)
            (int row, int col) lastTarget = targets[targets.Count - 1];
            targets.RemoveAt(targets.Count - 1);

            // Verifique as posições vizinhas (vertical e horizontal)
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };
            for (int i = 0; i < 4; i++)
            {
                int newRow = lastTarget.Item1 + dx[i];
                int newCol = lastTarget.Item2 + dy[i];

                if (IsValidAttack(newRow, newCol))
                {
                    targets.Add((newRow, newCol));
                }
            }

            return targets[targets.Count - 1];
        }

        public void PlaceShipsRandomly()
        {
            foreach (Ship ship in playerShips)
            {
                PlaceShipRandomly(ship);
            }

            foreach (Ship ship in computerShips)
            {
                PlaceShipRandomly(ship);
            }
        }

        //Method to insert ships randomly on the board
        public void PlaceShipRandomly(Ship ship)
        {
            Random random = new Random();

            while (true) 
            {
                ShipOrientation orientation = (ShipOrientation)random.Next(2);

                if (orientation == ShipOrientation.Horizontales)
                {
                    int row = random.Next(0, rows);
                    int col = random.Next(0, cols - ship.Size);

                    bool isValidPosition = true;
                    for (int c = col; c < col; c++)
                    {
                        if (_board[row, c] != _emptyCell)
                        {
                            isValidPosition = false;
                            break;
                        }
                    }

                    if (isValidPosition)
                    {
                        for (int c = col; c < col; c++)
                        {
                            _board[row, c] = _shipCell;
                        }
                        ship.Orientation = ShipOrientation.Horizontales;
                        ship.Row = row;
                        ship.Col = col;
                        break;
                    }
                }
                else
                {
                    int row = random.Next(0, rows - ship.Size);
                    int col = random.Next(0, cols);

                    bool isValidPosition = true;
                    for (int r = row; r < row; r++)
                    {
                        if (_board[row, r] != _emptyCell)
                        {
                            isValidPosition = false;
                            break;
                        }
                    }

                    if (isValidPosition)
                    {
                        for (int r = row; r < row; r++)
                        {
                            _board[row, r] = _shipCell;
                        }
                        ship.Orientation = ShipOrientation.Verticales;
                        ship.Row = row;
                        ship.Col = col;
                        break;
                    }
                }
            }
        }

        public void SetupShips()
        {
            for (int i = 0; i < playerShips.Length; i++)
            {
                PlaceShipRandomly(playerShips[i]);
            }

            for (int i = 0; i < computerShips.Length; i++)
            {
                PlaceShipRandomly(computerShips[i]);
            }
        }

        //Method to check if the Attack is valid
        public bool IsValidAttack(int row, int col)
        {
            if (row < 0 || row >= rows || col <0 || col >= cols)
            {
                return false;
            }
            //Check if the cell have been already atacked
            return _board[row, col] != _hitCell && _board[row, col] != _missCell;
        }

        //Method to make the attack and verify if a ship was hit
        public bool Attack(int row, int col)
        {
            if (playerShips.Length > 0)
            {
                //Check if a player's ship was hit
                foreach (Ship ship in playerShips)
                {
                    if (ship.Hit(row, col))
                    {
                        _board[row, col] = _hitCell;
                        return true;
                    }
                }
            }

            if (computerShips.Length > 0)
            {
                foreach (Ship ship in computerShips)
                {
                    if (ship.Hit(row, col))
                    {
                        _board[row, col] = _hitCell;

                        // Armazene o alvo atingido para futuros ataques
                        targets.Add((row, col));

                        return true;
                    }
                }
            }

            _board[row, col] = _missCell;
            return false;
        }

        public bool IsGameOver()
        {
            return playerShips.All(ship => ship.IsDestroyed()) || computerShips.All(ship => ship.IsDestroyed());
        }

        public bool AreAllComputerShipsDestroyed()
        {
            return computerShips.All(ship => ship.IsDestroyed());
        }

        public void DisplayBoard()
        {
            Console.Write("   ");
            for (int col = 0; col < cols; col++)
            {
                Console.Write((col + 1) + " ");
            }
            Console.WriteLine();

            for (int row = 0; row < rows; row++)
            {
                if (row >= 9)
                {
                    Console.Write((row + 1) + " ");
                }
                else
                {
                    Console.Write((row + 1) + "  ");
                }
                for (int col = 0; col < cols; col++)
                {
                    Console.Write(_board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
