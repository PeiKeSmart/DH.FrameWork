using System.Reflection;

namespace DG.SafeOrbit.Memory.InjectionServices.Reflection
{
    public static class MethodInfoExtensions
    {
        /// <summary>
        ///     Gets the IL-code as bytes for the specified <see cref="MethodInfo" />.
        /// </summary>
        /// <param name="methodInfo">The method information of the IL-code.</param>
        /// <returns>IL-code as bytes</returns>
        /// <exception cref="ArgumentNullException"><paramref name="methodInfo" /> is <see langword="null" /></exception>
        public static byte[] GetIlBytes(this MethodInfo methodInfo)
        {
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            var methodBody = methodInfo.GetMethodBody();
            var result = methodBody?.GetILAsByteArray();
            return result;
        }
    }
}