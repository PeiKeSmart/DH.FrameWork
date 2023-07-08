using System.Collections.Generic;

namespace DG.SafeOrbit.Memory.SafeContainerServices.Instance.Validation
{
    internal interface IInstanceProviderRule
    {
        IEnumerable<string> Errors { get; }
        RuleType Type { get; }
        bool IsSatisfiedBy(IInstanceProvider instance);
    }
}