using System;
using System.Text.RegularExpressions;

namespace ModelLib
{
    public class PhoneNumber
    {
        private readonly string number;
        private readonly string ext;

        /// <summary>
        /// Конструктор, принимает телефонный номер в текстовом виде.
        /// </summary>
        public PhoneNumber(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Телефонный номер не может быть пустым.");
            }

            string mainPart = text;
            string extensionPart = "";

            int xIndex = text.IndexOf('x', StringComparison.OrdinalIgnoreCase);
            if (xIndex >= 0)
            {
                mainPart = text.Substring(0, xIndex);
                extensionPart = text.Substring(xIndex + 1).Trim();
            }

            mainPart = Regex.Replace(mainPart, @"[\s\-\(\)]", "");

            if (mainPart.StartsWith("+"))
            {
                number = mainPart;
            }
            else if (mainPart.StartsWith("8"))
            {
                number = "+" + mainPart;
            }
            else if (mainPart.StartsWith("7"))
            {
                number = "+" + mainPart;
            }
            else
            {
                number = "+" + mainPart;
            }

            if (!Regex.IsMatch(number, @"^\+\d+$"))
            {
                throw new ArgumentException("Некорректный формат номера.");
            }

            int digitCount = number.StartsWith("+") ? number.Length - 1 : number.Length;
            if (digitCount < 8 || digitCount > 16)
            {
                throw new ArgumentException("Некорректная длина номера.");
            }

            if (!string.IsNullOrEmpty(extensionPart) && !Regex.IsMatch(extensionPart, @"^\d+$"))
            {
                throw new ArgumentException("Добавочный номер может содержать только цифры.");
            }

            ext = extensionPart;
        }

        /// <summary>
        /// Основной номер с +
        /// </summary>
        public string Number => number;

        /// <summary>
        /// Добавочный номер или пустая строка
        /// </summary>
        public string Ext => ext;

        /// <summary>
        /// Возвращает строковое представление номера с добавочным номером (если есть)
        /// </summary>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(ext))
            {
                return number + "x" + ext;
            }

            return number;
        }
    }
}
