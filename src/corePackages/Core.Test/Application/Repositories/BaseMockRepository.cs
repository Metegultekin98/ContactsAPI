using AutoMapper;
using Core.Application.Rules;
using Core.Domain.Entities.Base;
using Core.Persistence.Repositories;
using Core.Test.Application.FakeData;
using Core.Test.Application.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Core.Test.Application.Repositories;

public abstract class BaseMockRepository<TRepository, TEntity, TEntityId, TMappingProfile, TBusinessRules, TFakeData>
    where TEntity : Entity<TEntityId>, new()
    where TEntityId : struct, IEquatable<TEntityId>
    where TRepository : class, IAsyncRepository<TEntity, TEntityId>, IRepository<TEntity, TEntityId>
    where TMappingProfile : Profile, new()
    where TBusinessRules : BaseBusinessRules
    where TFakeData : BaseFakeData<TEntity, TEntityId>, new()
{
    public TBusinessRules BusinessRules;
    public IMapper Mapper;
    public Mock<TRepository> MockRepository;

    protected BaseMockRepository(IServiceProvider serviceProvider, TFakeData fakeData)
    {
        MapperConfiguration mapperConfig = new(c => { c.AddProfile<TMappingProfile>(); });
        Mapper = mapperConfig.CreateMapper();
        MockRepository = MockRepositoryHelper.GetRepository<TRepository, TEntity, TEntityId>(fakeData.Data);

        BusinessRules = ActivatorUtilities.CreateInstance<TBusinessRules>(serviceProvider, MockRepository.Object)
                        ?? throw new InvalidOperationException(
                            $"Cannot create an instance of {typeof(TBusinessRules).FullName}.");
    }
}