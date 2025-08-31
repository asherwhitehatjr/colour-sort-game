using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colour_sort_game
{
    internal class Level
    {
        private int levelCount;
        public int LevelCount
        {
            get { return levelCount; }
            set { levelCount = value; }
        }

        public void CreateLevel()
        {
            var Rand = new Random();

            Tubes MyTubes = new Tubes();
            MyTubes.NumberOfTubes = 4 + (LevelCount / 5);
            MyTubes.HeightOfTube = Math.Min(10,4+(levelCount/10));
            MyTubes.AmountOfColours = Rand.Next(MyTubes.NumberOfTubes/2 , MyTubes.NumberOfTubes);
            MyTubes.NumberOfEmptyTubes = 1 + ((MyTubes.NumberOfTubes)/4);
            MyTubes.CreateTubeArray();

            MyTubes.TubeCheck();
            Dictionary<int, int> MyDictionary = MyTubes.CreateColourDictionary();
            MyTubes.PopulatingTubes(MyDictionary, MyTubes.TubeArray);

            //Console.WriteLine(MyTubes.TubeArray);

            while (MyTubes.CheckGameCompleted() == false)
            {
                MyTubes.PrintTubes(MyTubes.TubeArray);

                Console.WriteLine("what tube do want to move from : ");
                int originTube = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("what tube do want to move to : ");
                int finalTube = Convert.ToInt16(Console.ReadLine());

                string moveItemStr = MyTubes.CheckMove(originTube, finalTube);
                try
                {
                    int moveItem = Convert.ToInt32(moveItemStr);
                    MyTubes.MakeMove(MyTubes.TubeArray[originTube - 1], MyTubes.TubeArray[finalTube - 1], MyTubes.HeightOfTube, moveItem);
                }
                catch (Exception e)
                {
                    Console.WriteLine(moveItemStr);
                }
            }

            Console.ReadLine();
        }
    }
}
