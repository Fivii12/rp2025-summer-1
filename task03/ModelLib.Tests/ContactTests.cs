namespace ModelLib.Tests
{
    public class ContactTests
    {
        [Theory]
        [MemberData(nameof(CreateContactData))]
        public void Can_create_contact_with_names(string firstName, string middleName, string lastName)
        {
            Contact contact = new Contact(firstName, middleName, lastName);

            Assert.Equal(firstName, contact.FirstName);
            Assert.Equal(middleName, contact.MiddleName);
            Assert.Equal(lastName, contact.LastName);
            Assert.Empty(contact.PhoneNumbers);
            Assert.Null(contact.PrimaryPhoneNumber);
        }

        public static TheoryData<string, string, string> CreateContactData()
        {
            return new TheoryData<string, string, string>
            {
                { "Иван", "Иванович", "Иванов" },
                { "Иван", "", "Иванов" },
                { "Иван", "", "" },
            };
        }

        [Theory]
        [MemberData(nameof(InvalidFirstNameData))]
        public void Cannot_create_contact_with_empty_firstname(string? firstName)
        {
            Assert.Throws<ArgumentException>(() => new Contact(firstName!));
        }

        public static TheoryData<string?> InvalidFirstNameData()
        {
            return new TheoryData<string?>
            {
                { "" },
                { "   " },
                { null },
            };
        }

        [Theory]
        [MemberData(nameof(AddPhoneNumberData))]
        public void AddPhoneNumber_updates_list_and_primary(PhoneNumber first, PhoneNumber? second, PhoneNumber expectedPrimary)
        {
            Contact contact = new Contact("Иван");

            contact.AddPhoneNumber(first);
            if (second != null)
            {
                contact.AddPhoneNumber(second);
            }

            Assert.Contains(first, contact.PhoneNumbers);
            if (second != null)
            {
                Assert.Contains(second, contact.PhoneNumbers);
            }

            Assert.Equal(expectedPrimary, contact.PrimaryPhoneNumber);
        }

        public static TheoryData<PhoneNumber, PhoneNumber?, PhoneNumber> AddPhoneNumberData()
        {
            PhoneNumber p1 = new PhoneNumber("+71234567890");
            PhoneNumber p2 = new PhoneNumber("+79876543210");

            return new TheoryData<PhoneNumber, PhoneNumber?, PhoneNumber>
            {
                { p1, null, p1 },
                { p1, p2, p1 },
            };
        }

        [Fact]
        public void Add_duplicate_phone_number_does_not_duplicate_in_list()
        {
            Contact contact = new Contact("Иван");
            PhoneNumber phone = new PhoneNumber("+71234567890");

            contact.AddPhoneNumber(phone);
            contact.AddPhoneNumber(phone);

            Assert.Single(contact.PhoneNumbers);
            Assert.Equal(phone, contact.PrimaryPhoneNumber);
        }

        [Theory]
        [MemberData(nameof(RemovePhoneNumberData))]
        public void RemovePhoneNumber_updates_primary(PhoneNumber first, PhoneNumber? second, PhoneNumber toRemove, PhoneNumber? expectedPrimary, int expectedCount)
        {
            Contact contact = new Contact("Иван");
            contact.AddPhoneNumber(first);
            if (second != null)
            {
                contact.AddPhoneNumber(second);
            }

            contact.RemovePhoneNumber(toRemove);

            Assert.Equal(expectedCount, contact.PhoneNumbers.Count);
            Assert.Equal(expectedPrimary, contact.PrimaryPhoneNumber);
        }

        public static TheoryData<PhoneNumber, PhoneNumber?, PhoneNumber, PhoneNumber?, int> RemovePhoneNumberData()
        {
            PhoneNumber p1 = new PhoneNumber("+71234567890");
            PhoneNumber p2 = new PhoneNumber("+79876543210");

            return new TheoryData<PhoneNumber, PhoneNumber?, PhoneNumber, PhoneNumber?, int>
            {
                { p1, null, p1, null, 0 },
                { p1, p2, p1, p2, 1 },
                { p1, p2, p2, p1, 1 },
            };
        }

        [Theory]
        [MemberData(nameof(SetPrimaryPhoneNumberData))]
        public void SetPrimaryPhoneNumber_updates_primary(PhoneNumber first, PhoneNumber second, PhoneNumber toSet, PhoneNumber expectedPrimary)
        {
            Contact contact = new Contact("Иван");
            contact.AddPhoneNumber(first);
            contact.AddPhoneNumber(second);

            contact.SetPrimaryPhoneNumber(toSet);

            Assert.Equal(expectedPrimary, contact.PrimaryPhoneNumber);
        }

        public static TheoryData<PhoneNumber, PhoneNumber, PhoneNumber, PhoneNumber> SetPrimaryPhoneNumberData()
        {
            PhoneNumber p1 = new PhoneNumber("+71234567890");
            PhoneNumber p2 = new PhoneNumber("+79876543210");

            return new TheoryData<PhoneNumber, PhoneNumber, PhoneNumber, PhoneNumber>
            {
                { p1, p2, p2, p2 },
                { p1, p2, p1, p1 },
            };
        }

        [Fact]
        public void SetPrimaryPhoneNumber_throws_if_not_added()
        {
            Contact contact = new Contact("Иван");
            PhoneNumber p1 = new PhoneNumber("+71234567890");

            Assert.Throws<InvalidOperationException>(() => contact.SetPrimaryPhoneNumber(p1));
        }
    }
}
