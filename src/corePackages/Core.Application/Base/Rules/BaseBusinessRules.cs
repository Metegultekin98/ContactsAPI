using Core.Application.Base.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Base;
using Core.Persistence.Repositories;

namespace Core.Application.Base.Rules;

public class BaseBusinessRules<TEntity, TEntityId> : BaseBusinessRules
    where TEntity : Entity<TEntityId>
    where TEntityId : struct, IEquatable<TEntityId>
{
    private readonly IAsyncRepository<TEntity, TEntityId> _asyncRepository;

    public BaseBusinessRules(IAsyncRepository<TEntity, TEntityId> asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }

    public async Task BaseIdShouldExistWhenSelected(TEntityId id)
    {
        TEntity? baseData = await _asyncRepository.GetAsync(x => x.Id.Equals(id), enableTracking: false);
        if (baseData is null) throw new BusinessException(typeof(TEntity).Name + BaseMessages.EntityDoesNotExist);
    }
}