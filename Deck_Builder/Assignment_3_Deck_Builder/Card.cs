using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Name: Pratig Thapa Magar
/// Course code: COSC 2100
/// Description: Represents a single playing card
/// Date: 2025-11-14

namespace Assignment_3_Deck_Builder
{
    // Class representing a single card with a suit and a rank
    public class Card
    {
        // Property for the card's suit (e.g., Hearts, Spades)
        public string Suit { get; set; }

        // Property for the card's rank (e.g., Ace, 2, King)
        public string Rank { get; set; }

        // Constructor to initialize the card with a suit and rank
        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }

        // Override ToString to return a readable representation of the card
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}
