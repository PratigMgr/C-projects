using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using global::Assignment4_ContactManagar;
// ---------------------------------------------------------
// File: IContactRepository.cs
// Author: Pratig Thapa Magar (provided solution)
// Date: 2025-11-30
// Description:
//   Defines the contract for any data access class that can
//   load and save Contact objects. This separates UI from storage.
// ---------------------------------------------------------


namespace Assignment4_ContactManagar

{
    /// <summary>
    /// Provides methods for working with persisted Contact data.
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Returns all contacts currently stored in the underlying data source.
        /// </summary>
        List<Contact> GetAll();

        /// <summary>
        /// Completely replaces the stored data with the provided contacts.
        /// This is used by the demo's "Save All" operation.
        /// </summary>
        void SaveAll(IEnumerable<Contact> contacts);
    }
}
