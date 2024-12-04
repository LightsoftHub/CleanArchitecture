using Light.Contracts;

namespace CleanArch.Application.FeatureName.Commands.ProcessATask;

public record ProcessATaskCommand : ICommand<int>
{
}

public class ProcessATaskCommandValidator : AbstractValidator<ProcessATaskCommand>
{
    public ProcessATaskCommandValidator()
    {
    }
}

public class ProcessATaskCommandHandler : ICommandHandler<ProcessATaskCommand, int>
{
    public Task<int> Handle(ProcessATaskCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
