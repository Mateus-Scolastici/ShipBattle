using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBattle
{
    public class Game
    {
        private Board board;

        public Game() 
        { 
            board = new Board();
        }

        private void ComputerTurn()
        {
            Console.WriteLine("Computer's turn:");

            int computerRow, computerCol;

            // Se não há alvos marcados como atingidos, ataque aleatoriamente
            if (!board.HasTargets())
            {
                do
                {
                    computerRow = new Random().Next(0, 10);
                    computerCol = new Random().Next(0, 10);
                } while (!board.IsValidAttack(computerRow, computerCol));
            }
            else
            {
                // Se há alvos marcados como atingidos, ataque nas direções vertical e horizontal
                (computerRow, computerCol) = board.GetNextTarget();
            }

            bool computerHit = board.Attack(computerRow, computerCol);

            // Display the result of the computer's attack
            if (computerHit)
                Console.WriteLine($"Computer hit at ({computerRow}, {computerCol})!");
            else
                Console.WriteLine($"Computer missed at ({computerRow}, {computerCol}).");
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Ship Battle");

            // Place ships
            board.PlaceShipsRandomly();

            // Game loop
            while (!board.IsGameOver())
            {
                Console.WriteLine("Your board:");
                board.DisplayBoard();

                // Player's turn
                Console.WriteLine("Your turn - Enter attack coordinates");
                Console.Write("Row: ");
                int playerRow = int.Parse(Console.ReadLine());
                Console.Write("\nCol: ");
                int playerCol = int.Parse(Console.ReadLine());
                Console.WriteLine();

                // Verify if the attack is valid and make the attack
                if (board.IsValidAttack(playerRow, playerCol))
                {
                    bool hit = board.Attack((playerRow - 1), (playerCol - 1));

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
                if (board.IsGameOver())
                    break;

                // Computer's turn
                Console.WriteLine("Computer's turn:");

                int computerRow, computerCol;
                do
                {
                    // Generate random attack coordinates for the computer
                    computerRow = new Random().Next(0, 10);
                    computerCol = new Random().Next(0, 10);
                } while (!board.IsValidAttack(computerRow, computerCol));

                bool computerHit = board.Attack(computerRow, computerCol);

                // Display the result of the computer's attack
                if (computerHit)
                    Console.WriteLine($"Computer hit at ({computerRow + 1}, {computerCol + 1})!");
                else
                    Console.WriteLine($"Computer missed at ({computerRow + 1}, {computerCol + 1}).");

                // Check if the game is over after computer's turn
                if (board.IsGameOver())
                    break;
            }

            // Display the final board with all the ships
            Console.WriteLine("Final board:");
            board.DisplayBoard();

            // Check the result of the game
            if (board.AreAllComputerShipsDestroyed())
            {
                Console.WriteLine("Congratulations! You destroyed all the computer's ships. You won!");
            }
            else
            {
                Console.WriteLine("All your ships were destroyed. You lost!");
            }
        }
    }
}
