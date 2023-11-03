using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
     
    public class DriverController : BaseApiController
    {
        private IUnitOfWork unitofwork;
        private readonly IMapper mapper;

        public DriverController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            this.unitofwork = unitOfWork;
            this.mapper = Mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<DriverDto>>> Get([FromQuery]Params DriverParams)
        {
            var Driver = await unitofwork.Drivers.GetAllAsync(DriverParams.PageIndex,DriverParams.PageSize, DriverParams.Search,"Name");
            var listaDriversDto= mapper.Map<List<DriverDto>>(Driver.registros);
            return new Pager<DriverDto>(listaDriversDto, Driver.totalRegistros,DriverParams.PageIndex,DriverParams.PageSize,DriverParams.Search);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DriverDto>> Get(int id)
        {
            var Driver = await unitofwork.Drivers.GetByIdAsync(id);
            return mapper.Map<DriverDto>(Driver);
        }


          [HttpPost]
          [ProducesResponseType(StatusCodes.Status201Created)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          public async Task<ActionResult<Driver>> Post([FromBody]DriverDto DriverDto)
          {
              var Driver = mapper.Map<Driver>(DriverDto);
              unitofwork.Drivers.Add(Driver);
              await unitofwork.SaveAsync();

              if (Driver == null){
                  return BadRequest();
              }
              DriverDto.Id = Driver.Id;
              return CreatedAtAction(nameof(Post), new {id = DriverDto.Id}, DriverDto); 
           }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<DriverDto>> Put(int id, [FromBody]DriverDto DriverDto){
            if(DriverDto == null)
                return NotFound();

            var Driver = this.mapper.Map<Driver>(DriverDto);
            unitofwork.Drivers.Update(Driver);
            await unitofwork.SaveAsync();
            return DriverDto;
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete (int id){
            var Driver = await unitofwork.Drivers.GetByIdAsync(id);
            if(Driver == null)
                return NotFound();
            
            unitofwork.Drivers.Remove(Driver);
            await unitofwork.SaveAsync();
            return NoContent();    }
    }

}