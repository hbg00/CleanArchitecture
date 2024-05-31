using Application.CQRS.Restaurant.Request.Command;
using Application.CQRS.Restaurant.Request.Query;
using Application.DTOs.Restaurant;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestraurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<RestaurantDto>>> GetAll()
        {
            var restaurant = await _mediator.Send(new GetRestaurantsListRequest());
            return Ok(restaurant);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetById(int id)
        {
            var restaurant = await _mediator.Send(new GetRestaurantDetailsRequest { Id = id });
            return Ok(restaurant);
        }

        [HttpGet("admin/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<UpdateRestaurantDto>> GetByIdForAdmin(int id)
        {
            var restaurant = await _mediator.Send(new GetRestaurantDetailsAdminRequest { Id = id });
            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurant)
        {
            var command = new CreateRestaurantCommand { CreateRestaurantDto = createRestaurant };
            var respone = await _mediator.Send(command);
            return Ok(respone);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> UpdateRestaurant(int id, [FromBody] UpdateRestaurantDto updateRestaurant)
        {
            var command = new UpdateRestaurantCommand { Id = id, UpdateRestaurantDto = updateRestaurant };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteRestaurant(int id)
        {
            var command = new DeleteRestaurantCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
