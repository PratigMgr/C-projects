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
using System.Collections.ObjectModel;
using Assignment4_ContactManagar;

// ---------------------------------------------------------
// File: MainWindow.xaml.cs
// Author: Pratig Thapa Magar
// Date: 2025-11-30
// Description: Create a WPF Application the Assignment 4 Contact Manage includes handles user actions, form validation, DataGrid updates, and communication with the SQL repository.
// ---------------------------------------------------------


namespace Assignment4_ContactManager
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Contact collection used by the DataGrid.
        /// ObservableCollection updates the UI automatically.
        /// </summary>
        private ObservableCollection<Contact> contacts;

        /// <summary>
        /// SQL Server repository used for saving and loading.
        /// </summary>
        private IContactRepository repository;

        /// <summary>
        /// Currently selected contact (for updating/deleting).
        /// </summary>
        private Contact? selectedContact;

        /// <summary>
        /// Initializes the window, repository, and loads data.
        /// </summary>
        public MainWindow()
        {
            // Initializes WPF visual components
            InitializeComponent();

            // Create repository and collection
            repository = new SqlContactRepository();
            contacts = new ObservableCollection<Contact>();

            // Bind collection to DataGrid for UI updates
            dataGridContacts.ItemsSource = contacts;

            // Load all contacts from database
            LoadFromDatabase();

            // Reset form to initial state
            ResetForm();
            contacts.Clear();
        }

        // -----------------------------------------------------
        // MENU ACTIONS
        // -----------------------------------------------------

        // Handles loading contacts from the database
        private void MenuFileLoad_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Reload contacts from the database? Unsaved changes will be lost.",
                "Reload From Database",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                LoadFromDatabase();
                ResetForm();
            }
        }

        // Handles saving all UI contacts to the database
        private void MenuFileSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                repository.SaveAll(contacts);

                MessageBox.Show("Contacts saved successfully.",
                                "Save Complete",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving contacts:\n" + ex.Message,
                                "Save Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        // Closes the application
        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Shows program information
        private void MenuHelpAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Assignment 4 - Contact Manager\n\n" +
                "Demonstrates validation, data binding, SQL Server\n" +
                "repositories, and WPF UI design.\n\n" +
                "Course: COSC 2100 - OOP2",
                "About",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        // -----------------------------------------------------
        // BUTTON ACTIONS
        // -----------------------------------------------------

        // Adds a new validated contact to the UI list
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Contact newContact = CreateContactFromForm();
                newContact.Id = 0;

                contacts.Add(newContact);
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Validation Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        // Updates the selected contact with new validated values
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedContact == null)
            {
                MessageBox.Show("Please select a contact to update.",
                                "No Selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }

            try
            {
                Contact updated = CreateContactFromForm();
                updated.Id = selectedContact.Id;

                selectedContact.FirstName = updated.FirstName;
                selectedContact.LastName = updated.LastName;
                selectedContact.Email = updated.Email;
                selectedContact.Phone = updated.Phone;
                selectedContact.Category = updated.Category;

                // Refresh DataGrid UI to display updated values
                dataGridContacts.Items.Refresh();

                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Validation Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        // Deletes the selected contact from the UI list
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedContact == null)
            {
                MessageBox.Show("Please select a contact to delete.",
                                "No Selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Delete selected contact?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                contacts.Remove(selectedContact);
                ResetForm();
            }
        }

        // Resets the form fields to blank state
        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
            //Clear all the contact from the list but does not affect the database.
            MessageBoxResult result = MessageBox.Show(
                "Are you sure want to clear all contacts from list table? Press No if you don't want to. This does not affect the database.",
                "New Contact List",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                contacts.Clear();
            }
        }

        // Closes the application window
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // -----------------------------------------------------
        // DATAGRID SELECTION HANDLER
        // -----------------------------------------------------

        // Loads selected contact details into the form fields
        private void DataGridContacts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedContact = dataGridContacts.SelectedItem as Contact;

            if (selectedContact != null)
            {
                textBoxFirstName.Text = selectedContact.FirstName;
                textBoxLastName.Text = selectedContact.LastName;
                textBoxEmail.Text = selectedContact.Email;
                textBoxPhone.Text = selectedContact.Phone;
                comboBoxCategory.Text = selectedContact.Category;
            }
        }

        // -----------------------------------------------------
        // HELPER METHODS
        // -----------------------------------------------------

        // Loads all contacts from SQL Server into the collection
        private void LoadFromDatabase()
        {
            try
            {
                contacts.Clear();

                foreach (Contact c in repository.GetAll())
                {
                    contacts.Add(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading contacts:\n" + ex.Message,
                                "Load Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        // Clears all form fields and deselects the DataGrid
        private void ResetForm()
        {

            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "";
            comboBoxCategory.Text = "";

            selectedContact = null;
            dataGridContacts.UnselectAll();
            textBoxFirstName.Focus();

        }

        // Validates form fields and constructs a Contact object
        private Contact CreateContactFromForm()
        {
            string first = Validators.RequireNotBlank(textBoxFirstName.Text, "First Name");
            string last = Validators.RequireNotBlank(textBoxLastName.Text, "Last Name");
            string email = Validators.ValidateEmail(textBoxEmail.Text);
            string phone = Validators.ValidatePhone(textBoxPhone.Text);
            string category = Validators.RequireCategory(comboBoxCategory.Text);

            return new Contact
            {
                FirstName = first,
                LastName = last,
                Email = email,
                Phone = phone,
                Category = category
            };
        }

        private void MenuFileNew_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}