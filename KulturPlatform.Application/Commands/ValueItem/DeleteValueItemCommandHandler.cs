using KulturPlatform.Application.Interfaces.ValueItem;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.ValueItem
{
    public class DeleteValueItemCommandHandler : IRequestHandler<DeleteValueItemCommand>
    {
        private readonly IValueItemWriteRepository _valueItemWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteValueItemCommandHandler(
            IValueItemWriteRepository valueItemWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _valueItemWriteRepository = valueItemWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteValueItemCommand request, CancellationToken cancellationToken)
        {
            var valueItem = await _valueItemWriteRepository.GetByIdAsync(request.Id, cancellationToken);
            if (valueItem == null)
                throw new KeyNotFoundException($"ValueItem with Id {request.Id} not found.");

            _valueItemWriteRepository.Delete(valueItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
