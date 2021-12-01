using Asan.Entities;
using Asan.Helpers;
using Asan.Models;
using Asan.ResourceParams;
using Asan.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/subscribers")]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IMapper _mapper;

        public SubscribersController(ISubscriberRepository subscriberRepository, IMapper mapper)
        {
            _subscriberRepository = subscriberRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ListResponse<SubscriberQueryDto>> GetAll()
        {
            var subscribers = _subscriberRepository.GetAll();
            return Ok(_mapper.Map<List<SubscriberQueryDto>>(subscribers));
        }

        [HttpPost]
        [Route("/GetAll")]
        public ActionResult<ListResponse<SubscriberQueryDto>> GetAll(ListRequest request)
        {
            var subscribers = _subscriberRepository.GetAll(request);
            return Ok(_mapper.Map<List<SubscriberQueryDto>>(subscribers));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, bool includeAddresses = false)
        {
            var subscriber = _subscriberRepository.GetById(id, includeAddresses);
            if (subscriber == null)
                return NotFound();

            return Ok(_mapper.Map<SubscriberQueryDto>(subscriber));
        }

        [HttpPost]
        public IActionResult Create([FromBody] SubscriberCreateDto subscriber)
        {
            if (subscriber is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new InvalidEntityObjectResult(ModelState);

            var subscriberEntity = _mapper.Map<Subscriber>(subscriber);

            return Ok(_subscriberRepository.Create(_mapper.Map<Subscriber>(subscriber)));            
        }

        [HttpPut]
        public IActionResult Update([FromBody] SubscriberUpdateDto subscriber)
        {
            if (subscriber is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new InvalidEntityObjectResult(ModelState);

            if (!_subscriberRepository.EntityExists(subscriber.Id))
                return NotFound();

            var subscriberEntity = _mapper.Map<Subscriber>(subscriber);

            return Ok(_subscriberRepository.Update(subscriberEntity));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSubscriber(int id)
        {
            if (id <= 0)
                return BadRequest();

            var subscriberEntity = _subscriberRepository.GetById(id, true);
            if (subscriberEntity is null)
                return NotFound();

            return Ok(_subscriberRepository.Delete(subscriberEntity));
        }
    }
}
