using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBattle
{
    public class Game
    {
        private Board _myboard;
        private Board _cpuboard;

        public Game() 
        { 
            _myboard = new Board();
            _cpuboard = new Board();
        }

        public void DisplayBoard()
        {
            DisplayBoard(_myboard, _cpuboard);
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Ship Battle");

            // Place ships
            _myboard.PlaceShipsRandomly();
            _cpuboard.PlaceShipsRandomly();

            // Game loop
            while (!_cpuboard.IsGameOver() && !_myboard.IsGameOver())
            {
                DisplayBoard();

                // Player's turn
                Console.WriteLine("Your turn - Enter attack coordinates");
                Console.Write("Row: ");
                int playerRow = int.Parse(Console.ReadLine());
                Console.Write("\nCol: ");
                int playerCol = int.Parse(Console.ReadLine());
                Console.WriteLine();

                // Verify if the attack is valid and make the attack
                if (_cpuboard.IsValidAttack(playerRow, playerCol))
                {
                    bool hit = _cpuboard.Attack((playerRow - 1), (playerCol - 1));

                    // Display the result of the attack to the player
                    if (hit)
                        Console.WriteLine("You hit a ship!");
                    else
                        Console.WriteLine("You missed.");
                }
                else
                {
                    Console.WriteLine("Invalid coordinates or you have already attacked this position. Try again.");
                }

                // Check if the game is over after player's turn
                if (_cpuboard.IsGameOver())
                    break;

                // Computer's turn
                Console.WriteLine("Computer's turn:");

                int computerRow, computerCol;
                do
                {
                    // Generate random attack coordinates for the computer
                    computerRow = new Random().Next(0, 10);
                    computerCol = new Random().Next(0, 10);
                } while (!_myboard.IsValidAttack(computerRow, computerCol));

                bool computerHit = _myboard.Attack(computerRow, computerCol);

                // Display the result of the computer's attack
                if (computerHit)
                    Console.WriteLine($"Computer hit at ({computerRow + 1}, {computerCol + 1})!");
                else
                    Console.WriteLine($"Computer missed at ({computerRow + 1}, {computerCol + 1}).");

                // Check if the game is over after computer's turn
                if (_myboard.IsGameOver())
                    break;
            }

            // Display the final board with all the ships
            Console.WriteLine("Final board:");
            DisplayBoard();

            // Check the result of the game
            if (_cpuboard.AreAllComputerShipsDestroyed())
            {
                Console.WriteLine("Congratulations! You destroyed all the computer's ships. You won!");
            }
            else
            {
                Console.WriteLine("All your ships were destroyed. You lost!");
            }
        }

        public void DisplayBoard(Board _myboard, Board _cpuboard)
        {
            Console.Write("   ");
            for (int col = 0; col < _myboard.GetBoard().GetLength(1); col++)
            {
                Console.Write((col + 1) + " ");
            }

            Console.Write("   ");
            for (int col = 0; col < _cpuboard.GetBoard().GetLength(1); col++)
            {
                Console.Write((col + 1) + " ");
            }

            Console.WriteLine();

            for (int row = 0; row < _cpuboard.GetBoard().GetLength(0); row++)
            {
                if (row >= 9)
                {
                    Console.Write((row + 1) + " ");
                }
                else
                {
                    Console.Write((row + 1) + "  ");
                }
                for (int col = 0; col < _myboard.GetBoard().GetLength(1); col++)
                {
                    Console.Write(_myboard.GetBoard()[row, col] + " ");
                }
                Console.Write("    ");

                for (int col = 0; col < _cpuboard.GetBoard().GetLength(1); col++)
                {
                    Console.Write(_cpuboard.GetBoard()[row, col] + " ");
                }

                Console.WriteLine();

            }
        }
    }
}
