using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colour_sort_game
{
    internal class MainGame
    {
        public static void Main(string[] args)
        {
            Level NewLevel = new Level();
            NewLevel.LevelCount = 50;

            NewLevel.CreateLevel();
        }
    }
}
