using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommo.Application.Handlers.External.RoomTypes.UpdateRoomTypeCost
{
    public class UpdateRoomTypeCostCommandHandler : IRequestHandler<UpdateRoomTypeCostCommand, GetRoomTypeExternalDto>
    {
        private readonly IBaseWriteRepository<RoomType> _roomType;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoomTypeCostCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public UpdateRoomTypeCostCommandHandler(
            IBaseWriteRepository<RoomType> roomType,
            IMapper mapper,
            ILogger<UpdateRoomTypeCostCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _roomType = roomType;
            _mapper = mapper;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetRoomTypeExternalDto> Handle(UpdateRoomTypeCostCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(r => r.RoomTypeId == idGuid && r.IsActive, cancellationToken);
            if (roomType == null)
            {
                throw new NotFoundException(request);
            }

            roomType.UpdateBaseCost(request.BaseCost);
            _logger.LogInformation($"Cost for room type {request.Id} updated in AccommoMicroservice.");
            _cleanAccommoCacheService.ClearAllCaches();
            return _mapper.Map<GetRoomTypeExternalDto>(await _roomType.UpdateAsync(roomType, cancellationToken));
        }
    }
}
