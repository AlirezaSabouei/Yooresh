using MediatR;

namespace Villages.UnitTests.Application.Base;

public abstract class CommandHandlerTests<THandler,TCommand,TResult> 
    where THandler : IRequestHandler<TCommand,TResult> 
    where TCommand : IRequest<TResult>
{
    protected TCommand? Command;
    protected THandler? Handler;
    
    protected CommandHandlerTests()
    {
        InitDependencies();
        SetupDependencies();
        Command=CreateValidCommand();
        Handler = CreateCommandHandler();
    }

    protected virtual void InitDependencies()
    {
        
    }
    
    protected virtual void SetupDependencies()
    {
        
    }
    
    protected abstract TCommand CreateValidCommand();
    protected abstract THandler CreateCommandHandler();
}