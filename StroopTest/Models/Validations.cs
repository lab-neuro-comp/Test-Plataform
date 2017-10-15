﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TestPlatform.Models
{
    // Used to create validations for classes and variables type
    class Validations
    {
        private static String hexPattern = "^#(([0-9a-fA-F]{2}){3}|([0-9a-fA-F]){3})$";

        // Used to check if the string has no characters
        public static bool isEmpty(string word)
        {
            if (string.IsNullOrEmpty(word))
                return true;
            return false;
        }

        // Used to check if the character on the string a numeric one
        public static bool isAlphanumeric(string word)
        {
            if (!string.IsNullOrEmpty(word) && word.All(char.IsLetterOrDigit))
                return true;
            return false;
        }

        // Used to check if the character on the string is a digit
        public static bool isDigit(string word)
        {
            if (word.All(char.IsDigit))
                return true;
            return false;
        }

        // Used to check if the value of the int variable passed is valid
        public static bool isNumExpositionsValid(int value)
        {
            if (value > 0)
                return true;
            return false;
        }

        // Used to check if the value of the int variable passed is within the valid range
        public static bool isExpositionTimeValid(int value)
        {
            if (value > 0 && value < 10000)
                return true;
            return false;
        }

        public static bool isSubtitlePlaceValid(int value)
        {
            if (value > 0 && value <= 6)
                return true;
            return false;
        }

        public static bool isIntervalTimeValid(int value)
        {
            if (value > 0)
                return true;
            return false;
        }

        public static bool isExperimentIntervalTimeValid(int value)
        {
            if (value >= 0)
                return true;
            return false;
        }

        public static bool isListValid (string value)
        {
            if (value.Length > 4 && value.Substring(value.Length - 4) == ".lst" || value.ToLower() == "false")
                return true;
            return false;
        }
        public static bool isColorValid(string value)
        {
            if (Regex.IsMatch(value, hexPattern) || value.ToLower() == "false")
                return true;
            return false;
        }

        public static bool isHexPattern(string value)
        {
            if (Regex.IsMatch(value, hexPattern))
                return true;
            return false;
        }

        public static bool allHexPattern(string[] value)
        {
            foreach (string c in value) // tests if colors list contains only hexadecimal color codes
            {
                if (!Regex.IsMatch(c, hexPattern))
                    return false;
            }
            return true;
        }

        public static bool isExpoTypeValid(string value)
        {
            if (value.ToLower() == "txt" || value.ToLower() == "img" || value.ToLower() == "imgtxt" || value.ToLower() == "txtaud" || value.ToLower() == "imgaud")
                return true;
            return false;
        }

        public static bool isFixPointValid(string value)
        {
            if (value == "+" || value.ToLower() == "o" || value.ToLower() == "false")
                return true;
            return false;
        }

        public static bool isColorLengthValid(int value)
        {
            if (value == 7)
                return true;
            return false;
        }
        public static bool isLengthValid(string value)
        {
            if (value.Length > 0 && value != "abrir")
                return true;
            return false;
        }

        public static bool isExpoEnabled(Button value)
        {
            if (value.Enabled)
                return true;
            return false;
        }

        public static bool isExpoReactValid(string value)
        {
            if (value == "Formas" || value == "Palavra" || value == "Imagem e Palavra" || value == "Imagem" ||
                value == "Palavra com Aúdio" || value == "Imagem com Aúdio")
                return true;
            else
                return false;
        }
    }
}
