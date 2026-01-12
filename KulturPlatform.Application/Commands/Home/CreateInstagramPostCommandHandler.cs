using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class CreateInstagramPostCommandHandler : IRequestHandler<CreateInstagramPostCommand, Guid>
    {
        private readonly IInstagramPostRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateInstagramPostCommandHandler(IInstagramPostRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateInstagramPostCommand request, CancellationToken cancellationToken)
        {
            var link = !string.IsNullOrWhiteSpace(request.Link) ? Url.Create(request.Link) : null;

            var instagramPost = InstagramPost.Create(
                Url.Create(request.ImageUrl),
                link
            );

            await _repository.AddAsync(instagramPost, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return instagramPost.Id;
        }
    }
}
