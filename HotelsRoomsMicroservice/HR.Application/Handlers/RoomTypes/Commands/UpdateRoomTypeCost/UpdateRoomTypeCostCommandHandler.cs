using AutoMapper;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Caches;
using HR.Application.Exceptions;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.RoomTypes.Commands.UpdateRoomTypeCost
{
    public class UpdateRoomTypeCostCommandHandler : IRequestHandler<UpdateRoomTypeCostCommand, GetRoomTypeDto>
    {
        private readonly IBaseWriteRepository<RoomType> _roomType;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoomTypeCostCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly IRoomTypeProvider _roomTypeProvider;
        private readonly ICurrentUserService _currentUserService;

        public UpdateRoomTypeCostCommandHandler(
            IBaseWriteRepository<RoomType> roomType, 
            IMapper mapper, 
            ILogger<UpdateRoomTypeCostCommandHandler> logger, 
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            IRoomTypeProvider roomTypeProvider,
            ICurrentUserService currentUserService)
        {
            _roomType = roomType;
            _mapper = mapper;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _roomTypeProvider = roomTypeProvider;
            _currentUserService = currentUserService;
        }

        public async Task<GetRoomTypeDto> Handle(UpdateRoomTypeCostCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(r => r.RoomTypeId == idGuid && r.IsActive, cancellationToken);
            if (roomType == null)
            {
                throw new NotFoundException(request);
            }

            roomType.UpdateBaseCost(request.BaseCost);
            await _roomTypeProvider.UpdateCostRoomTypeAsync(_currentUserService.Token, request.Id, roomType, cancellationToken);
            _logger.LogInformation($"Cost for room type  {request.Id} updated");
            _cleanHotelRoomCacheService.ClearAllCaches();
            return _mapper.Map<GetRoomTypeDto>(await _roomType.UpdateAsync(roomType, cancellationToken));
        }
    }
}
