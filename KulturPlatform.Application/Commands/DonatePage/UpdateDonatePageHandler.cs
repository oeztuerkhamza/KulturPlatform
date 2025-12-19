using AutoMapper;
using KulturPlatform.Application.Interfaces.DonatePage;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.DonatePage
{
    public class UpdateDonatePageHandler : IRequestHandler<UpdateDonatePageCommand, bool>
    {
        private readonly IDonatePageRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public UpdateDonatePageHandler(IDonatePageRepository repo, IMapper mapper, IUnitOfWork uow)
        {
            _repo = repo;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<bool> Handle(UpdateDonatePageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null) return false;

            _mapper.Map(request, entity);

            _repo.Update(entity, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}
