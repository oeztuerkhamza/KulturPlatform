using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class UpdateInstagramPostCommandHandler : IRequestHandler<UpdateInstagramPostCommand>
    {
        private readonly IInstagramPostRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInstagramPostCommandHandler(IInstagramPostRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateInstagramPostCommand request, CancellationToken cancellationToken)
        {
            var instagramPost = await _repository.GetByIdAsync(request.Id, cancellationToken);
            
            if (instagramPost == null)
            {
                throw new KeyNotFoundException($"Instagram post with ID {request.Id} not found.");
            }

            var link = !string.IsNullOrWhiteSpace(request.Link) ? Url.Create(request.Link) : null;

            instagramPost.Update(
                Url.Create(request.ImageUrl),
                link
            );

            _repository.Update(instagramPost, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
