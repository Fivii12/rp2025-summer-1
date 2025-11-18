using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelLib;


public class Contact
{
    private readonly List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();
    private PhoneNumber? primaryPhoneNumber;

    public Contact(string firstName, string middleName = "", string lastName = "")
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("FirstName не может быть пустым.", nameof(firstName));
        }

        FirstName = firstName;
        MiddleName = middleName ?? "";
        LastName = lastName ?? "";
    }

    /// <summary>
    /// Имя контакта.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Отчество контакта.
    /// </summary>
    public string MiddleName { get; private set; }

    /// <summary>
    /// Фамилия контакта.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Список всех телефонных номеров контакта.
    /// </summary>
    public IReadOnlyList<PhoneNumber> PhoneNumbers => phoneNumbers.AsReadOnly();

    /// <summary>
    /// Основной (primary) номер телефона контакта. Может быть null, если номеров нет.
    /// </summary>
    public PhoneNumber? PrimaryPhoneNumber => primaryPhoneNumber;

    /// <summary>
    /// Добавляет телефонный номер к контакту. Если это первый номер, он становится основным.
    /// </summary>
    public void AddPhoneNumber(PhoneNumber value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!phoneNumbers.Contains(value))
        {
            phoneNumbers.Add(value);

            if (primaryPhoneNumber == null)
            {
                primaryPhoneNumber = value;
            }
        }
    }

    /// <summary>
    /// Удаляет телефонный номер из контакта. Если удаляется основной номер, новый основной номер выбирается автоматически.
    /// </summary>
    public void RemovePhoneNumber(PhoneNumber value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (phoneNumbers.Contains(value))
        {
            phoneNumbers.Remove(value);

            if (primaryPhoneNumber == value)
            {
                primaryPhoneNumber = phoneNumbers.FirstOrDefault();
            }
        }
    }

    /// <summary>
    /// Устанавливает существующий номер телефона как основной (primary).
    /// </summary>
    public void SetPrimaryPhoneNumber(PhoneNumber value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!phoneNumbers.Contains(value))
        {
            throw new InvalidOperationException("Номер должен быть добавлен перед тем как сделать его основным.");
        }

        primaryPhoneNumber = value;
    }
}
