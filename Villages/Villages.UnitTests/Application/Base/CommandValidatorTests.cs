using FluentValidation;
using MediatR;

namespace Villages.UnitTests.Application.Base;

public abstract class CommandValidatorTests<TValidator, TCommand, TResult>
    where TValidator : AbstractValidator<TCommand>
    where TCommand : IRequest<TResult>
{
    protected TCommand? Command;
    protected TValidator? Validator;

    protected CommandValidatorTests()
    {
        InitDependencies();
        SetupDependencies();
        Command = CreateValidCommand();
        Validator = CreateCommandValidator();
    }

    protected virtual void InitDependencies()
    {
    }

    protected virtual void SetupDependencies()
    {
    }

    protected abstract TCommand CreateValidCommand();
    protected abstract TValidator CreateCommandValidator();
}