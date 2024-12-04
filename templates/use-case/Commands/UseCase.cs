using Light.Contracts;

namespace CleanArch.Application.FeatureName.Commands.UseCase;

public record UseCaseCommand : ICommand<object>
{
}

public class UseCaseCommandValidator : AbstractValidator<UseCaseCommand>
{
    public UseCaseCommandValidator()
    {
    }
}

public class UseCaseCommandHandler : ICommandHandler<UseCaseCommand, object>
{
    public Task<object> Handle(UseCaseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
