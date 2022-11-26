using DH.Clay.Behaviors;

namespace DH.Clay;

public static class Nil
{
    static readonly object Singleton = new Clay(new NilBehavior(), new InterfaceProxyBehavior());
    public static object Instance { get { return Singleton; } }
}