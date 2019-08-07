using AutoMapper;

namespace CodexBank.Common.AutoMapping.Contracts
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
