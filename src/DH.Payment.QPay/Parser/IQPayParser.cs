﻿namespace DG.Payment.QPay.Parser
{
    public interface IQPayParser<T> where T : QPayObject
    {
        T Parse(string body);
    }
}
