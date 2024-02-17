using System.Collections.ObjectModel;
using System.Text;

namespace DH.SLazyCaptcha.Generator.Code;

public class DefaultCaptchaCodeGenerator : ICaptchaCodeGenerator {
    private readonly CaptchaType _captchaType = CaptchaType.DEFAULT;

    private static IReadOnlyList<char> GetCharacters(CaptchaType captchaType)
    {
        return captchaType switch
        {
            CaptchaType.DEFAULT => Characters.DEFAULT,
            CaptchaType.CHINESE => Characters.CHINESE,
            CaptchaType.NUMBER => Characters.NUMBER,
            CaptchaType.NUMBER_ZH_CN => Characters.NUMBER_ZH_CN,
            CaptchaType.NUMBER_ZH_HK => Characters.NUMBER_ZH_HK,
            CaptchaType.WORD_NUMBER_LOWER => Characters.WORD_NUMBER_LOWER,
            CaptchaType.WORD_NUMBER_UPPER => Characters.WORD_NUMBER_UPPER,
            CaptchaType.WORD => Characters.WORD,
            CaptchaType.WORD_LOWER => Characters.WORD_LOWER,
            CaptchaType.WORD_UPPER => Characters.WORD_UPPER,

            _ => Characters.DEFAULT,
        };
    }

    private static readonly ThreadLocal<Random> ThreadRandom = new(() => new Random());
    private static Random random => ThreadRandom.Value;

    /// <summary>
    /// 中文操作符
    /// </summary>
    private static IReadOnlyDictionary<char, char> OPERATOR_MAP { get; } = new ReadOnlyDictionary<char, char>(new Dictionary<char, char>()
        {
             { '+', '加' },  { '-', '减' }
        });


    public DefaultCaptchaCodeGenerator() : this(CaptchaType.DEFAULT)
    {

    }

    public DefaultCaptchaCodeGenerator(CaptchaType captchaType)
    {
        _captchaType = captchaType;
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="length">长度</param>
    /// <returns>（渲染文本，code）</returns>
    public (string renderText, string code) Generate(int length)
    {
        if (_captchaType == CaptchaType.ARITHMETIC)
        {
            return GenerateaArithmetic(length);
        }
        else if (_captchaType == CaptchaType.ARITHMETIC_ZH)
        {
            return GenerateaArithmeticZh(length);
        }
        else if (_captchaType == CaptchaType.NUMBER_ZH_CN)
        {
            return GenerateaNumberZH(length, false);
        }
        else if (_captchaType == CaptchaType.NUMBER_ZH_HK)
        {
            return GenerateaNumberZH(length, true);
        }
        else
        {
            var chars = GetCharacters(_captchaType);
            var code = Pick(chars, length);
            return (code, code);
        }
    }

    private (string renderText, string code) GenerateaNumberZH(int length, bool isHk)
    {
        var sb1 = new StringBuilder();
        var sb2 = new StringBuilder();
        var characters = isHk ? Characters.NUMBER_ZH_HK : Characters.NUMBER_ZH_CN;

        for (var i = 0; i < length; i++)
        {
            var num = random.Next(characters.Count);
            sb1.Append(characters[num]);
            sb2.Append(Characters.NUMBER[num]);
        }

        return (sb1.ToString(), sb2.ToString());
    }

    /// <summary>
    /// 生成算术表达式组成部分
    /// </summary>
    /// <param name="length">数字位数</param>
    /// <returns></returns>
    private (int number1, char operators, int number2, int result) GenerateaArithmeticParts(int length)
    {
        var max = length switch
        {
            1 => 10,
            2 => 100,
            3 => 1000,
            4 => 10000,
            5 => 100000,
            6 => 1000000,
            7 => 10000000,
            8 => 100000000,
            9 => 1000000000,
            _ => throw new ArgumentOutOfRangeException(nameof(length), "must 1 to 9")
        };

        var result = random.Next(max);
        var number1 = random.Next(max);

        if (number1 > result)
            return (number1, '-', number1 - result, result);
        else
            return (number1, '+', result - number1, result);
    }

    /// <summary>
    /// 生成阿拉伯算术表达式
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    private (string renderText, string code) GenerateaArithmetic(int length)
    {
        var (number1, operators, number2, result) = GenerateaArithmeticParts(length);

        // 生成表达式
        var sb = new StringBuilder();

        sb.Append(number1).Append(operators).Append(number2); //.Append("=?"); // 显示字符数量问题，不增加最后问号

        return (sb.ToString(), result.ToString());
    }

    /// <summary>
    /// 生成汉字算术表达式
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    private (string renderText, string code) GenerateaArithmeticZh(int length)
    {
        var (renderText, code) = GenerateaArithmetic(length);
        var sb = new StringBuilder(renderText.Length);
        foreach (var item in renderText)
        {
            if (item is >= '0' and <= '9')
                sb.Append(Characters.NUMBER_ZH_CN[item - '0']);
            else if (item is '+' or '-')
                sb.Append(OPERATOR_MAP[item]);
            else
                sb.Append(item);
        }

        return (sb.ToString(), code);
    }

    /// <summary>
    /// 随机挑选字符
    /// </summary>
    /// <param name="characters">字符列表</param>
    /// <param name="count">数量</param>
    /// <returns></returns>
    private string Pick(IReadOnlyList<char> characters, int count)
    {
        var result = new StringBuilder();

        for (var i = 0; i < count; i++)
        {
            result.Append(characters[random.Next(characters.Count)]);
        }

        return result.ToString();
    }
}