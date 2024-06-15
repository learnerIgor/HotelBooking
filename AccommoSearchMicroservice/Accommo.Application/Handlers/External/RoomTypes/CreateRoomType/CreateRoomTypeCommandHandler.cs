using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommo.Application.Handlers.External.RoomTypes.CreateRoomType
{
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, GetRoomTypeExternalDto>
    {
        private readonly IBaseWriteRepository<RoomType> _roomType;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoomTypeCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
        public CreateRoomTypeCommandHandler(
            IBaseWriteRepository<RoomType> roomType,
            IMapper mapper,
            ILogger<CreateRoomTypeCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _roomType = roomType;
            _mapper = mapper;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetRoomTypeExternalDto> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _roomType.AsAsyncRead().SingleOrDefaultAsync(r => r.Name == request.Name && r.IsActive, cancellationToken);
            if (isExist != null)
            {
                throw new BadOperationException($"Type of room with name {request.Name} already exists in AccommoMicroservice.");
            }

            var roomType = new RoomType(request.RoomTypeId, request.Name, request.BaseCost, true);

            roomType = await _roomType.AddAsync(roomType, cancellationToken);
            _logger.LogInformation($"New type of room {roomType.RoomTypeId} created in AccommoMicroservice.");
            _cleanAccommoCacheService.ClearListCaches();

            return _mapper.Map<GetRoomTypeExternalDto>(roomType);
        }
    }
}
