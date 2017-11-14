using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Pile
    {
        public Card[] cards;
        public int size = 0;

        public Pile()
        {
            cards = new Card[52];
        }

        public void AddCardToEnd(Card card)
        {
            cards[size] = card;
            size++;
        }

        public void AddCardToStart(Card card)
        {
            for (int i = size; i > 0; i--)
            {
                cards[i] = cards[i-1];
            }
            cards[0] = card;
            size++;
        }

        public Card RemoveCardFromEnd()
        {
            Card retval = GetCardFromEnd();
            if (retval != null)
            {
                size--;
            }
            return retval;
        }

        public Card GetCardFromEnd()
        {
            Card retval = null;
            if (size > 0)
            {
                retval = cards[size - 1];
            }
            return retval;
        }

        public Card RemoveCardFromStart()
        {
            Card retval = null;
            if (size > 0)
            {
                retval = cards[0];
                size--;
                for (int i = 0; i < size; i++)
                {
                    cards[i] = cards[i + 1];
                }
                
            }
            return retval;
        }
        public Card GetCard(int index)
        {
            Card retval = null;
            if (index >= 0 && index <= size)
            {
                retval = cards[index];
            }
            return retval;
        }

        public Pile GetCardsFromStart(int size)
        {
            Pile pile = new Pile();
            for (int i = 0; i < size; i++)
            {
                pile.AddCardToEnd(this.RemoveCardFromStart());
            }
            return pile;
        }

        public Pile GetCardsFromEnd(int size)
        {
            Pile pile = new Pile();
            for (int i = 0; i < size; i++)
            {
                pile.AddCardToEnd(this.RemoveCardFromEnd());
            }
            return pile;
        }

        public Card GetCardFromStart()
        {
            Card retval = null;
            if (size > 0)
            {
                retval = cards[0];
            }
            return retval;
        }

        public void Order()
        {
            Array.Sort(cards);
        }

        public void Shuffle()
        {
            int n = size;
            Random rng = new Random();
            while (n > 1)
            {
                int k = rng.Next(n--);
                Card temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }
        }

        public void AddPile(Pile pile)
        {
            for (int i = 0; i < pile.size; i++)
            {
                Card card = pile.GetCard(i);
                this.AddCardToEnd(card);
            }
        }
    }
}
