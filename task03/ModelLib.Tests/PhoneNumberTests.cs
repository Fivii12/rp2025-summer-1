namespace ModelLib.Tests
{
    public class PhoneNumberTests
    {
        [Theory]
        [MemberData(nameof(ValidPhoneNumbersData))]
        public void Can_create_phone_number(string input, string expectedNumber, string expectedExt)
        {
            PhoneNumber phone = new PhoneNumber(input);

            Assert.Equal(expectedNumber, phone.Number);
            Assert.Equal(expectedExt, phone.Ext);

            if (string.IsNullOrEmpty(expectedExt))
            {
                Assert.Equal(expectedNumber, phone.ToString());
            }
            else
            {
                Assert.Equal(expectedNumber + "x" + expectedExt, phone.ToString());
            }
        }

        [Theory]
        [MemberData(nameof(EdgeLengthPhoneNumbersData))]
        public void Can_create_phone_number_with_edge_lengths(string input, string expectedNumber)
        {
            PhoneNumber phone = new PhoneNumber(input);

            Assert.Equal(expectedNumber, phone.Number);
        }

        public static TheoryData<string, string> EdgeLengthPhoneNumbersData()
        {
            return new TheoryData<string, string>
            {
                { "12345678", "+12345678" },
                { "+12345678", "+12345678" },
                { "1234567890123456", "+1234567890123456" },
                { "+1234567890123456", "+1234567890123456" },
            };
        }

        public static TheoryData<string, string, string> ValidPhoneNumbersData()
        {
            return new TheoryData<string, string, string>
            {
                { "+71234567890", "+71234567890", "" },
                { "81234567890", "+81234567890", "" },
                { "71234567890", "+71234567890", "" },
                { "12345678", "+12345678", "" },
                { "+71234567890x123", "+71234567890", "123" },
                { "8(123)456-78-90x001", "+81234567890", "001" },
            };
        }

        [Theory]
        [MemberData(nameof(InvalidPhoneNumbersData))]
        public void Cannot_create_phone_number_with_invalid_input(string input, string expectedMessage)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new PhoneNumber(input));

            Assert.Contains(expectedMessage, ex.Message);
        }

        public static TheoryData<string, string> InvalidPhoneNumbersData()
        {
            return new TheoryData<string, string>
            {
                { "", "Телефонный номер не может быть пустым" },
                { "      ", "Телефонный номер не может быть пустым" },
                { "+12abc__345678", "Некорректный формат номера" },
                { "1234567", "Некорректная длина номера" },
                { "12345678901234567", "Некорректная длина номера" },
                { "+12345678xa1b", "Добавочный номер может содержать только цифры" },
            };
        }

        [Fact]
        public void ToString_returns_correct_format()
        {
            PhoneNumber phone1 = new PhoneNumber("+71234567890");
            PhoneNumber phone2 = new PhoneNumber("+71234567890x123");

            Assert.Equal("+71234567890", phone1.ToString());
            Assert.Equal("+71234567890x123", phone2.ToString());
        }
    }
}
