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
     
    public class TeamController : BaseApiController
    {
        private IUnitOfWork unitofwork;
        private readonly IMapper mapper;

        public TeamController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            this.unitofwork = unitOfWork;
            this.mapper = Mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<TeamDto>>> Get([FromQuery]Params TeamParams)
        {
            var Team = await unitofwork.Teams.GetAllAsync(TeamParams.PageIndex,TeamParams.PageSize, TeamParams.Search,"Name");
            var listaTeamsDto= mapper.Map<List<TeamDto>>(Team.registros);
            return new Pager<TeamDto>(listaTeamsDto, Team.totalRegistros,TeamParams.PageIndex,TeamParams.PageSize,TeamParams.Search);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeamDto>> Get(int id)
        {
            var Team = await unitofwork.Teams.GetByIdAsync(id);
            return mapper.Map<TeamDto>(Team);
        }


          [HttpPost]
          [ProducesResponseType(StatusCodes.Status201Created)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          public async Task<ActionResult<Team>> Post([FromBody]TeamDto TeamDto)
          {
              var Team = mapper.Map<Team>(TeamDto);
              unitofwork.Teams.Add(Team);
              await unitofwork.SaveAsync();

              if (Team == null){
                  return BadRequest();
              }
              TeamDto.Id = Team.Id;
              return CreatedAtAction(nameof(Post), new {id = TeamDto.Id}, TeamDto); 
           }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<TeamDto>> Put(int id, [FromBody]TeamDto TeamDto){
            if(TeamDto == null)
                return NotFound();

            var Team = this.mapper.Map<Team>(TeamDto);
            unitofwork.Teams.Update(Team);
            await unitofwork.SaveAsync();
            return TeamDto;
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete (int id){
            var Team = await unitofwork.Teams.GetByIdAsync(id);
            if(Team == null)
                return NotFound();
            
            unitofwork.Teams.Remove(Team);
            await unitofwork.SaveAsync();
            return NoContent();    }
    }

}