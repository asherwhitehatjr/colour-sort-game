using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace colour_sort_game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tubes MyTubes = new Tubes();
            MyTubes.NumberOfTubes = 7;
            MyTubes.HeightOfTube = 6;
            MyTubes.AmountOfColours = 5;
            MyTubes.NumberOfEmptyTubes = 2;
            MyTubes.CreateTubeArray();

            MyTubes.TubeCheck();
            Dictionary<int,int> MyDictionary = MyTubes.CreateColourDictionary();
            MyTubes.PopulatingTubes(MyDictionary,MyTubes.TubeArray);

            //Console.WriteLine(MyTubes.TubeArray);

            foreach (var item in MyTubes.TubeArray)
            {
                //Console.WriteLine(item.ToString());
                Console.Write("[");
                foreach (var stackitems in item)
                {
                    Console.Write(stackitems);
                }
                Console.WriteLine("]");
            }

            Console.ReadLine();

        }
        static int CheckMove(Stack<int> originStack, Stack<int> finalStack, int height)
        {
            // Colours need to be the same between origin and final. DONE
            // FinalStack must have space (can't have a height of over the height variable). DONE
            // If there are multiple of the same colour in a row in origin, and space for them in final, then those multiple colours move together


            int TopItemOrigin = originStack.Peek();
            int TopItemFinal = finalStack.Peek();

            if (finalStack.Count >= height)
            {
                return -1;
            }

            if (TopItemOrigin != TopItemFinal)
            {
                return -1;
            }
            return TopItemFinal;

        }

        static void MakeMove(Stack<int> originStack, Stack<int> finalStack, int height, int ItemToMove)
        {
            while (originStack.Peek() == ItemToMove && finalStack.Count < height)
            {
                int item = originStack.Pop();
                finalStack.Push(item);
            }

        }
    }

    class Tubes
    {
        private int numberOfTubes;
        public int NumberOfTubes
        {
            get { return numberOfTubes; }
            set { numberOfTubes = value; }
        }

        private int heightOfTube;
        public int HeightOfTube
        {
            get { return heightOfTube; }
            set { heightOfTube = value; }
        }

        private int amountOfColours;
        public int AmountOfColours
        {
            get { return amountOfColours; }
            set { amountOfColours = value; }
        }

        private int numberOfEmptyTubes;
        public int NumberOfEmptyTubes
        {
            get { return numberOfEmptyTubes; }
            set { numberOfEmptyTubes = value; }
        }

        private Stack<int>[] tubeArray;

        public Stack<int>[] TubeArray
        {
            get { return tubeArray; }
            set { tubeArray = value; }
        }
        
        public int ColourTubes()
        {
            //calculates the number of tubes that will have colours in them.
            return NumberOfTubes - NumberOfEmptyTubes;
        }

        /*public void CreateTubeArray()
        {
            Tubes.TubeArray = new Stack<int>[Tubes.NumberOfTubes];

            for (int i = 0; i < Tubes.NumberOfTubes; i++)
            {
                Tubes.TubesArray[i] = new Stack<int>();
            }
        }*/

        public Stack<int>[] CreateTubeArray()
        {
            TubeArray = new Stack<int>[NumberOfTubes];

            for (int i = 0; i < NumberOfTubes; i++)
            {
                TubeArray[i] = new Stack<int>();
            }

            return TubeArray;
        }

        /* Create a method that:
           Check that a given Amount of Tubes (that can hold colours), and amount of colours is compatible together
            That there is at least 1 extra empty tube

            Amount of tubes with colours in
            Height of the tubes
         */
        public Boolean TubeCheck()
        {
            if (AmountOfColours < 2 || ColourTubes() < AmountOfColours)
            {
                return false;
            }
            if (numberOfEmptyTubes < 1)
            {
                return false;
            }

            return true;
        }

        public Dictionary<int, int> CreateColourDictionary()
        {
            Dictionary<int, int> colourDictionary = new Dictionary<int, int>();

            // the number of blocks of colours
            int AmountOfColourSpots = heightOfTube * ColourTubes();// 70

            // the minimumm amount of colour blocks per colour required to fill the tubes
            int MinColourAmount = ((AmountOfColourSpots / AmountOfColours) / HeightOfTube) * HeightOfTube; // 15

            // the minimum total number of colour blocks
            int TotalMinColours = MinColourAmount * AmountOfColours; // 60

            // the amount of blocks that dont have a colour
            int RemainingColourSpots = AmountOfColourSpots - TotalMinColours; //10

            // a count used for the dictionary to find out how many colours can get more colour blocks that is a multiple of the height of tube
            int ExtraColoursCount = RemainingColourSpots / HeightOfTube; // 2

            // going through the dictionary and allocating how many colour blocks each colour gets 
            for (int i = 0; i < AmountOfColours; i++)
            {
                if (ExtraColoursCount > 0)
                {
                    colourDictionary.Add(i, MinColourAmount + HeightOfTube); //0:5
                    ExtraColoursCount -= 1;
                }
                else
                {
                    colourDictionary.Add(i, MinColourAmount);
                }
            }

            // producing the dictionary
            /*foreach (var item in colourDictionary)
            {
                Console.WriteLine(item);
            }*/

            return colourDictionary;
        }

        public Stack<int>[] PopulatingTubes(Dictionary<int,int> ColourDictionary, Stack<int>[] TubeArray)
        {
            var rand = new Random();

            for (int i = 0;i < TubeArray.Length - NumberOfEmptyTubes; i++)
            {
                for(int j = 0;j < HeightOfTube; j++)
                {
                    Console.WriteLine(ColourDictionary.Keys);
                    int RandomSelection = rand.Next(ColourDictionary.Count);
                    int keyOfSelection = ColourDictionary.ElementAt(RandomSelection).Key;
                    TubeArray[i].Push(keyOfSelection);
                    //Console.WriteLine(ColourDictionary[RandomSelection]);
                    //ColourDictionary[RandomSelection] -= 1;

                    Console.WriteLine("New iteration");

                    foreach (KeyValuePair<int, int> kvp in ColourDictionary)
                    {
                        Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                    }

                    ColourDictionary[keyOfSelection] -= 1;

                    if (ColourDictionary[keyOfSelection] == 0)
                    {
                        ColourDictionary.Remove(keyOfSelection);
                    }
                }
            }
            return TubeArray;
        }
    }
}
