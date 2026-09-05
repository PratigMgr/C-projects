using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
// ---------------------------------------------------------
// File: Validators.cs
// Author: Pratig Thapa Magar (provided solution)
// Date: 2025-11-30
// Description:
//   Contains helper methods used to validate user input for
//   the Mini Contact Manager. These methods throw exceptions
//   with friendly messages when data is invalid.
// ---------------------------------------------------------


namespace Assignment4_ContactManagar
{
    /// <summary>
    /// Utility class containing static validation methods.
    /// Keeping all validation here helps to avoid repeating
    /// the same checks inside event handlers.
    /// </summary>
    public static class Validators
    {

        // Phone pattern: More than 10 or less than 10 digits, spaces, parentheses, plus, dashes not allowed.
        private static readonly Regex PhonePattern = new Regex(@"^\d{10}$", RegexOptions.Compiled);
        // Simple but reasonable email pattern for demo purposes.
        private static readonly Regex EmailPattern = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        static Validators()
        {
        }

        /// <summary>
        /// Ensures that the given text is not null, empty, or whitespace.
        /// If the value is valid, returns a trimmed version of the text.
        /// Throws ArgumentException on invalid input.
        /// </summary>
        public static string RequireNotBlank(string text, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException(fieldName + " is required.");
            };

            string trimmed = text.Trim();

            // Prevent any numeric characters.
            if (Regex.IsMatch(trimmed, @"\d"))
                throw new ArgumentException(fieldName + " must not contain numbers.");

            return trimmed;
        }

        /// <summary>
        /// Validates the category text. For this demo it must not be blank.
        /// </summary>
        public static string RequireCategory(string text)
        {
            return RequireNotBlank(text, "Category");
        }
        /// <summary>
        /// Validates phone. Blank is allowed; if present, must match pattern.
        /// </summary>
        public static string ValidatePhone(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Phone Number is required.");
            }

            string trimmed = text.Trim();

            if (!PhonePattern.IsMatch(trimmed))
            {
                throw new ArgumentException("Phone contains invalid characters. Only 10 digits numbers are allowed.");
            }

            return trimmed;
        }

        /// <summary>
        /// Validates email. Throws ArgumentException if invalid.
        /// </summary>
        public static string ValidateEmail(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Email is required.");
            }

            string trimmed = text.Trim();

            if (!EmailPattern.IsMatch(trimmed))
            {
                throw new ArgumentException("Email is not in a valid format.");
            }

            return trimmed;
        }

    }
}
