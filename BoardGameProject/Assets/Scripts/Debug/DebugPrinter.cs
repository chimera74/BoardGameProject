using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Debug
{
    public class DebugPrinter
    {
        public static void PrintDeckInfo(Deck deck)
        {
            string text = "Deck; C: " + deck.CardCount + "; Face: " + (deck.IsFaceUp ? "Up" : "Down") + ";\n";

            text += deck.IsFaceUp ? "Top " : "Bottom ";
            bool first = true;
            foreach (Card card in deck.GetCardList())
            {
                if (first)
                    first = false;
                else
                    text += ", ";
                text += card.ToString();
            }
            text += deck.IsFaceUp ? " Bottom" : " Top";
            UnityEngine.Debug.Log(text);
        }

        public static void PrintCardInfo(Card card)
        {
            string text = "Card; Face: " + (card.IsFaceUp ? "Up" : "Down") + "; " + card.ToString();
            UnityEngine.Debug.Log(text);
        }
    }
}
