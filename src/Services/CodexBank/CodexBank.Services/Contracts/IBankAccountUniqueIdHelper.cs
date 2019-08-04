namespace CodexBank.Services.Contracts
{
    public interface IBankAccountUniqueIdHelper
    {
        string GenerateAccountUniqueId();

        bool IsUniqueIdValid(string id);
    }
}
