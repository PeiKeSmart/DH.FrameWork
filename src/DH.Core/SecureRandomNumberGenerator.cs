using System.Security.Cryptography;

namespace DH.Core
{
    /// <summary>
    ///  表示加密随机数生成器派生的类实现
    /// </summary>
    public partial class SecureRandomNumberGenerator : RandomNumberGenerator
    {
        #region 字段

        private bool _disposed = false;
        private readonly RandomNumberGenerator _rng;

        #endregion

        #region 初始化

        public SecureRandomNumberGenerator()
        {
            _rng = Create();
        }

        #endregion

        #region 方法

        public int Next()
        {
            var data = new byte[sizeof(int)];
            _rng.GetBytes(data);
            return BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
        }

        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            return (int)Math.Floor(minValue + ((double)maxValue - minValue) * NextDouble());
        }

        public double NextDouble()
        {
            var data = new byte[sizeof(uint)];
            _rng.GetBytes(data);
            var randUint = BitConverter.ToUInt32(data, 0);
            return randUint / (uint.MaxValue + 1.0);
        }

        public override void GetBytes(byte[] data)
        {
            _rng.GetBytes(data);
        }

        public override void GetNonZeroBytes(byte[] data)
        {
            _rng.GetNonZeroBytes(data);
        }

        /// <summary>
        /// 随机处置安全
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Dispose模式的受保护实现。
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _rng?.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}
