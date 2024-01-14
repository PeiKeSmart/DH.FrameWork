using Aliyun.Acs.Core.Utils;

namespace DH.Sms.AliYun.Dysmsapi.Model.V20170525
{
    public class SendBatchSmsRequest : Aliyun.Acs.Core.RpcAcsRequest<SendBatchSmsResponse>
    {
        public SendBatchSmsRequest()
            : base("Dysmsapi", "2017-05-25", "SendBatchSms")
        {
        }

        private string templateCode;

        private string templateParamJson;

        private string accessKeyId;

        private string resourceOwnerAccount;

        private string action;

        private string smsUpExtendCodeJson;

        private long? resourceOwnerId;

        private string signNameJson;

        private long? ownerId;

        private string phoneNumberJson;

        public string TemplateCode
        {
            get { return templateCode; }
            set
            {
                templateCode = value;
                DictionaryUtil.Add(QueryParameters, "TemplateCode", value);
            }
        }

        public string TemplateParamJson
        {
            get { return templateParamJson; }
            set
            {
                templateParamJson = value;
                DictionaryUtil.Add(QueryParameters, "TemplateParamJson", value);
            }
        }

        public string AccessKeyId
        {
            get { return accessKeyId; }
            set
            {
                accessKeyId = value;
                DictionaryUtil.Add(QueryParameters, "AccessKeyId", value);
            }
        }

        public string ResourceOwnerAccount
        {
            get { return resourceOwnerAccount; }
            set
            {
                resourceOwnerAccount = value;
                DictionaryUtil.Add(QueryParameters, "ResourceOwnerAccount", value);
            }
        }

        public string Action
        {
            get { return action; }
            set
            {
                action = value;
                DictionaryUtil.Add(QueryParameters, "Action", value);
            }
        }

        public string SmsUpExtendCodeJson
        {
            get { return smsUpExtendCodeJson; }
            set
            {
                smsUpExtendCodeJson = value;
                DictionaryUtil.Add(QueryParameters, "SmsUpExtendCodeJson", value);
            }
        }

        public long? ResourceOwnerId
        {
            get { return resourceOwnerId; }
            set
            {
                resourceOwnerId = value;
                DictionaryUtil.Add(QueryParameters, "ResourceOwnerId", value.ToString());
            }
        }

        public string SignNameJson
        {
            get { return signNameJson; }
            set
            {
                signNameJson = value;
                DictionaryUtil.Add(QueryParameters, "SignNameJson", value);
            }
        }

        public long? OwnerId
        {
            get { return ownerId; }
            set
            {
                ownerId = value;
                DictionaryUtil.Add(QueryParameters, "OwnerId", value.ToString());
            }
        }

        public string PhoneNumberJson
        {
            get { return phoneNumberJson; }
            set
            {
                phoneNumberJson = value;
                DictionaryUtil.Add(QueryParameters, "PhoneNumberJson", value);
            }
        }

        public override SendBatchSmsResponse GetResponse(Aliyun.Acs.Core.Transform.UnmarshallerContext unmarshallerContext)
        {
            return SendBatchSmsResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
