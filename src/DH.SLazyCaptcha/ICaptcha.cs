﻿namespace DH.SLazyCaptcha;

public interface ICaptcha {
    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <param name="captchaId">验证码id</param>
    /// <param name="expirySeconds">缓存时间，未设定则使用配置时间</param>
    /// <returns></returns>
    CaptchaData Generate(string captchaId, int? expirySeconds = null);

    /// <summary>
    /// 校验
    /// </summary>
    /// <param name="captchaId">验证码id</param>
    /// <param name="code">用户输入的验证码</param>
    /// <param name="removeIfSuccess">校验成功时是否移除缓存(用于多次验证)</param>
    /// <param name="removeIfFail">校验失败时是否移除</param>
    /// <returns></returns>
    bool Validate(string captchaId, string code, bool removeIfSuccess = true, bool removeIfFail = true);
}