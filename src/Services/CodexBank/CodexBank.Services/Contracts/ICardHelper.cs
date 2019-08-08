using System;
using System.Collections.Generic;
using System.Text;

namespace CodexBank.Services.Contracts
{
    public interface ICardHelper
    {
        bool CheckLuhn(string creditCardNumber);

        string Generate16DigitNumber();

        string Generate3DigitSecurityCode();
    }
}
