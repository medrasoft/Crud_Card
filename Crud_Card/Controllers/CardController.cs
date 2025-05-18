using AutoMapper;
using Crud_Card.DAL;
using Crud_Card.DAL.Entities;
using Crud_Card.DAL.Interfaces;
using Crud_Card.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_Card.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CardController(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] CardDto carddto)
        {
            if ( !ModelState.IsValid )
                return BadRequest(ModelState);

            var card = _mapper.Map<Card>(carddto);
            await _unitOfWork.Cards.AddAsync(card);
            await _unitOfWork.CreateAsync();

            return CreatedAtAction(nameof(GetCardById) , new { id = card.Id } , card);
        }

        [HttpGet]
        public async Task<ActionResult<List<CardDto>>> Get()
        {
            var result = await _unitOfWork.Cards.GetAllAsync();
            if ( result == null || !result.Any() )
                return NoContent();

            var listaCards = _mapper.Map<List<CardDto>>(result);
            return Ok(listaCards);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCard([FromBody] CardDto carddto)
        {
            if ( !ModelState.IsValid )
                return BadRequest(ModelState);

            var existingCard = await _unitOfWork.Cards.GetByIdAsync(carddto.Id);
            if ( existingCard == null )
                return NotFound();

            _mapper.Map(carddto , existingCard);
            _unitOfWork.Cards.Update(existingCard);
            await _unitOfWork.CreateAsync();

            return NoContent();
        }

        

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCardById(int id)
        {
            var result = await _unitOfWork.Cards.GetByIdAsync(id);
            if ( result == null )
                return NotFound();

            var carddto = _mapper.Map<CardDto>(result);
            return Ok(carddto);
        }


       
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCard(int id)
        {
            var card = await _unitOfWork.Cards.GetByIdAsync(id);
            if ( card == null )
                return NotFound();

            _unitOfWork.Cards.Remove(card);
            await _unitOfWork.CreateAsync();

            return NoContent();
        }


    }
}
