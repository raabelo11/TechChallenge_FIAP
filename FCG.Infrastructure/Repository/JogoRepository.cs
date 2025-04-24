using FCG.Domain.Interface;
using FCG.Domain.Models;
using FCG.Infrastructure.Context;

namespace FCG.Infrastructure.Repository
{
    public class JogoRepository : ReposityGeneric<Jogos>, IJogoRepository
    {
        public JogoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
