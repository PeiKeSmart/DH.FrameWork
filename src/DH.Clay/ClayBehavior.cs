﻿using System.Linq.Expressions;

namespace DH.Clay;

public abstract class ClayBehavior : IClayBehavior
{
    public virtual object InvokeMember(Func<object> proceed, object self, string name, INamedEnumerable<object> args) { return proceed(); }
    public virtual object GetMember(Func<object> proceed, object self, string name) { return proceed(); }
    public virtual object SetMember(Func<object> proceed, object self, string name, object value) { return proceed(); }
    public virtual object GetIndex(Func<object> proceed, object self, IEnumerable<object> keys) { return proceed(); }
    public virtual object SetIndex(Func<object> proceed, object self, IEnumerable<object> keys, object value) { return proceed(); }
    public virtual object Convert(Func<object> proceed, object self, Type type, bool isExplicit) { return proceed(); }
    public virtual object BinaryOperation(Func<object> proceed, object self, ExpressionType operation, object value) { return proceed(); }
    public virtual object GetMembers(Func<object> proceed, object self, IDictionary<string, object> members) { return proceed(); }

    public virtual object InvokeMemberMissing(Func<object> proceed, object self, string name, INamedEnumerable<object> args) { return proceed(); }
    public virtual object GetMemberMissing(Func<object> proceed, object self, string name) { return proceed(); }
    public virtual object SetMemberMissing(Func<object> proceed, object self, string name, object value) { return proceed(); }
    public virtual object ConvertMissing(Func<object> proceed, object self, Type type, bool isExplicit) { return proceed(); }
}