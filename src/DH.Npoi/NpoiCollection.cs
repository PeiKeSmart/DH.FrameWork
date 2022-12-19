﻿using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DH.Npoi
{
    /// <summary>
    ///     npoi sheet row collection
    /// </summary>
    public sealed class NpoiRowCollection : IReadOnlyCollection<IRow>
    {
        private readonly ISheet _sheet;

        public NpoiRowCollection(ISheet sheet) => _sheet = sheet ?? throw new ArgumentNullException(nameof(sheet));

        public int Count => _sheet.LastRowNum - _sheet.FirstRowNum + 1;

        public IEnumerator<IRow> GetEnumerator()
        {
            for (var i = _sheet.FirstRowNum; i <= _sheet.LastRowNum; i++)
            {
                yield return _sheet.GetRow(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    ///     npoi row cell collection
    /// </summary>
    public sealed class NpoiCellCollection : IReadOnlyCollection<ICell>
    {
        private readonly IRow _row;

        public NpoiCellCollection(IRow row) => _row = row;

        public int Count => _row.LastCellNum - _row.FirstCellNum;

        public IEnumerator<ICell> GetEnumerator()
        {
            for (var i = _row.FirstCellNum; i < _row.LastCellNum; i++)
            {
                yield return _row.GetCell(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
