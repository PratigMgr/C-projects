using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

// ---------------------------------------------------------
// File: SqlContactRepository.cs
// Author: Pratig Thapa Magar (provided solution)
// Date: 2025-11-30
// Description:
//   Concrete implementation of IContactRepository that uses
//   SQL Server and ADO.NET to load and save Contact data.
// ---------------------------------------------------------

namespace Assignment4_ContactManagar
{
    /// <summary>
    /// Provides SQL Server-based implementations of the repository methods.
    /// </summary>
    public class SqlContactRepository : IContactRepository
    {
        private readonly string connectionString;

        // ----------------------------------------------
        // Constructor loads the connection string from App.config.
        // Ensures the configuration exists before assigning it.
        // ----------------------------------------------
        public SqlContactRepository()
        {
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings["ContactsDb"];

            if (settings == null)
            {
                throw new InvalidOperationException(
                    "Connection string 'ContactsDb' is missing in App.config.");
            }

            connectionString = settings.ConnectionString;
        }

        // ----------------------------------------------
        // Loads all Contact records from the database.
        // Converts each row from SqlDataReader into a Contact object.
        // ----------------------------------------------
        public List<Contact> GetAll()
        {
            List<Contact> contacts = new List<Contact>();

            // Create and open the SQL connection.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL query to fetch all contacts sorted by name.
                using (SqlCommand command = new SqlCommand(
                    "SELECT Id, FirstName, LastName, Email, Phone, Category FROM Contacts ORDER BY LastName, FirstName",
                    connection))
                {
                    // Execute the query and read the results.
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Convert SQL row into a Contact object.
                            Contact c = new Contact();
                            c.Id = reader.GetInt32(0);
                            c.FirstName = reader.GetString(1);
                            c.LastName = reader.GetString(2);
                            c.Email = reader.GetString(3);

                            // Handle nullable Phone column.
                            if (!reader.IsDBNull(4))
                                c.Phone = reader.GetString(4);
                            else
                                c.Phone = string.Empty;

                            c.Category = reader.GetString(5);

                            // Add the Contact to the list.
                            contacts.Add(c);
                        }
                    }
                }
            }

            return contacts;
        }

        // ----------------------------------------------
        // Saves all contacts by clearing the table and re-inserting each item.
        // Uses a SQL transaction to ensure data integrity.
        // ----------------------------------------------
        public void SaveAll(IEnumerable<Contact> contacts)
        {
            // Create and open a SQL connection.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Begin transaction for delete + insert operations.
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Remove all existing rows from Contacts table.
                        using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM Contacts", connection, transaction))
                        {
                            deleteCommand.ExecuteNonQuery();
                        }

                        // Insert each contact one by one with parameters.
                        foreach (Contact contact in contacts)
                        {
                            using (SqlCommand insertCommand = new SqlCommand(
                                "INSERT INTO Contacts (FirstName, LastName, Email, Phone, Category) " +
                                "VALUES (@FirstName, @LastName, @Email, @Phone, @Category);",
                                connection,
                                transaction))
                            {
                                // Assign parameter values.
                                insertCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = contact.FirstName;
                                insertCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = contact.LastName;
                                insertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = contact.Email;

                                // Handle nullable phone number.
                                if (string.IsNullOrEmpty(contact.Phone))
                                {
                                    insertCommand.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                                }
                                else
                                {
                                    insertCommand.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = contact.Phone;
                                }

                                insertCommand.Parameters.Add("@Category", SqlDbType.NVarChar, 100).Value = contact.Category;

                                // Insert into database.
                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        // Commit all changes if everything succeeded.
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Roll back changes on error.
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
