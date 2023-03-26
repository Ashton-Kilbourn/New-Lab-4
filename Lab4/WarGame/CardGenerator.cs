using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarGame
{
    enum suits
    {
        Spades, Hearts, Diamonds, Clubs
    }
    enum faces
    {
        A = 1, _2, _3, _4, _5, _6, _7, _8, _9, _10, J, Q, K
    }
    internal static class CardGenerator
    {
        public static void Generate()
        {
            using (StreamWriter sw = new StreamWriter("cards.csv"))
            {
                bool isFirstSuit = true;
                foreach (var suit in Enum.GetValues<suits>())
                {
                    if (!isFirstSuit)
                        sw.Write(';');
                    isFirstSuit = false;

                    char symbol = ' ';
                    switch (suit)
                    {
                        case suits.Spades:
                            symbol = '♠';
                            break;
                        case suits.Hearts:
                            symbol = '♥';
                            break;
                        case suits.Diamonds:
                            symbol = '♦';
                            break;
                        case suits.Clubs:
                            symbol = '♣';
                            break;
                    }
                    bool first = true;
                    foreach (var face in Enum.GetValues<faces>())
                    {
                        if (!first)
                            sw.Write(';');
                        if (face == faces.A || (int)face > 10)
                            sw.Write(face);
                        else
                            sw.Write((int)face);
                        sw.Write(symbol);
                        first = false;
                    }
                }
            }
        }

    }
}
