﻿using DH.Core.Configuration;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;

using System.Data;

namespace DH.Core.Caching
{
    /// <summary>
    /// 表示MsSql服务器分布式缓存
    /// </summary>
    public class MsSqlServerCacheManager : DistributedCacheManager
    {
        #region Fields

        private readonly DistributedCacheConfig _distributedCacheConfig;

        #endregion

        #region Ctor

        public MsSqlServerCacheManager(AppSettings appSettings, IDistributedCache distributedCache) : base(appSettings,
            distributedCache)
        {
            _distributedCacheConfig = appSettings.Get<DistributedCacheConfig>();
        }

        #endregion

        #region Utilities

        protected async Task PerformActionAsync(SqlCommand command, params SqlParameter[] parameters)
        {
            var conn = new SqlConnection(_distributedCacheConfig.ConnectionString);
            try
            {
                conn.Open();
                command.Connection = conn;
                if (parameters.Any())
                    command.Parameters.AddRange(parameters);

                await command.ExecuteNonQueryAsync();
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Remove items by cache key prefix
        /// </summary>
        /// <param name="prefix">Cache key prefix</param>
        /// <param name="prefixParameters">Parameters to create cache key prefix</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task RemoveByPrefixAsync(string prefix, params object[] prefixParameters)
        {
            prefix = PrepareKeyPrefix(prefix, prefixParameters);

            var command =
                new SqlCommand(
                    $"DELETE FROM {_distributedCacheConfig.SchemaName}.{_distributedCacheConfig.TableName} WHERE Id LIKE @Prefix + '%'");

            await PerformActionAsync(command, new SqlParameter("Prefix", SqlDbType.NVarChar) { Value = prefix });

            await RemoveByPrefixInstanceDataAsync(prefix);
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task ClearAsync()
        {
            var command =
                new SqlCommand(
                    $"TRUNCATE TABLE {_distributedCacheConfig.SchemaName}.{_distributedCacheConfig.TableName}");

            await PerformActionAsync(command);

            ClearInstanceData();
        }

        #endregion
    }
}
