﻿using System.Threading.Tasks;
using DH.Payment.UnionPay.Parser;
using DH.Payment.UnionPay.Utility;
using Microsoft.AspNetCore.Http;

namespace DH.Payment.UnionPay
{
    public class UnionPayNotifyClient : IUnionPayNotifyClient
    {
        #region UnionPayNotifyClient Constructors

        public UnionPayNotifyClient()
        {
        }

        #endregion

        #region IUnionPayNotifyClient Members

        public async Task<T> ExecuteAsync<T>(HttpRequest request) where T : UnionPayNotify
        {
            return await ExecuteAsync<T>(request, null);
        }

        public async Task<T> ExecuteAsync<T>(HttpRequest request, UnionPayOptions options) where T : UnionPayNotify
        {
            var parameters = await GetParametersAsync(request);
            var parser = new UnionPayDictionaryParser<T>();
            var rsp = parser.Parse(parameters);
            CheckNotifySign(parameters, options);
            return rsp;
        }

        #endregion

        #region Common Method

        private async Task<UnionPayDictionary> GetParametersAsync(HttpRequest request)
        {
            var parameters = new UnionPayDictionary();
            var form = await request.ReadFormAsync();
            foreach (var iter in form)
            {
                parameters.Add(iter.Key, iter.Value);
            }
            return parameters;
        }

        private void CheckNotifySign(UnionPayDictionary dictionary, UnionPayOptions options)
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                throw new UnionPayException("sign check fail: sign is Empty!");
            }

            var ifValidateCNName = !options.TestMode;
            if (!UnionPaySignature.Validate(dictionary, options.RootCertificate.cert, options.MiddleCertificate.cert, options.SecureKey, ifValidateCNName))
            {
                throw new UnionPayException("sign check fail: check Sign and Data Fail!");
            }
        }

        #endregion
    }
}