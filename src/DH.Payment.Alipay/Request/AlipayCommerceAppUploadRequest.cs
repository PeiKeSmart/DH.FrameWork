﻿using System.Collections.Generic;
using DH.Payment.Alipay.Domain;
using DH.Payment.Alipay.Response;
using DH.Payment.Alipay.Utility;

namespace DH.Payment.Alipay.Request
{
    /// <summary>
    /// alipay.commerce.app.upload
    /// </summary>
    public class AlipayCommerceAppUploadRequest : IAlipayUploadRequest<AlipayCommerceAppUploadResponse>
    {
        /// <summary>
        /// 租户应用服务数据
        /// </summary>
        public CommerceAppUploadRequestContent Content { get; set; }

        /// <summary>
        /// 上传文件的二进制流
        /// </summary>
        public FileItem File { get; set; }

        /// <summary>
        /// 租户应用服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 目标租户
        /// </summary>
        public string TargetId { get; set; }

        #region IAlipayRequest Members

        private bool needEncrypt = false;
        private string apiVersion = "1.0";
        private string terminalType;
        private string terminalInfo;
        private string prodCode;
        private string notifyUrl;
        private string returnUrl;
        private AlipayObject bizModel;
        private Dictionary<string, string> udfParams; //add user-defined text parameters

        public void SetNeedEncrypt(bool needEncrypt)
        {
            this.needEncrypt = needEncrypt;
        }

        public bool GetNeedEncrypt()
        {
            return needEncrypt;
        }

        public void SetNotifyUrl(string notifyUrl)
        {
            this.notifyUrl = notifyUrl;
        }

        public string GetNotifyUrl()
        {
            return notifyUrl;
        }

        public void SetReturnUrl(string returnUrl)
        {
            this.returnUrl = returnUrl;
        }

        public string GetReturnUrl()
        {
            return returnUrl;
        }

        public void SetTerminalType(string terminalType)
        {
            this.terminalType = terminalType;
        }

        public string GetTerminalType()
        {
            return terminalType;
        }

        public void SetTerminalInfo(string terminalInfo)
        {
            this.terminalInfo = terminalInfo;
        }

        public string GetTerminalInfo()
        {
            return terminalInfo;
        }

        public void SetProdCode(string prodCode)
        {
            this.prodCode = prodCode;
        }

        public string GetProdCode()
        {
            return prodCode;
        }

        public void SetApiVersion(string apiVersion)
        {
            this.apiVersion = apiVersion;
        }

        public string GetApiVersion()
        {
            return apiVersion;
        }

        public string GetApiName()
        {
            return "alipay.commerce.app.upload";
        }

        public void PutOtherTextParam(string key, string value)
        {
            if (udfParams == null)
            {
                udfParams = new Dictionary<string, string>();
            }
            udfParams.Add(key, value);
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new AlipayDictionary
            {
                { "content", Content },
                { "service_name", ServiceName },
                { "target_id", TargetId }
            };
            if (udfParams != null)
            {
                parameters.AddAll(udfParams);
            }
            return parameters;
        }

        public AlipayObject GetBizModel()
        {
            return bizModel;
        }

        public void SetBizModel(AlipayObject bizModel)
        {
            this.bizModel = bizModel;
        }

        #endregion

        #region IAlipayUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            var parameters = new Dictionary<string, FileItem>
            {
                { "file", File }
            };
            return parameters;
        }

        #endregion
    }
}
