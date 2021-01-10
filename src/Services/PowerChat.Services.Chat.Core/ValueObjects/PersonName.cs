using System.Collections.Generic;
using PowerChat.Common.Domain;

namespace PowerChat.Services.Chat.Core.ValueObjects
{
    public class PersonName : ValueObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => this.ToString();

        public static PersonName Create(string firstName, string lastName)
        {
            var personName = new PersonName
            {
                FirstName = firstName,
                LastName = lastName
            };

            return personName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
