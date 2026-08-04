using Mulkora.Entity.Concrete;

namespace Mulkora.DataAccess.Abstract;

public interface IAgentDal : IGenericDal<Agent>
{
    Task<List<Agent>> GetAllWithUserAsync();
    Task<Agent> GetWithUserByIdAsync(int id);
}