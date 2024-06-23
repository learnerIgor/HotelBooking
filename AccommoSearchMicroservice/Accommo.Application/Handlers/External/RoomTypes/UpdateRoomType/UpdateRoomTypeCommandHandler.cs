using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Exceptions;
using Accommo.Application.Caches;

namespace Accommo.Application.Handlers.External.RoomTypes.UpdateRoomType
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, GetRoomTypeExternalDto>
    {
        private readonly IBaseWriteRepository<RoomType> _roomType;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoomTypeCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public UpdateRoomTypeCommandHandler(
            IBaseWriteRepository<RoomType> roomType,
            IMapper mapper,
            ILogger<UpdateRoomTypeCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _roomType = roomType;
            _mapper = mapper;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }
        public async Task<GetRoomTypeExternalDto> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(i => i.RoomTypeId == idGuid && i.IsActive, cancellationToken);
            if (roomType == null)
            {
                throw new NotFoundException(request);
            }

            roomType.UpdateName(request.Name);

            var result = await _roomType.UpdateAsync(roomType, cancellationToken);
            _logger.LogInformation($"Type of room {request.Id} updated in AccommoMicroservice.");
            _cleanAccommoCacheService.ClearAllCaches();

            return _mapper.Map<GetRoomTypeExternalDto>(result);
        }
    }
}
