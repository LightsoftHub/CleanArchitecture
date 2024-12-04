using Light.Contracts;

namespace CleanArch.Application.FeatureName.Commands.ProcessATask1;

public record ProcessATask1Command : ICommand<int>
{
}

public class ProcessATask1CommandValidator : AbstractValidator<ProcessATask1Command>
{
    public ProcessATask1CommandValidator()
    {
    }
}

public class ProcessATask1CommandHandler : ICommandHandler<ProcessATask1Command, int>
{
    public Task<int> Handle(ProcessATask1Command request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
