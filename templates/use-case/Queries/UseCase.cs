using Light.Contracts;

namespace CleanArch.Application.FeatureName.Queries.UseCase;

public record UseCaseQuery : IQuery<object>
{
}

public class UseCaseQueryValidator : AbstractValidator<UseCaseQuery>
{
    public UseCaseQueryValidator()
    {
    }
}

public class UseCaseQueryHandler : IQueryHandler<UseCaseQuery, object>
{
    public Task<object> Handle(UseCaseQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
