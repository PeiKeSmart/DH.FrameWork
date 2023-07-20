﻿namespace DG.SafeOrbit.Library
{
    internal static class InternalsVisibleTo
    {
#if DEBUG
        public const string ToUnitTests = "UnitTests";
        public const string ToPerformanceTests = "PerformanceTests";
        public const string ToIntegrationTests = "IntegrationTests";
        public const string ToDynamicProxyGenAssembly2 = "DynamicProxyGenAssembly2";
#else
        public const string ToUnitTests =
 "UnitTests, PublicKey=00240000048000009400000006020000002400005253413100040000010001007f31988ffc56a37521b815e00113792fb0694934fc29891b88582e4f94761011a090fac4b58a235d0f2f9a8dfb160e4cefd4c6fd7591fed06f57d2bdbe205d7414cc852ed76c89f53af2e022310f4f960442e1059aae6af15d19f9ae446841ad751957e040dc8fac4a780b48c2b90e4cb0c368d7f196e4c3d1e214bfd79b57d3";
        public const string ToPerformanceTests =
 "PerformanceTests, PublicKey=00240000048000009400000006020000002400005253413100040000010001007f31988ffc56a37521b815e00113792fb0694934fc29891b88582e4f94761011a090fac4b58a235d0f2f9a8dfb160e4cefd4c6fd7591fed06f57d2bdbe205d7414cc852ed76c89f53af2e022310f4f960442e1059aae6af15d19f9ae446841ad751957e040dc8fac4a780b48c2b90e4cb0c368d7f196e4c3d1e214bfd79b57d3";
        public const string ToIntegrationTests =
 "IntegrationTests, PublicKey=00240000048000009400000006020000002400005253413100040000010001007f31988ffc56a37521b815e00113792fb0694934fc29891b88582e4f94761011a090fac4b58a235d0f2f9a8dfb160e4cefd4c6fd7591fed06f57d2bdbe205d7414cc852ed76c89f53af2e022310f4f960442e1059aae6af15d19f9ae446841ad751957e040dc8fac4a780b48c2b90e4cb0c368d7f196e4c3d1e214bfd79b57d3";

        /// <summary>
        ///     Constant to use when making assembly internals visible to proxy types generated by DynamicProxy. Required when
        ///     proxying internal types.
        /// </summary>
        public const string ToDynamicProxyGenAssembly2 =
            "DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7";
#endif
    }
}