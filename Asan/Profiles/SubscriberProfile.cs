using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asan.Entities;
using Asan.Models;
using AutoMapper;

namespace Asan.Profiles
{
    public class SubscriberProfile : Profile
    {
        public SubscriberProfile()
        {
            CreateMap<Subscriber, SubscriberQueryDto>();
            CreateMap<SubscriberCreateDto, Subscriber>();
            CreateMap<SubscriberUpdateDto, Subscriber>();
        }
    }
}
