using EnumsNET.Utilities;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EnumsNET;
internal sealed class NamesContainer : IReadOnlyList<string> {
    private readonly IEnumerable<EnumMemberInternal> _members;
    private string[]? _namesArray;

    public int Count { get; }

    public string this[int index] => (_namesArray ??= ArrayHelper.ToArray(this, Count))[index];

    public NamesContainer(IEnumerable<EnumMemberInternal> members, int count, bool cached)
    {
        Debug.Assert(count == members.Count());
        _members = members;
        Count = count;
        if (cached)
        {
            _namesArray = ArrayHelper.ToArray(this, count);
        }
    }

    public IEnumerator<string> GetEnumerator() => _namesArray != null ? ((IEnumerable<string>)_namesArray).GetEnumerator() : Enumerate();

    private IEnumerator<string> Enumerate()
    {
        foreach (var member in _members)
        {
            yield return member.Name;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}