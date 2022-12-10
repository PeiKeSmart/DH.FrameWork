﻿using DH.LazyCaptcha.Storage;

using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace DH.LazyCaptcha
{
    /// <summary>
    /// 验证服务构建器
    /// </summary>
    public class CaptchaServiceBuilder
    {
        private CaptchaOptions CaptchaOptions;
        private IStorage InnerStorage;

        public CaptchaServiceBuilder()
        {
            CaptchaOptions = GenerateDefaultOptions();
            InnerStorage = new MemeoryStorage();
        }

        public static CaptchaServiceBuilder New()
        {
            return new CaptchaServiceBuilder();
        }

        /// <summary>
        /// 默认选项
        /// </summary>
        /// <returns></returns>
        private CaptchaOptions GenerateDefaultOptions()
        {
            var options = new CaptchaOptions();
            options.ImageOption.Width = 98;
            options.ImageOption.Height = 35;
            options.ImageOption.ForegroundColors = DefaultColors.Instance.Colors;
            return options;
        }

        /// <summary>
        /// 设定存储
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder Storage(IStorage storage)
        {
            InnerStorage = storage;
            return this;
        }

        /// <summary>
        /// 验证码类型
        /// </summary>
        /// <param name="captchaType"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder CaptchaType(CaptchaType captchaType)
        {
            CaptchaOptions.CaptchaType = captchaType;
            return this;
        }

        /// <summary>
        /// 验证码长度 当CaptchaType为ARITHMETIC, ARITHMETIC_ZH时， 长度代表乘数个数
        /// </summary>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder CodeLength(int codeLength)
        {
            CaptchaOptions.CodeLength = codeLength;
            return this;
        }

        /// <summary>
        /// 过期时长
        /// </summary>
        /// <param name="expirySeconds"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder ExpirySeconds(int expirySeconds)
        {
            CaptchaOptions.ExpirySeconds = expirySeconds;
            return this;
        }

        /// <summary>
        /// code比较是否忽略大小写
        /// </summary>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder CodeLength(bool ignoreCase)
        {
            CaptchaOptions.IgnoreCase = ignoreCase;
            return this;
        }

        /// <summary>
        /// 存储键前缀
        /// </summary>
        /// <param name="storeageKeyPrefix"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder StoreageKeyPrefix(string storeageKeyPrefix)
        {
            CaptchaOptions.StoreageKeyPrefix = storeageKeyPrefix;
            return this;
        }

        /// <summary>
        /// 是否启用动画
        /// </summary>
        /// <param name="animation"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder Animation(bool animation)
        {
            CaptchaOptions.ImageOption.Animation = animation;
            return this;
        }

        /// <summary>
        /// 背景色
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder BackgroundColor(Color backgroundColor)
        {
            CaptchaOptions.ImageOption.BackgroundColor = backgroundColor;
            return this;
        }

        /// <summary>
        /// 前景色
        /// </summary>
        /// <param name="foregroundColors"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder ForegroundColors(List<Color> foregroundColors)
        {
            CaptchaOptions.ImageOption.ForegroundColors = foregroundColors;
            return this;
        }

        /// <summary>
        /// FontFamily
        /// </summary>
        /// <param name="fontFamily"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder FontFamily(FontFamily fontFamily)
        {
            CaptchaOptions.ImageOption.FontFamily = fontFamily;
            return this;
        }

        /// <summary>
        /// FontStyle
        /// </summary>
        /// <param name="fontStyle"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder FontStyle(FontStyle fontStyle)
        {
            CaptchaOptions.ImageOption.FontStyle = fontStyle;
            return this;
        }

        /// <summary>
        /// 字体大小
        /// </summary>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder FontSize(float fontSize)
        {
            CaptchaOptions.ImageOption.FontSize = fontSize;
            return this;
        }

        /// <summary>
        /// 验证码的宽
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder Width(int width)
        {
            CaptchaOptions.ImageOption.Width = width;
            return this;
        }

        /// <summary>
        /// 验证码高
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder Height(int height)
        {
            CaptchaOptions.ImageOption.Height = height;
            return this;
        }

        /// <summary>
        /// 气泡最小半径
        /// </summary>
        /// <param name="bubbleMinRadius"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder BubbleMinRadius(int bubbleMinRadius)
        {
            CaptchaOptions.ImageOption.BubbleMinRadius = bubbleMinRadius;
            return this;
        }

        /// <summary>
        /// 气泡最小半径
        /// </summary>
        /// <param name="bubbleMaxRadius"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder BubbleMaxRadius(int bubbleMaxRadius)
        {
            CaptchaOptions.ImageOption.BubbleMaxRadius = bubbleMaxRadius;
            return this;
        }

        /// <summary>
        /// 气泡数量
        /// </summary>
        /// <param name="bubbleCount"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder BubbleCount(int bubbleCount)
        {
            CaptchaOptions.ImageOption.BubbleCount = bubbleCount;
            return this;
        }

        /// <summary>
        /// 气泡边沿厚度
        /// </summary>
        /// <param name="bubbleThickness"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder BubbleThickness(float bubbleThickness)
        {
            CaptchaOptions.ImageOption.BubbleThickness = bubbleThickness;
            return this;
        }

        /// <summary>
        /// 干扰线数量
        /// </summary>
        /// <param name="interferenceLineCount"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder InterferenceLineCount(int interferenceLineCount)
        {
            CaptchaOptions.ImageOption.InterferenceLineCount = interferenceLineCount;
            return this;
        }

        /// <summary>
        /// 每帧延迟,Animation=true时有效
        /// </summary>
        /// <param name="frameDelay"></param>
        /// <returns></returns>
        public CaptchaServiceBuilder FrameDelay(int frameDelay)
        {
            CaptchaOptions.ImageOption.FrameDelay = frameDelay;
            return this;
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns></returns>
        public CaptchaService Build()
        {
            return new CaptchaService(CaptchaOptions, InnerStorage);
        }
    }
}
