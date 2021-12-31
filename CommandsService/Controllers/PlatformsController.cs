using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
  [ApiController]
  [Route("api/c/[controller]")]
  public class PlatformsController : ControllerBase
  {
    private readonly ICommandRepo _repository;
    public IMapper _mapper { get; }

    public PlatformsController(ICommandRepo repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }
    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
      Console.WriteLine("--> Ambil Platforms dari CommandService");
      var results = _repository.GetAllPltforms();
      return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(results));
    }

    [HttpPost]
    public ActionResult TestIndboundConnection()
    {
      Console.WriteLine("--> Inbound POST command service");
      return Ok("Inbound test from platforms controller");
    }

  }
}