using FluentValidation;
using MediatR;
using NUnit.Framework;
using Shouldly;

namespace Yooresh.UnitTests.Application.Base;

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

    [Test]
    public async Task Validator_CommandIsValid_ValidationTrue()
    {
        //Arrange
        // Command is valid

        //Act
        var result = await Validator!.ValidateAsync(Command);

        //Assert
        result.IsValid.ShouldBe(true);
    }
}