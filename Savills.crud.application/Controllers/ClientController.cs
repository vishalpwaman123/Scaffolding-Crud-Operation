using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Savills.SIA.Entities.Models;
using Savills.SIA.Models.Dto.Client;
using Savills.SIA.Services.Interface;

namespace Savills.SIA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService; 
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(CreateClientRequest request)
        {
            var siaClient = _mapper.Map<Siaclient>(request);
            var result = await _clientService.Create(siaClient);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(UpdateClientRequest request)
        {
            var result = await _clientService.Update(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Siaclient>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _clientService.GetList();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _clientService.Delete(id);
            return Ok(result);
        }
    }
}
