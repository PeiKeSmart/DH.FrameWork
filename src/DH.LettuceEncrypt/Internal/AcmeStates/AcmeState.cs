using Microsoft.Extensions.DependencyInjection;

namespace LettuceEncrypt.Internal.AcmeStates;

internal interface IAcmeState
{
    Task<IAcmeState> MoveNextAsync(CancellationToken cancellationToken);
}

internal class TerminalState : IAcmeState
{
    public static TerminalState Singleton { get; } = new();

    private TerminalState() { }

    public Task<IAcmeState> MoveNextAsync(CancellationToken cancellationToken)
    {
        throw new OperationCanceledException();
    }
}

internal abstract class AcmeState : IAcmeState
{
    private readonly AcmeStateMachineContext _context;

    public AcmeState(AcmeStateMachineContext context)
    {
        _context = context;
    }

    public abstract Task<IAcmeState> MoveNextAsync(CancellationToken cancellationToken);

    protected T MoveTo<T>() where T : IAcmeState
    {
        return _context.Services.GetRequiredService<T>();
    }
}

internal abstract class SyncAcmeState : AcmeState
{
    protected SyncAcmeState(AcmeStateMachineContext context) : base(context)
    {
    }

    public override Task<IAcmeState> MoveNextAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var next = MoveNext();

        return Task.FromResult(next);
    }

    public abstract IAcmeState MoveNext();
}
