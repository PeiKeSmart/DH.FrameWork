using System.Diagnostics;
using System.Runtime;

using NewLife;

namespace DH.Systems;

/// <summary>
/// 提供系统级的帮助方法
/// </summary>
public class NativeHelper {
    /// <summary>释放内存。GC回收后再释放虚拟内存</summary>
    public void FreeMemory()
    {
        var max = GC.MaxGeneration;
        var mode = GCCollectionMode.Forced;
        //#if NET7_0_OR_GREATER
#if NET8_0_OR_GREATER
        mode = GCCollectionMode.Aggressive;
#endif
#if NET451_OR_GREATER || NETSTANDARD || NETCOREAPP
        GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
#endif
        GC.Collect(max, mode);
        GC.WaitForPendingFinalizers();
        GC.Collect(max, mode);

        if (Runtime.Windows)
        {
            var p = Process.GetCurrentProcess();
            NativeMethods.EmptyWorkingSet(p.Handle);
        }
    }
}
