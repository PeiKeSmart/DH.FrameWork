﻿using Microsoft.VisualBasic;

namespace DH.MockData.Datas.Models.Banks;

/// <summary>
/// 银行卡规则项
/// </summary>
public class CardRuleItem
{
    /// <summary>
    /// 银行卡前缀
    /// </summary>
    public string CardPrefix { get; set; }

    /// <summary>
    /// 长度
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// 银行卡类型
    /// </summary>
    public CardType Type { get; set; }
}