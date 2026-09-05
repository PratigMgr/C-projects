using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Name: Pratig Thapa Magar
/// Course code: COSC 2100
/// Description: Base Deck class to manage a collection of cards
/// Date: 2025-11-14

namespace Assignment_3_Deck_Builder
{
    // Base class representing a deck of cards
    public class Deck
    {
        // Protected list to store cards, accessible by derived classes
        protected List<Card> cards = new List<Card>();

        // Public property to access the list of cards
        public List<Card> Cards => cards;

        // Method to add a single card to the deck
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        // Method to shuffle the deck using Fisher-Yates algorithm
        public void Shuffle()
        {
            Random rng = new Random();

            // Loop backwards through the deck
            for (int i = cards.Count - 1; i > 0; i--)
            {
                // Pick a random index from 0 to i
                int j = rng.Next(i + 1);

                // Swap the current card with the randomly chosen one
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }
        }

        // Method to deal a number of cards from the top of the deck
        public List<Card> Deal(int amount)
        {
            List<Card> dealt = new List<Card>();

            // Loop to remove and return cards up to the requested amount
            for (int i = 0; i < amount && cards.Count > 0; i++)
            {
                dealt.Add(cards[0]);   // Add top card to dealt list
                cards.RemoveAt(0);     // Remove it from the deck
            }

            return dealt;
        }

        // Method to clear all cards from the deck
        public void Clear()
        {
            cards.Clear();
        }
    }
}
