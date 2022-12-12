using NewLife;
using NewLife.Collections;

using SkiaSharp;

namespace DH.SkiaCaptcha;

public class VerifyCode
{
    private Random objRandom = new Random();

    #region 设置

    /// <summary>
    /// 验证码长度
    /// </summary>
    public Int32 SetLength { get; set; } = 4;

    /// <summary>
    /// 验证码字符串
    /// </summary>
    public String SetVerifyCodeText { get; set; }

    /// <summary>
    /// 是否加入小写字母
    /// </summary>
    public Boolean SetAddLowerLetter { get; set; }

    /// <summary>
    /// 是否加入大写字母
    /// </summary>
    public Boolean SetAddUpperLetter { get; set; }

    /// <summary>
    /// 字体大小
    /// </summary>
    public Int32 SetFontSize = 36;

    /// <summary>
    /// 字体颜色
    /// </summary>
    public SKColor SetFontColor { get; set; } = SKColors.Blue;

    /// <summary>
    /// 字体类型
    /// </summary>
    public String SetFontFamily { get; set; } = "Verdana";

    /// <summary>
    /// 背景色
    /// </summary>
    public SKColor SetBackgroundColor { get; set; } = SKColors.AliceBlue;

    /// <summary>
    /// 是否加入背景线
    /// </summary>
    public Boolean SetIsBackgroundLine { get; set; }

    /// <summary>
    /// 前景噪点数量
    /// </summary>
    public Int32 SetForeNoisePointCount { get; set; } = 2;

    /// <summary>
    /// 随机码的旋转角度
    /// </summary>
    public Int32 SetRandomAngle { get; set; } = 40;

    /// <summary>
    /// 是否随机字体颜色
    /// </summary>
    public Boolean SetIsRandomColor { get; set; } = true;

    /// <summary>
    /// 图片宽度
    /// </summary>
    public Int32 SetWidth { get; set; } = 200;

    /// <summary>
    /// 图片高度
    /// </summary>
    public Int32 SetHeight { get; set; } = 40;

    /// <summary>
    /// 问题验证码答案，适用于运算符
    /// </summary>
    public String VerifyCodeResult { get; private set; }

    #endregion

    #region 构造函数方法

    public VerifyCode()
    {
    }

