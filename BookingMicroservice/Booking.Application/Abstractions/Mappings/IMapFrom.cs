﻿using AutoMapper;

namespace Booking.Application.Abstractions.Mappings
{
    public interface IMapFrom<T>
    {
        void CreateMap(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
