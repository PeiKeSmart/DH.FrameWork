﻿using DH.MockData.Abstractions.Randomizers;
using DH.MockData.Core.Options;
using DH.MockData.Datas;
using DH.MockData.Datas.Models;
using DH.MockData.Extensions;
using DH.MockData.Internals.Generators;

namespace DH.MockData.Core.Randomizers;

/// <summary>
/// 银行账号随机生成器
/// </summary>
public class IBANRandomizer : RandomizerBase<IBANFieldOptions>, IStringRandomizer
{
    /// <summary>
    /// 项生成器
    /// </summary>
    private readonly RandomItemFromListGenerator<IBAN> _itemGenerator;

    /// <summary>
    /// 初始化一个<see cref="IBANRandomizer"/>类型的实例
    /// </summary>
    /// <param name="options">银行账号配置</param>
    public IBANRandomizer(IBANFieldOptions options) : base(options)
    {
        Func<IBAN, bool> predicate = null;
        if (!string.IsNullOrEmpty(options.CountryCode))
        {
            predicate = (iban) => iban.CountryCode == options.CountryCode;
        }

        var list = CommonData.Instance.IBANs;
        switch (options.Type)
        {
            case "BBAN":
                list = CommonData.Instance.BBANs;
                break;
            case "BOTH":
                list = list.Union(CommonData.Instance.BBANs);
                break;
        }

        _itemGenerator = new RandomItemFromListGenerator<IBAN>(list, predicate);
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <returns></returns>
    public string Generate()
    {
        if (IsNull())
        {
            return null;
        }

        var iban = _itemGenerator.Generate();
        return iban.Generator.Generate();
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="upperCase">是否大写</param>
    /// <returns></returns>
    public string Generate(bool upperCase)
    {
        return Generate().ToCasedInvariant(upperCase);
    }
}