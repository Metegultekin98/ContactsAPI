using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities.Base;
using Core.Persistence.Repositories;
using MediatR;

namespace Core.Application.Base.Commands.Create;

public class CreateCommand<TEntity, TEntityId, TModel> : IRequest<CustomResponseDto<TModel>>
    where TEntity : Entity<TEntityId>
    where TEntityId : struct, IEquatable<TEntityId>
    where TModel : class
{
    public TModel Model { get; set; }

    public CreateCommand(TModel model)
    {
        Model = model;
    }

    public class CreateCommandHandler : IRequestHandler<CreateCommand<TEntity, TEntityId, TModel>, CustomResponseDto<TModel>>
    {
        private readonly IAsyncRepository<TEntity, TEntityId> _asyncRepository;
        private readonly IMapper _mapper;

        public CreateCommandHandler(IAsyncRepository<TEntity, TEntityId> asyncRepository, IMapper mapper)
        {
            _asyncRepository = asyncRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<TModel>> Handle(CreateCommand<TEntity, TEntityId, TModel> request, CancellationToken cancellationToken)
        {
            TEntity entity = _mapper.Map<TEntity>(request.Model);
            TEntity createdEntity = await _asyncRepository.AddAsync(entity);
            TModel createdModel = _mapper.Map<TModel>(createdEntity);

            return CustomResponseDto<TModel>.Success((int)HttpStatusCode.Created, createdModel, isSuccess: true);
        }
    }
}