﻿namespace DH.Clay.Behaviors;

public class NilResultBehavior : ClayBehavior
{

    public override object GetMember(Func<object> proceed, object self, string name)
    {
        return proceed() ?? Nil.Instance;
    }

    public override object GetIndex(Func<object> proceed, object self, IEnumerable<object> keys)
    {
        return proceed() ?? Nil.Instance;
    }

    public override object InvokeMember(Func<object> proceed, object self, string name, INamedEnumerable<object> args)
    {
        if (args.Any())
            return proceed();

        return proceed() ?? Nil.Instance;
    }
}