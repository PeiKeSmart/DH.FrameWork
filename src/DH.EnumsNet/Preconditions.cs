using System.Runtime.CompilerServices;

namespace EnumsNET;
internal static class Preconditions {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NotNull(object value, string paramName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(paramName);
        }
    }
}