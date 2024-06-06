using Application.CQRS.Dish.Handler.Command;
using Application.CQRS.Dish.Request.Command;
using Application.Exceptions;
using Application.UnitTest.Helpers.Interface;
using FluentValidation;

namespace Application.UnitTest.Helpers
{
    public class CreateDishExceptionHelper : IDishExceptionHelper<CreateDishCommandHandler, CreateDishCommand>
    {     
        public async Task<Exception> ValidationEx(CreateDishCommandHandler handler, CreateDishCommand command)
        {
            try
            {
                await handler.Handle(command, CancellationToken.None);
                return null;
            }
            catch (NotFoundException ex)
            {
                return ex;
            }
            catch (ValidationException ex)
            {
                return ex;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
