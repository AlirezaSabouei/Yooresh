using MediatR;
using NUnit.Framework;

namespace Yooresh.UnitTests.Application.Base;

public abstract class RequestHandlerTests<THandler,TRequest,TResult> 
    where THandler : IRequestHandler<TRequest,TResult> 
    where TRequest : IRequest<TResult>
{
    protected TRequest? Request;
    protected THandler? Handler;

    [SetUp]
    protected async Task RunBeforeEachTest()
    {
        await InitDependenciesAsync();
        await SetupDependenciesAsync();
        Request = CreateValidRequest();
        Handler = CreateRequestHandler();
    }

    protected virtual Task InitDependenciesAsync()
    {
        return Task.CompletedTask;
    }
    
    protected virtual Task SetupDependenciesAsync()
    {
        return Task.CompletedTask;
    }
    
    protected abstract TRequest CreateValidRequest();
    protected abstract THandler CreateRequestHandler();
}