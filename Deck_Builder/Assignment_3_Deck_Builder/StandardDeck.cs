using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_3_Deck_Builder;

/// Name: Pratig Thapa Magar
/// Course code: COSC 2100
/// Description: StandardDeck class to create a full 52-card deck
/// Date: 2025-11-14

namespace Assignment_3_Deck_Builder
{
    // StandardDeck inherits from Deck and automatically fills with 52 cards
    public class StandardDeck : Deck
    {
        // Array of all four suits
        private static readonly string[] Suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

        // Array of all ranks in a standard deck
        private static readonly string[] Ranks =
        {
            "Ace","2","3","4","5","6","7","8",
            "9","10","Jack","Queen","King"
        };

        // Constructor that builds the full deck
        public StandardDeck()
        {
            // Loop through each suit
            foreach (string suit in Suits)
            {
                // Loop through each rank
                foreach (string rank in Ranks)
                {
                    // Add a new card with current suit and rank to the 'cards' list
                    cards.Add(new Card(suit, rank));
                }
            }
        }
    }
}
