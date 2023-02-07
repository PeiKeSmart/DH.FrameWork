﻿using System.Collections.Generic;
using DG.Payment.UnionPay.Response;

namespace DG.Payment.UnionPay.Request
{
    /// <summary>
    /// 二维码支付(V2.2) 文件传输类交易接口
    /// </summary>
    public class UnionPayQrCodePayFileTransferRequest : IUnionPayRequest<UnionPayQrCodePayFileTransferResponse>
    {
        /// <summary>
        /// 产品类型
        /// </summary>
        public string BizType { get; set; }

        /// <summary>
        /// 订单发送时间
        /// </summary>
        public string TxnTime { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string TxnType { get; set; }

        /// <summary>
        /// 交易子类
        /// </summary>
        public string TxnSubType { get; set; }

        /// <summary>
        /// 清算日期	
        /// </summary>
        public string SettleDate { get; set; }

        /// <summary>
        /// 文件类型	
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 接入机构代码	
        /// </summary>
        public string AccInsCode { get; set; }

        /// <summary>
        /// 请求方保留域
        /// </summary>
        public string ReqReserved { get; set; }

        #region IUnionPayRequest

        private string version = string.Empty;

        public string GetApiVersion()
        {
            return version;
        }

        public void SetApiVersion(string version)
        {
            this.version = version;
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new UnionPayDictionary
            {
                { "bizType", BizType },
                { "txnTime", TxnTime },
                { "txnType", TxnType },
                { "txnSubType", TxnSubType },
                { "settleDate", SettleDate },
                { "fileType", FileType },
                { "accInsCode", AccInsCode },
                { "reqReserved", ReqReserved }
            };
            return parameters;
        }

        public string GetRequestUrl(bool isTest)
        {
            return isTest ? "https://filedownload.test.95516.com/" : "https://filedownload.95516.com/";
        }

        public bool HasEncryptCertId()
        {
            return false;
        }

        #endregion
    }
}
