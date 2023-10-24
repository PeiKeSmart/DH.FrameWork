namespace LettuceEncrypt.Internal.AcmeStates;

internal class AcmeStateMachineContext
{
    public IServiceProvider Services { get; }

    public AcmeStateMachineContext(IServiceProvider services)
    {
        Services = services;
    }
}
