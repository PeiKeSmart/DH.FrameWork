using DH.Clay.Behaviors;

namespace DH.Clay;

public class ClayFactory : Clay
{
    public ClayFactory() : base(new ClayFactoryBehavior(), new ArrayFactoryBehavior())
    {
    }
}