using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringLib
{
    public static class TextUtil
    {
        private const string ENGLISHCONSONANTS = "BCDFGHJKLMNPQRSTVWXZbcdfghjklmnpqrstvwxz";
        private const string RUSSIANCONSONANTS = "БВГДЖЗЙКЛМНПРСТФХЦЧШЩбвгджзйклмнпрстфхцчшщ";
        private const string ALLCONSONANTS = ENGLISHCONSONANTS + RUSSIANCONSONANTS;

        /// <summary>
        /// Разбивает строку на слова, учитывая дефисы и апострофы.
        /// </summary>
        public static List<string> SplitIntoWords(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new List<string>();
            }

            const string pattern = @"\p{L}+(?:[\-\']\p{L}+)*";
            Regex regex = new(pattern, RegexOptions.Compiled);

            return regex.Matches(text)
                .Select(match => match.Value)
                .ToList();
        }

        /// <summary>
        /// Подсчитывает количество согласных букв в строке (русских и английских).
        /// </summary>
        public static int CountConsonants(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            int consonantsAmount = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (ALLCONSONANTS.Contains(text[i]))
                {
                    consonantsAmount++;
                }
            }

            return consonantsAmount;
        }
    }
}
