using Application.CQRS.Dish.Request.Command;
using Application.CQRS.Dish.Request.Query;
using Application.DTOs.Dish;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestraurantAPI.Controllers
{
    [Route("api/{restaurantId}/[controller]")]
    [ApiController]
    [Authorize]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<DishDto>>> GetAll([FromRoute] int restaurantId)
        {
            var restaurant = await _mediator.Send(new GetDishesListRequest { RestaurantId = restaurantId });
            return Ok(restaurant);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetById([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var restaurant = await _mediator.Send(new GetDishDetailsRequest { RestaurantId = restaurantId, Id = dishId });
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRestaurant([FromRoute] int restaurantId, [FromBody] CreateDishDto createDishDto)
        {
            var command = new CreateDishCommand { RestaurantId = restaurantId, CreateDishDto = createDishDto };
            var respone = await _mediator.Send(command);
            return Ok(respone);
        }

        [HttpPut("{dishId}")]
        public async Task<ActionResult> UpdateRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId, [FromBody] UpdateDishDto updateDish)
        {
            var command = new UpdateDishCommand { Id = dishId, RestaurantId = restaurantId, UpdateDishDto = updateDish };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{dishId}")]
        public async Task<ActionResult> DeleteRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var command = new DeleteDishCommand { Id = dishId, RestaurantId = restaurantId };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
