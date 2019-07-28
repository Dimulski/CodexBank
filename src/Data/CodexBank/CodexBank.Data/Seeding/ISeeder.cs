using System.Threading.Tasks;

namespace CodexBank.Data.Seeding
{
    public interface ISeeder
    {
        Task Seed();
    }
}
