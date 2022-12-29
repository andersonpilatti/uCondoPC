using Core.Interfaces.Base;
using Data.Interfaces.Repositories;
using Domain.Model;
using System.Linq.Expressions;

namespace Data.Repository
{
    public class PlanoContaRepository
        : IPlanoContaRepository
    {
        private readonly IBaseRepository<PlanoContaEntity> _baseRepository;
        public PlanoContaRepository(IBaseRepository<PlanoContaEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddAsync(PlanoContaEntity entity)
        {
            await _baseRepository.AddAssync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<PlanoContaEntity, bool>> match)
        {
            return await _baseRepository.AnyAsync(match);
        }

        public async Task DeleteAsync(int id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PlanoContaEntity>> ListAsync(Expression<Func<PlanoContaEntity, bool>> predicate)
        {
            return await _baseRepository.ListAsync(predicate);
        }
    }
}