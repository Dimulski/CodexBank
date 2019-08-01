namespace CodexBank.Common
{
    public static class ModelConstants
    {
        public static class User
        {
            public const int FirstNameMaxLength = 16;
            public const int MiddleNameMaxLength = 16;
            public const int LastNameMaxLength = 16;
            public const int FullNameMaxLength = 48;
        }

        public static class BankAccount
        {
            public const int NameMaxLength = 35;
            public const int UniqueIdMaxLength = 34;
            
        }

        public static class Card
        {
            public const int NameMaxLength = 50;
            public const int ExpiryDateMaxLength = 5;
            public const int SecurityCodeMaxLength = 3;
        }

        public static class Transaction
        {
            public const int DescriptionMaxLength = 150;
            public const string MinStartingPrice = "0.01";
            public const string MaxStartingPrice = "79228162514264337593543950335";
        }
    }
}
