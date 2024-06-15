﻿using AutoMapper;

namespace HR.Application.Abstractions.Mappings
{
    public interface IMapFrom<T>
    {
        void CreateMap(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
