using DH.Extensions;
using DH.Helpers;

using System.Linq.Expressions;

namespace DH.Expressions;

/// <summary>
/// 谓词表达式生成器
/// </summary>
public class PredicateExpressionBuilder<TEntity>
{
    /// <summary>
    /// 参数
    /// </summary>
    private readonly ParameterExpression _parameter;
    /// <summary>
    /// 结果表达式
    /// </summary>
    private Expression _result;

    /// <summary>
    /// 初始化谓词表达式生成器
    /// </summary>
    public PredicateExpressionBuilder()
    {
        _parameter = Lambda.CreateParameter<TEntity>();
    }

    /// <summary>
    /// 获取参数
    /// </summary>
    public ParameterExpression GetParameter()
    {
        return _parameter;
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        _result = null;
    }

    /// <summary>
    /// 转换为Lambda表达式
    /// </summary>
    public Expression<Func<TEntity, bool>> ToLambda()
    {
        return _result.ToLambda<Func<TEntity, bool>>(_parameter);
    }
}