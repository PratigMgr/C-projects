using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Name: Pratig Thapa Magar
/// Course code: COSC 2100
/// Description: CustomDeck class inherits from Deck and allows adding custom cards
/// Date: 2025-11-14

namespace Assignment_3_Deck_Builder
{
    // CustomDeck inherits all features from the Deck class
    public class CustomDeck : Deck
    {
        // Method to add a custom card to the deck
        public void AddCustomCard(string suit, string rank)
        {
            // Create a new Card object and add it to the 'cards' list inherited from Deck
            cards.Add(new Card(suit, rank));
        }
    }
}