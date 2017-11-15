using System;
using System.Collections.Generic;
using System.Text;


namespace Solitaire
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck.Shuffle();

            // Solitaire Piles
            int numSuits = 4;

            FoundationPile[] foundationPiles = new FoundationPile[numSuits];
            for (int i = 0; i < numSuits; i++)
            {
                foundationPiles[i] = new FoundationPile();
                //Debug.WriteLine("Foundation Pile " + (i + 1));
                PrintPile(foundationPiles[i]);
            }
            
            Pile[] tableauPiles = new Pile[7];

            for (int i = 0; i < 7; i++) {
                tableauPiles[i] = new Pile();
                tableauPiles[i].AddPile(deck.GetCardsFromStart(i + 1));
                tableauPiles[i].GetCardFromEnd().facingUp = true;
              //  Debug.WriteLine("Tableau Pile " + (i + 1));
                PrintPile(tableauPiles[i]);
            }

        }

        public static void PrintPile(Pile pile)
        {
            for (int i = 0; i < pile.size; i++)
            {
                ICard card = pile.GetCard(i);
                //Debug.WriteLine(card);
            }
        }
    }
}