    public VerifyCode(Int32 length = 4, Boolean isOperation = false)
    {
        if (isOperation)
        {
            var dic = GetQuestion();
            SetVerifyCodeText = dic.Key;
            VerifyCodeResult = dic.Value;
            SetRandomAngle = 0;
        }
        else
        {
            SetLength = length;
            GetVerifyCodeText();
        }
        SetWidth = SetVerifyCodeText.Length * SetFontSize;
        SetHeight = Convert.ToInt32((60.0 / 100) * SetFontSize + SetFontSize);

        InitColors();
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 得到验证码字符串
    /// </summary>
    private void GetVerifyCodeText()
    {
        // 没有外部输入验证码时随机生成
        if (SetVerifyCodeText.IsNullOrWhiteSpace())
        {
            var objStringBuilder = Pool.StringBuilder.Get();

            // 加入数字1-9
            for (var i = 1; i <= 9; i++)
            {
                objStringBuilder.Append(i);
            }

            // 加入大写字母A-Z，不包括O
            if (SetAddUpperLetter)
            {
                for (var i = 0; i < 26; i++)
                {
                    char temp = Convert.ToChar(i + 65);

                    // 如果生成的字母不是'O'
                    if (!temp.Equals('O'))
                    {
                        objStringBuilder.Append(temp);
                    }
                }
            }

            // 加入小写字母a-z，不包括o
            if (SetAddLowerLetter)
            {
                for (var i = 0; i < 26; i++)
                {
                    char temp = Convert.ToChar(i + 97);

                    // 如果生成的字母不是'o'
                    if (!temp.Equals('o'))
                    {
                        objStringBuilder.Append(temp);
                    }
                }
            }

            // 生成验证码字符串
            {
                for (var i = 0; i < SetLength; i++)
                {
                    int index = objRandom.Next(0, objStringBuilder.Length);
                    SetVerifyCodeText += objStringBuilder[index];

                    objStringBuilder.Remove(index, 1);
                }
            }

        }
    }

    /// <summary>
    /// 获得随机颜色
    /// </summary>
    /// <returns></returns>
    private SKColor GetRandomColor()
    {
        Random RandomNum_First = new Random((Int32)DateTime.Now.Ticks);
        // 对于C#的随机数，没什么好说的
        System.Threading.Thread.Sleep(RandomNum_First.Next(50));
        Random RandomNum_Sencond = new Random((Int32)DateTime.Now.Ticks);
        // 为了在白色背景上显示，尽量生成深色
        Int32 int_Red = RandomNum_First.Next(256);
        Int32 int_Green = RandomNum_Sencond.Next(256);
        Int32 int_Blue = (int_Red + int_Green) > 400 ? 0 : 400 - int_Red - int_Green;
        int_Blue = (int_Blue > 255) ? 255 : int_Blue;

        return SKColor.FromHsv(int_Red, int_Green, int_Blue);
    }

    #endregion

    #region 公有方法

    public void GetCodeText(Int32 length = 4, Boolean isOperation = false)
    {
        if (isOperation)
        {
            var dic = GetQuestion();
            SetVerifyCodeText = dic.Key;
            VerifyCodeResult = dic.Value;
            SetRandomAngle = 0;
        }
        else
        {
            SetLength = length;
            GetVerifyCodeText();
        }
        SetWidth = SetVerifyCodeText.Length * SetFontSize;
        SetHeight = Convert.ToInt32((60.0 / 100) * SetFontSize + SetFontSize);

        InitColors();
    }

    /// <summary>
    /// 获取问题
    /// </summary>
    /// <param name="questionList">默认数字加减验证</param>
    /// <returns></returns>
    public KeyValuePair<String, String> GetQuestion(Dictionary<String, String> questionList = null)
    {
        if (questionList == null)
        {
            questionList = new Dictionary<string, string>();
            var operArray = new String[] { "+", "*", "-", "/" };
            var left = objRandom.Next(0, 10);
            var right = objRandom.Next(0, 10);
            var oper = operArray[objRandom.Next(0, operArray.Length)];
            String key = String.Empty, val = String.Empty;
            switch (oper)
            {
                case "+":
                    key = String.Format("{0}+{1}=?", left, right);
                    val = (left + right).ToString();
                    questionList.Add(key, val);
                    break;

                case "*":
                    key = String.Format("{0}*{1}=?", left, right);
                    val = (left * right).ToString();
                    questionList.Add(key, val);
                    break;

                case "-":
                    if (left < right)
                    {
                        var intTemp = left;
                        left = right;
                        right = intTemp;
                    }
                    key = String.Format("{0}-{1}=?", left, right);
                    val = (left - right).ToString();
                    questionList.Add(key, val);
                    break;

                case "/":
                    left = right * objRandom.Next(0, 10);
                    key = String.Format("{0}*{1}=?", left, right);
                    val = (left / right).ToString();
                    questionList.Add(key, val);
                    break;
            }
        }
        return questionList.ToList()[objRandom.Next(0, questionList.Count)];
    }

    #endregion

    #region 画验证码

    /// <summary>
    /// 干扰线的颜色集合
    /// </summary>
    private List<SKColor> colors { get; set; }
    public void InitColors()
    {
        colors = new List<SKColor>();
        colors.Add(SKColors.AliceBlue);
        colors.Add(SKColors.PaleGreen);
        colors.Add(SKColors.PaleGoldenrod);
        colors.Add(SKColors.Orchid);
        colors.Add(SKColors.OrangeRed);
        colors.Add(SKColors.Orange);
        colors.Add(SKColors.OliveDrab);
        colors.Add(SKColors.Olive);
        colors.Add(SKColors.OldLace);
        colors.Add(SKColors.Navy);
        colors.Add(SKColors.NavajoWhite);
        colors.Add(SKColors.Moccasin);
        colors.Add(SKColors.MistyRose);
        colors.Add(SKColors.MintCream);
        colors.Add(SKColors.MidnightBlue);
        colors.Add(SKColors.MediumVioletRed);
        colors.Add(SKColors.MediumTurquoise);
        colors.Add(SKColors.MediumSpringGreen);
        colors.Add(SKColors.LightSlateGray);
        colors.Add(SKColors.LightSteelBlue);
        colors.Add(SKColors.LightYellow);
        colors.Add(SKColors.Lime);
        colors.Add(SKColors.LimeGreen);
        colors.Add(SKColors.Linen);
        colors.Add(SKColors.PaleTurquoise);
        colors.Add(SKColors.Magenta);
        colors.Add(SKColors.MediumAquamarine);
        colors.Add(SKColors.MediumBlue);
        colors.Add(SKColors.MediumOrchid);
        colors.Add(SKColors.MediumPurple);
        colors.Add(SKColors.MediumSeaGreen);
        colors.Add(SKColors.MediumSlateBlue);
        colors.Add(SKColors.Maroon);
        colors.Add(SKColors.PaleVioletRed);
        colors.Add(SKColors.PapayaWhip);
        colors.Add(SKColors.PeachPuff);
        colors.Add(SKColors.Snow);
        colors.Add(SKColors.SpringGreen);
        colors.Add(SKColors.SteelBlue);
        colors.Add(SKColors.Tan);
        colors.Add(SKColors.Teal);
        colors.Add(SKColors.Thistle);
        colors.Add(SKColors.SlateGray);
        colors.Add(SKColors.Tomato);
        colors.Add(SKColors.Violet);
        colors.Add(SKColors.Wheat);
        colors.Add(SKColors.White);
        colors.Add(SKColors.WhiteSmoke);
        colors.Add(SKColors.Yellow);
        colors.Add(SKColors.YellowGreen);
        colors.Add(SKColors.Turquoise);
        colors.Add(SKColors.LightSkyBlue);
        colors.Add(SKColors.SlateBlue);
        colors.Add(SKColors.Silver);
        colors.Add(SKColors.Peru);
        colors.Add(SKColors.Pink);
        colors.Add(SKColors.Plum);
        colors.Add(SKColors.PowderBlue);
        colors.Add(SKColors.Purple);
        colors.Add(SKColors.Red);
        colors.Add(SKColors.SkyBlue);
        colors.Add(SKColors.RosyBrown);
        colors.Add(SKColors.SaddleBrown);
        colors.Add(SKColors.Salmon);
        colors.Add(SKColors.SandyBrown);
        colors.Add(SKColors.SeaGreen);
        colors.Add(SKColors.SeaShell);
        colors.Add(SKColors.Sienna);
        colors.Add(SKColors.RoyalBlue);
        colors.Add(SKColors.LightSeaGreen);
        colors.Add(SKColors.LightSalmon);
        colors.Add(SKColors.LightPink);
        colors.Add(SKColors.Crimson);
        colors.Add(SKColors.Cyan);
        colors.Add(SKColors.DarkBlue);
        colors.Add(SKColors.DarkCyan);
        colors.Add(SKColors.DarkGoldenrod);
        colors.Add(SKColors.DarkGray);
        colors.Add(SKColors.Cornsilk);
        colors.Add(SKColors.DarkGreen);
        colors.Add(SKColors.DarkMagenta);
        colors.Add(SKColors.DarkOliveGreen);
        colors.Add(SKColors.DarkOrange);
        colors.Add(SKColors.DarkOrchid);
        colors.Add(SKColors.DarkRed);
        colors.Add(SKColors.DarkSalmon);
        colors.Add(SKColors.DarkKhaki);
        colors.Add(SKColors.DarkSeaGreen);
        colors.Add(SKColors.CornflowerBlue);
        colors.Add(SKColors.Chocolate);
        colors.Add(SKColors.AntiqueWhite);
        colors.Add(SKColors.Aqua);
        colors.Add(SKColors.Aquamarine);
        colors.Add(SKColors.Azure);
        colors.Add(SKColors.Beige);
        colors.Add(SKColors.Bisque);
        colors.Add(SKColors.Coral);
        colors.Add(SKColors.Black);
        colors.Add(SKColors.Blue);
        colors.Add(SKColors.BlueViolet);
        colors.Add(SKColors.Brown);
        colors.Add(SKColors.BurlyWood);
        colors.Add(SKColors.CadetBlue);
        colors.Add(SKColors.Chartreuse);
        colors.Add(SKColors.BlanchedAlmond);
        colors.Add(SKColors.Transparent);
        colors.Add(SKColors.DarkSlateBlue);
        colors.Add(SKColors.DarkTurquoise);
        colors.Add(SKColors.IndianRed);
        colors.Add(SKColors.Indigo);
        colors.Add(SKColors.Ivory);
        colors.Add(SKColors.Khaki);
        colors.Add(SKColors.Lavender);
        colors.Add(SKColors.LavenderBlush);
        colors.Add(SKColors.HotPink);
        colors.Add(SKColors.LawnGreen);
        colors.Add(SKColors.LightBlue);
        colors.Add(SKColors.LightCoral);
        colors.Add(SKColors.LightCyan);
        colors.Add(SKColors.LightGoldenrodYellow);
        colors.Add(SKColors.LightGray);
        colors.Add(SKColors.LightGreen);
        colors.Add(SKColors.LemonChiffon);
        colors.Add(SKColors.DarkSlateGray);
        colors.Add(SKColors.Honeydew);
        colors.Add(SKColors.Green);
        colors.Add(SKColors.DarkViolet);
        colors.Add(SKColors.DeepPink);
        colors.Add(SKColors.DeepSkyBlue);
        colors.Add(SKColors.DimGray);
        colors.Add(SKColors.DodgerBlue);
        colors.Add(SKColors.Firebrick);
        colors.Add(SKColors.GreenYellow);
        colors.Add(SKColors.FloralWhite);
        colors.Add(SKColors.Fuchsia);
        colors.Add(SKColors.Gainsboro);
        colors.Add(SKColors.GhostWhite);
        colors.Add(SKColors.Gold);
        colors.Add(SKColors.Goldenrod);
        colors.Add(SKColors.Gray);
        colors.Add(SKColors.ForestGreen);
    }

    /// <summary>
    /// 创建画笔
    /// </summary>
    /// <param name="color"></param>
    /// <param name="fontSize"></param>
    /// <returns></returns>
    private SKPaint CreatePaint(SKColor color, float fontSize)
    {
        SkiaSharp.SKTypeface font = SKTypeface.FromFamilyName(null, SKFontStyleWeight.SemiBold, SKFontStyleWidth.ExtraCondensed, SKFontStyleSlant.Upright);
        SKPaint paint = new SKPaint();

        paint.IsAntialias = true;
        paint.Color = color;
        paint.Typeface = font;
        paint.TextSize = fontSize;
        return paint;
    }

    /// <summary>
    /// 获取验证码
    /// </summary>
    /// <param name="lineNum">干扰线数量</param>
    /// <param name="lineStrookeWidth">干扰线宽度</param>
    /// <returns></returns>
    public Byte[] GetVerifyCodeImage(Int32 lineNum = 1, Int32 lineStrookeWidth = 1)
    {
        // 创建Bitmap位图
        using (SKBitmap image2d = new SKBitmap(SetWidth, SetHeight, SKColorType.Bgra8888, SKAlphaType.Premul))
        {
            // 创建画笔
            using (SKCanvas canvas = new SKCanvas(image2d))
            {
                if (SetIsRandomColor)
                {
                    SetFontColor = GetRandomColor();
                }

                // 填充白色背景
                canvas.Clear(SetBackgroundColor);

                AddForeNoisePoint(image2d);
                AddBackgroundNoisePoint(image2d, canvas);

                // 将文字写到画布上
                var drawStyle = new SKPaint();
                drawStyle.IsAntialias = true;
                drawStyle.TextSize = SetFontSize;
                char[] chars = SetVerifyCodeText.ToCharArray();

                for (Int32 i = 0; i < chars.Length; i++)
                {
                    var font = SKTypeface.FromFamilyName(SetFontFamily, SKFontStyleWeight.SemiBold, SKFontStyleWidth.ExtraCondensed, SKFontStyleSlant.Upright);

                    // 转动的度数
                    var angle = objRandom.Next(-30, 30);

                    canvas.Translate(12, 12);

                    float px = (i) * SetFontSize;
                    float py = SetHeight / 2;

                    canvas.RotateDegrees(angle, px, py);

                    drawStyle.Typeface = font;
                    drawStyle.Color = SetFontColor;

                    // 写字 (i + 1)* 16, 28
                    canvas.DrawText(chars[i].ToString(), px, py, drawStyle);

                    canvas.RotateDegrees(-angle, px, py);
                    canvas.Translate(-12, -12);
                }

                //画随机干扰线
                using (SKPaint disturbStyle = new SKPaint())
                {
                    Random random = new Random();
                    for (int i = 0; i < lineNum; i++)
                    {
                        disturbStyle.Color = colors[random.Next(colors.Count)];
                        disturbStyle.StrokeWidth = lineStrookeWidth;
                        canvas.DrawLine(random.Next(0, SetWidth), random.Next(0, SetHeight), random.Next(0, SetWidth), random.Next(0, SetHeight), disturbStyle);
                    }
                }

                //返回图片byte
                using (SKImage img = SKImage.FromBitmap(image2d))
                {
                    using (SKData p = img.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        return p.ToArray();
                    }
                }

            }
        }
    }

    /// <summary>
    /// 生成前景噪点
    /// </summary>
    /// <param name="objBitmap"></param>
    private void AddForeNoisePoint(SKBitmap objBitmap)
    {
        for (var i = 0; i < objBitmap.Width * SetForeNoisePointCount; i++)
        {
            objBitmap.SetPixel(objRandom.Next(objBitmap.Width), objRandom.Next(objBitmap.Height), SetFontColor);
        }
    }

    /// <summary>
    /// 背景干扰线
    /// </summary>
    /// <param name="objBitmap"></param>
    /// <param name="objGraphics"></param>
    private void AddBackgroundNoisePoint(SKBitmap objBitmap, SKCanvas objGraphics)
    {
        using (SKPaint objPen = CreatePaint(SKColors.Azure, 0))
        {
            for (var i = 0; i < objBitmap.Width * 2; i++)
            {
                objGraphics.DrawRect(objRandom.Next(objBitmap.Width), objRandom.Next(objBitmap.Height), 1, 1, objPen);
            }
        }
        if (SetIsBackgroundLine)
        {
            // 画图片的背景噪音线
            for (var i = 0; i < 12; i++)
            {
                var x1 = objRandom.Next(objBitmap.Width);
                var x2 = objRandom.Next(objBitmap.Width);
                var y1 = objRandom.Next(objBitmap.Height);
                var y2 = objRandom.Next(objBitmap.Height);

                objGraphics.DrawLine(x1, y1, x2, y2, CreatePaint(SKColors.Silver, 0));
            }
        }
    }

    #endregion
}
