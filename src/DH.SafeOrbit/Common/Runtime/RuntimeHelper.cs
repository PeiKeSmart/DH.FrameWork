using System.Runtime.CompilerServices;

namespace DG.SafeOrbit.Helpers
{
    public static class RuntimeHelper
    {
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="action" /> or <paramref name="cleanup" /> is
        ///     <see langword="null" />
        /// </exception>
        public static void ExecuteCodeWithGuaranteedCleanup(Action action, Action cleanup)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (cleanup == null) throw new ArgumentNullException(nameof(cleanup));

            //RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(
            //delegate
            //    {
            //        action.Invoke();
            //    }
            //    ,
            //delegate
            //{
            //    cleanup.Invoke();
            //},
            //    null);
            try
            {
                action.Invoke();
            }
            finally
            {
                cleanup.Invoke();
            }
        }

        /// <summary>
        ///     Create a CER (Constrained Execution Region)
        /// </summary>
        public static void PrepareConstrainedRegions()
        {
            RuntimeHelpers.PrepareConstrainedRegions();
        }
    }
}