using System;
using System.Collections.Generic;
using StringLib;
using Xunit;

namespace StringLib.Tests
{
    public class TextUtilTest
    {
        [Theory]
        [MemberData(nameof(CountConsonantsParams))]
        public void Can_count_consonants(string input, int expected)
        {
            int result = TextUtil.CountConsonants(input);
            Assert.Equal(expected, result);
        }

        public static TheoryData<string, int> CountConsonantsParams()
        {
            return new TheoryData<string, int>
            {
                { "The quick brown fox jumps over the lazy dog", 23 },
                { "Съешь же ещё этих мягких французских булок, да выпей чаю.", 26 },
                { "Hello, мир!", 5 },
                { null!, 0 },
                { "", 0 },
                { "   ", 0 },
                { "12345", 0 },
                { "@#$%^", 0 },
                { "аеёиоуыэюя", 0 },
                { "aeiou", 0 },
                { "bcdfghjklmnpqrstvwxz", 20 },
                { "BCDFGHJKLMNPQRSTVWXZ", 20 },
                { "бвгджзйклмнпрстфхцчшщ", 21 },
                { "БВГДЖЗЙКЛМНПРСТФХЦЧШЩ", 21 },
                { "-q-", 1 },
                { "-й-", 1 },
                { "i", 0 },
                { "я", 0 },
                { "b", 1 },
                { "б", 1 },
            };
        }
    }
}
