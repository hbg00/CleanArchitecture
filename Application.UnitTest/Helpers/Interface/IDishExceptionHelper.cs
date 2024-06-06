namespace Application.UnitTest.Helpers.Interface
{
    public interface IDishExceptionHelper<THandler, TRequest>
    {
        Task<Exception> ValidationEx(THandler commandHandler, TRequest command);
    }
}
