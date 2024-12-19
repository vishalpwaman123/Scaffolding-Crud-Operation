using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Savills.SIA.Entities.Models;
using Savills.SIA.Models.Dto.Client;
using Savills.SIA.Models.Dto.Contact;
using Savills.SIA.Services.Interface;

namespace Savills.SIA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContactType), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(CreateContactTypeRequest request)
        {
            var contactType = _mapper.Map<ContactType>(request);
            var result = await _contactService.Create(contactType);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ContactType), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(UpdateContactTypeRequest request)
        {
            var contactType = _mapper.Map<ContactType>(request);
            var result = await _contactService.Update(contactType);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ContactType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _contactService.GetList();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _contactService.Delete(id);
            return Ok(result);
        }

    }
}
