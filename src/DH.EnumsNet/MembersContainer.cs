using EnumsNET.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EnumsNET;
internal sealed class MembersContainer<TEnum> : IReadOnlyList<EnumMember<TEnum>>
       where TEnum : struct, Enum {
    private readonly IEnumerable<EnumMemberInternal> _members;
    private EnumMember<TEnum>[] _membersArray;

    public int Count { get; }

    public EnumMember<TEnum> this[int index] => (_membersArray ??= ArrayHelper.ToArray(this, Count))[index];

    public MembersContainer(IEnumerable<EnumMemberInternal> members, int count, bool cached)
    {
        Debug.Assert(count == members.Count());
        _members = members;
        Count = count;
        if (cached)
        {
            _membersArray = ArrayHelper.ToArray(this, count);
        }
    }

    public IEnumerator<EnumMember<TEnum>> GetEnumerator() => _membersArray != null ? ((IEnumerable<EnumMember<TEnum>>)_membersArray).GetEnumerator() : Enumerate();

    private IEnumerator<EnumMember<TEnum>> Enumerate()
    {
        foreach (var member in _members)
        {
            yield return UnsafeUtility.As<EnumMember<TEnum>>(member.EnumMember);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}