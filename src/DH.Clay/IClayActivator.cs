namespace DH.Clay;

public interface IClayActivator
{
    dynamic CreateInstance(Type baseType, IEnumerable<IClayBehavior> behaviors, IEnumerable<object> arguments);
}