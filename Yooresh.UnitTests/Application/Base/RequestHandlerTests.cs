using MediatR;

namespace Yooresh.UnitTests.Application.Base;

public abstract class RequestHandlerTests<THandler,TRequest,TResult> 
    where THandler : IRequestHandler<TRequest,TResult> 
    where TRequest : IRequest<TResult>
{
    protected TRequest? Request;
    protected THandler? Handler;
    
    protected RequestHandlerTests()
    {
        InitDependencies();
        SetupDependencies();
        Request = CreateValidRequest();
        Handler = CreateRequestHandler();
    }

    protected virtual void InitDependencies()
    {
        
    }
    
    protected virtual void SetupDependencies()
    {
        
    }
    
    protected abstract TRequest CreateValidRequest();
    protected abstract THandler CreateRequestHandler();
}