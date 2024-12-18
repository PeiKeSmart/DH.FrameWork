using System;
using System.Text;

namespace DG.SafeOrbit.Memory.Injection
{
    public sealed class InjectionMessage : IInjectionMessage
    {
        public InjectionMessage(bool isStateInjected, bool isCodeInjected, object injectedObject)
        {
            InjectionType = GetInjectionType(isStateInjected, isCodeInjected);
            InjectedObject = injectedObject;
            InjectionDetectionTime = DateTimeOffset.UtcNow;
        }

        public InjectionType InjectionType { get; }
        public object InjectedObject { get; }
        public DateTimeOffset InjectionDetectionTime { get; }

        public override string ToString()
        {
            return $"{nameof(InjectionType)} = \"{InjectionType}\", " +
                   $"{nameof(InjectionDetectionTime)} = \"{InjectionDetectionTime}\", " +
                   $"Injected object type = \"{InjectedObject?.GetType().Name ?? "null"}";
        }

        private static InjectionType GetInjectionType(bool isStateInjected, bool isCodeInjected)
        {
            if (isStateInjected && isCodeInjected) return InjectionType.CodeAndVariableInjection;
            if (isStateInjected) return InjectionType.VariableInjection;
            if (isCodeInjected) return InjectionType.CodeInjection;
            throw new ArgumentException("There is no injection : Neither state or code is injected.");
        }

        #region Equality
        public override int GetHashCode()
        {
            unchecked
            {
                return -590645308
                       * (int) InjectionType
                       * InjectionDetectionTime.GetHashCode()
                       * (InjectedObject?.GetHashCode() ?? 1);
            }
        }

        public override bool Equals(object obj) => Equals(obj as IInjectionMessage);
        public bool Equals(IInjectionMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            return InjectionType.Equals(other.InjectionType)
                   && (InjectedObject?.Equals(other.InjectedObject) ?? false)
                   && InjectionDetectionTime.Equals(other.InjectionDetectionTime);
        }

        public static bool operator ==(InjectionMessage a, InjectionMessage b) =>
            a?.Equals(b) ?? ReferenceEquals(null, b);

        public static bool operator !=(InjectionMessage a, InjectionMessage b) => !(a == b);

        #endregion
    }
}