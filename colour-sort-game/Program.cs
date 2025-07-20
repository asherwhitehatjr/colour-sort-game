using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colour_sort_game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int height = 5;

            Stack<int> origin = new Stack<int>();
            origin.Push(0);
            origin.Push(0);
            origin.Push(1);
            origin.Push(2);
            origin.Push(2);
            Stack<int> final = new Stack<int>();
            final.Push(0);
            final.Push(1);
            final.Push(2);
            final.Push(2);

            int ItemToMove = CheckMove(origin, final, height);
            if (ItemToMove != -1)
            {
                MakeMove(origin, final, height, ItemToMove);
            }

            foreach (var item in origin)
            {
                Console.WriteLine(item.ToString());
            }
            System.Console.WriteLine("Final");
            foreach (var item in final)
            {
                Console.WriteLine(item.ToString());
            }

        }
        static int CheckMove(Stack<int> originStack, Stack<int> finalStack, int height)
        {
            // Colours need to be the same between origin and final DONE
            // FinalStack must have space (can't have a height of over the height variable) DONE
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
}
