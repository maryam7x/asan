using Asan.Entities;
using Asan.Helpers;
using Asan.Models;
using Asan.ResourceParams;
using Asan.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/subscriber/{subscriberId}/addresses")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public AddressesController(IAddressRepository addressRepository, ISubscriberRepository subscriberRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _addressRepository = addressRepository;
            _subscriberRepository = subscriberRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult GetAddresses(int subscriberId)
        {
            if (subscriberId <= 0)
                return BadRequest();

            //ToDo: check subscriberId of current user (that have been logged in) is Equal to subscriberId parameter.

            if (!_subscriberRepository.EntityExists(subscriberId))
                return NotFound();

            var addresses = _addressRepository.GetSubscriberAddresses(new AddressListRequest() { SubescriberId = subscriberId });

            var result = _mapper.Map<List<AddressQueryDto>>(addresses);

            result = result.Select(address =>
            {
                address = CreateLinkForAddress(address);
                return address;
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetAddress")]
        public IActionResult GetAddress(int subscriberId, int id)
        {
            if (subscriberId <= 0 || id <= 0)
                return BadRequest();

            //ToDo: check subscriberId of current user (that have been logged in) is Equal to subscriberId parameter.

            if (!_subscriberRepository.EntityExists(subscriberId))
                return NotFound();
            var address = _addressRepository.GetSubscriberAddress(subscriberId, id);

            if (address is null)
                return NotFound();

            return Ok(CreateLinkForAddress(_mapper.Map<AddressQueryDto>(address)));
        }

        [HttpPost]
        public IActionResult AddSubscriberAddress(int subscriberId, AddressCreateDto address)
        {
            if (address is null || subscriberId <= 0)
                return BadRequest();

            if (!ModelState.IsValid)
                return new InvalidEntityObjectResult(ModelState);

            //ToDo: check subscriberId of current user (that have been logged in) is Equal to subscriberId parameter.

            if (!_subscriberRepository.EntityExists(subscriberId))
                return NotFound();

            var addressEntity = _mapper.Map<Address>(address);
            addressEntity.SubscriberId = subscriberId;
            return Ok(_addressRepository.Create(addressEntity));
        }

        [HttpPut("{id}", Name = "UpdateSubscriberAddress")]
        public IActionResult UpdateSubscriberAddress(int subscriberId, AddressUpdateDto address)
        {
            if (subscriberId <= 0)
                return BadRequest();

            if (!ModelState.IsValid)
                return new InvalidEntityObjectResult(ModelState);

            //ToDo: check subscriberId of current user (that have been logged in) is Equal to subscriberId parameter.

            if (!_subscriberRepository.EntityExists(subscriberId))
                return NotFound();

            var addressEntity = _addressRepository.GetById(address.Id);
            if (addressEntity is null)
                return NotFound();

            //ToDo: check subscriberId of current user (that have been logged in) is Equal to addressEntity.SubscriberId.

            _mapper.Map(address, addressEntity);
            return Ok(_addressRepository.Update(addressEntity));
        }

        [HttpDelete("{id}", Name = "DeleteSubscriberAddress")]
        public IActionResult DeleteSubscriberAddress(int subscriberId, int id)
        {
            if (subscriberId <= 0 || id <= 0)
                return BadRequest();

            //ToDo: check subscriberId of current user (that have been logged in) is Equal to subscriberId of address parameter.

            if (!_subscriberRepository.EntityExists(subscriberId))
                return NotFound();

            var addressEntity = _addressRepository.GetById(id);
            if (addressEntity is null)
                return NotFound();

            //ToDo: check subscriberId of current user (that have been logged in) is Equal to addressEntity.SubscriberId.

            return Ok(_addressRepository.Delete(addressEntity));
        }

        private AddressQueryDto CreateLinkForAddress(AddressQueryDto address)
        {           
            address.Links.Add(new Linkdto(_linkGenerator.GetUriByAction(HttpContext, nameof(GetAddress), values: new { address.SubscriberId, address.Id }), "self", "GET"));

            address.Links.Add(new Linkdto(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateSubscriberAddress), values: new { address.SubscriberId, address.Id }), "update_address", "PUT"));

            address.Links.Add(new Linkdto(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteSubscriberAddress), values: new { address.SubscriberId, address.Id }), "delete_address", "DELETE"));

            return address;
        }
    }
}
