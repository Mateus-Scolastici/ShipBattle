using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShipBattle
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
            Console.ReadKey();
        }
    }
}
