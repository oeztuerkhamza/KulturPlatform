using KulturPlatform.Application.Interfaces.ValueItem;
using MediatR;

namespace KulturPlatform.Application.Commands.ValueItem
{
    public class DeleteValueItemCommandHandler : IRequestHandler<DeleteValueItemCommand>
    {
        private readonly IValueItemWriteRepository _valueItemWriteRepository;

        public DeleteValueItemCommandHandler(IValueItemWriteRepository valueItemWriteRepository)
        {
            _valueItemWriteRepository = valueItemWriteRepository;
        }

        public async Task Handle(DeleteValueItemCommand request, CancellationToken cancellationToken)
        {
            var valueItem = await _valueItemWriteRepository.GetByIdAsync(request.Id, cancellationToken);
            if (valueItem == null)
                throw new KeyNotFoundException($"ValueItem with Id {request.Id} not found.");

            // _valueItemWriteRepository.Delete(valueItem, cancellationToken);
        }
    }
}
