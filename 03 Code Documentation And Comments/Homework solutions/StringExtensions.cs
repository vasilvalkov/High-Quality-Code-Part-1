﻿// <copyright file="StringExtensions.cs" company="Progress">
//     progress.com. All rights reserved.
// </copyright>
// <author></author>
/// <copyright file="StringExtensions.cs" company="Progress">
///     progress.com. All rights reserved.
/// </copyright>
/// <author></author>
namespace Telerik.ILS.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extensions to the String class for the Telerik ILS application.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the input string into its MD5 hash value.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>The MD5 hash value of the input string.</returns>
        public static string ToMd5Hash(this string input)
        {
            var md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new StringBuilder to collect the bytes
            // and create a string.
            var builder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return builder.ToString();
        }

        /// <summary>
        /// Attempts to convert a true-like or false-like string value to boolean value.
        /// </summary>
        /// <param name="input">A single-word string to convert.</param>
        /// <returns>true when the string value is true-like; otherwise false</returns>
        public static bool ToBoolean(this string input)
        {
            var stringTrueValues = new[] { "true", "ok", "yes", "1", "да" };
            return stringTrueValues.Contains(input.ToLower());
        }

        /// <summary>
        /// Converts the value of this instance into Signed 16-bit integer (System.Int16). Value should be a string representation of an integer number in the range -32,768 to 32,767.
        /// </summary>
        /// <returns>16-bit signed integer on success; otherwise returns 16-bit signed 0.</returns>
        public static short ToShort(this string input)
        {
            short shortValue;
            short.TryParse(input, out shortValue);
            return shortValue;
        }

        /// <summary>
        /// Converts the value of this instance into Signed 32-bit integer (System.Int32). Value should be a string representation of an integer number in the range -2,147,483,648 to 2,147,483,647.
        /// </summary>
        /// <returns>32-bit signed integer on success; otherwise returns 32-bit signed 0.</returns>
        public static int ToInteger(this string input)
        {
            int integerValue;
            int.TryParse(input, out integerValue);
            return integerValue;
        }

        /// <summary>
        /// Converts the value of this instance into Signed 64-bit integer (System.Int64). Value should be a string representation of an integer number in the range –9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
        /// </summary>
        /// <returns>64-bit signed integer on success; otherwise returns 64-bit signed 0.</returns>
        public static long ToLong(this string input)
        {
            long longValue;
            long.TryParse(input, out longValue);
            return longValue;
        }

        /// <summary>
        /// Converts the value of this instance its System.DateTime equivalent. Value should be a string representation of a date or date and time of day.
        /// </summary>
        /// <returns>An object that is equivalent to the date and time contained in this instance's value; If conversioin is not successful, returns null</returns>
        public static DateTime ToDateTime(this string input)
        {
            DateTime dateTimeValue;
            DateTime.TryParse(input, out dateTimeValue);
            return dateTimeValue;
        }

        /// <summary>
        /// Returns a new string with the first letter capitalized.
        /// </summary>
        /// <returns>New string with the first letter capitalized.</returns>
        public static string CapitalizeFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + input.Substring(1, input.Length - 1);
        }

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a specified substring and ends at another specified substring without including it.
        /// </summary>
        /// <param name="startString">The starting substring in this instance.</param>
        /// <param name="endString">The ending substring in this instance.</param>
        /// <param name="startFrom">Optional zero-based starting character position of a substring in this instance.</param>
        /// <returns>A string that is equivalent to the substring of length that begins at startString in this instance to the beginning of the endString. Returns System.String.Empty if neither startString, nor endString is found in this instance.</returns>
        public static string GetStringBetween(this string input, string startString, string endString, int startFrom = 0)
        {
            input = input.Substring(startFrom);
            startFrom = 0;
            if (!input.Contains(startString) || !input.Contains(endString))
            {
                return string.Empty;
            }

            var startPosition = input.IndexOf(startString, startFrom, StringComparison.Ordinal) + startString.Length;
            if (startPosition == -1)
            {
                return string.Empty;
            }

            var endPosition = input.IndexOf(endString, startPosition, StringComparison.Ordinal);
            if (endPosition == -1)
            {
                return string.Empty;
            }

            return input.Substring(startPosition, endPosition - startPosition);
        }

        /// <summary>
        /// Replaces all cyrillic letters in this instance with their latin representation.
        /// </summary>
        /// <returns>Returns a new string with replaced cyrillic characters.</returns>
        public static string ConvertCyrillicToLatinLetters(this string input)
        {
            var bulgarianLetters = new[]
                                       {
                                           "а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п",
                                           "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ь", "ю", "я"
                                       };
            var latinRepresentationsOfBulgarianLetters = new[]
                                                             {
                                                                 "a", "b", "v", "g", "d", "e", "j", "z", "i", "y", "k",
                                                                 "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h",
                                                                 "c", "ch", "sh", "sht", "u", "i", "yu", "ya"
                                                             };
            for (var i = 0; i < bulgarianLetters.Length; i++)
            {
                input = input.Replace(bulgarianLetters[i], latinRepresentationsOfBulgarianLetters[i]);
                input = input.Replace(bulgarianLetters[i].ToUpper(), latinRepresentationsOfBulgarianLetters[i].CapitalizeFirstLetter());
            }

            return input;
        }

        /// <summary>
        /// Replaces all latin letters in this instance with their cyrillic representation in the BG qwerty keyboard layout.
        /// </summary>
        /// <returns>Returns a new string with replaced latin characters.</returns>
        public static string ConvertLatinToCyrillicKeyboard(this string input)
        {
            var latinLetters = new[]
                                   {
                                       "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
                                       "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
                                   };

            var bulgarianRepresentationOfLatinKeyboard = new[]
                                                             {
                                                                 "а", "б", "ц", "д", "е", "ф", "г", "х", "и", "й", "к",
                                                                 "л", "м", "н", "о", "п", "я", "р", "с", "т", "у", "ж",
                                                                 "в", "ь", "ъ", "з"
                                                             };

            for (int i = 0; i < latinLetters.Length; i++)
            {
                input = input.Replace(latinLetters[i], bulgarianRepresentationOfLatinKeyboard[i]);
                input = input.Replace(latinLetters[i].ToUpper(), bulgarianRepresentationOfLatinKeyboard[i].ToUpper());
            }

            return input;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of invalid characters in the current instance are removed .
        /// </summary>
        /// <returns>New string that is a valid username.</returns>
        public static string ToValidUsername(this string input)
        {
            input = input.ConvertCyrillicToLatinLetters();
            return Regex.Replace(input, @"[^a-zA-z0-9_\.]+", string.Empty);
        }

        /// <summary>
        /// Returns a new string in which all occurrences of invalid characters in the current instance are removed 
        /// </summary>
        /// <returns>New string that is a valid file name.</returns>
        public static string ToValidLatinFileName(this string input)
        {
            input = input.Replace(" ", "-").ConvertCyrillicToLatinLetters();
            return Regex.Replace(input, @"[^a-zA-z0-9_\.\-]+", string.Empty);
        }

        /// <summary>
        /// Retrieves a substring from this instance that contains the first charsCount characters.
        /// </summary>
        /// <param name="charsCount">The count of characters of the substring.</param>
        /// <returns>New string containing the first charsCount characters.</returns>
        public static string GetFirstCharacters(this string input, int charsCount)
        {
            return input.Substring(0, Math.Min(input.Length, charsCount));
        }

        /// <summary>
        /// Retrieves the name of a file excluding its extension.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>New string containing the file name without the extension. Returns System.String.Empty when file name does not contain extension or is null or whitespace</returns>
        public static string GetFileExtension(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return string.Empty;
            }

            string[] fileParts = fileName.Split(new[] { "." }, StringSplitOptions.None);

            if (fileParts.Count() == 1 || string.IsNullOrEmpty(fileParts.Last()))
            {
                return string.Empty;
            }

            return fileParts.Last().Trim().ToLower();
        }

        /// <summary>
        /// Retrieves content type based on file extension.
        /// </summary>
        /// <returns> For a known extension returns a new string stating its content type: otherwise returns "application/octet-stream".</returns>
        public static string ToContentType(this string fileExtension)
        {
            var fileExtensionToContentType = new Dictionary<string, string>
                                                 {
                                                     { "jpg", "image/jpeg" },
                                                     { "jpeg", "image/jpeg" },
                                                     { "png", "image/x-png" },
                                                     {
                                                         "docx",
                                                         "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                                                     },
                                                     { "doc", "application/msword" },
                                                     { "pdf", "application/pdf" },
                                                     { "txt", "text/plain" },
                                                     { "rtf", "application/rtf" }
                                                 };
            if (fileExtensionToContentType.ContainsKey(fileExtension.Trim()))
            {
                return fileExtensionToContentType[fileExtension.Trim()];
            }

            return "application/octet-stream";
        }

        /// <summary>
        /// Copies the number of byte of this instance to a byte array and returns it.
        /// </summary>
        /// <returns>New byte array of this instance.</returns>
        public static byte[] ToByteArray(this string input)
        {
            var bytesArray = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytesArray, 0, bytesArray.Length);
            return bytesArray;
        }
    }
}
