using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Assignment_3_Deck_Builder
{
    /// <summary>
    /// Name:Pratig Thapa Magar
    /// Course code: COSC 2100
    /// Description: Controls all deck operations and button actions in the Deck Builder program like shuffling, adding, viewing and dealing.
    /// Date: 2025-11-14
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Stores the current deck (standard deck + any custom cards added)
        private Deck deck;

        public MainWindow()
        {
            InitializeComponent();

            // Load a new standard deck and clear all fields
            ResetForm();
        }

        // ========RESET FUNCTION==============
        private void ResetForm()
        {
            // Create a brand-new 52-card standard deck
            deck = new StandardDeck();

            // Clear all user input fields
            suitTextbox.Clear();
            rankTextbox.Clear();
            drawTextbox.Clear();

            // Clear the displayed lists on the screen
            cardDealtListBox.ItemsSource = null;
            deckListBox.ItemsSource = null;
        }

        // =========VALIDATION HELPERS===========
        private bool ValidateCustom()
        {
            // Suit text must not be empty
            if (string.IsNullOrWhiteSpace(suitTextbox.Text))
            {
                MessageBox.Show("Suit cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Rank text must not be empty
            if (string.IsNullOrWhiteSpace(rankTextbox.Text))
            {
                MessageBox.Show("Rank cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool ValidateDraw(out int amount)
        {
            // Ensure draw amount is a valid number
            if (!int.TryParse(drawTextbox.Text, out amount))
            {
                MessageBox.Show("Draw must be a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Ensure number is within valid range of deck size
            if (amount <= 0 || amount > deck.Cards.Count)
            {
                MessageBox.Show("Draw amount must be greater than 0 and less than remaining cards in the deck.",
                                "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        //================== BUTTON EVENTS============================

        // Displays all cards currently in the deck
        private void viewDeckButton_Click_1(object sender, RoutedEventArgs e)
        {
            // Refresh the view
            deckListBox.ItemsSource = null;   
            deckListBox.ItemsSource = deck.Cards;
        }

        // Randomizes the order of cards in the deck
        private void shuffleButton_Click_1(object sender, RoutedEventArgs e)
        {// Shuffle the list of cards
            deck.Shuffle();                   
            deckListBox.ItemsSource = null;
            deckListBox.ItemsSource = deck.Cards;
        }

        // Resets the deck and the entire form
        private void resetButton_Click_1(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }

        // Closes the application window
        private void exitButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Deals the number of cards requested by the user
        private void dealButton_Click_1(object sender, RoutedEventArgs e)
        {
            // Validate the draw amount before dealing
            if (!ValidateDraw(out int amount)) return;

            // Remove the specified number of cards from the deck
            List<Card> dealt = deck.Deal(amount);

            // Display the dealt cards
            cardDealtListBox.ItemsSource = null;
            cardDealtListBox.ItemsSource = dealt;
        }

        // Adds a new custom card to the deck
        private void addCustomButton_Click_1(object sender, RoutedEventArgs e)
        {
            // Validate the suit and rank fields
            if (!ValidateCustom()) return;

            // Add the new custom card into the deck
            deck.AddCard(new Card(suitTextbox.Text.Trim(), rankTextbox.Text.Trim()));

            // Clear fields after adding the card
            suitTextbox.Clear();
            rankTextbox.Clear();

            // Update the deck display on the screen
            deckListBox.ItemsSource = null;
            deckListBox.ItemsSource = deck.Cards;
        }
    }
}
