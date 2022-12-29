using DH.Extensions;

using NewLife;
using NewLife.Collections;

namespace DH.IdGenerators.MongoId;

/// <summary>
/// 主要实现了生产、解包、计算、转换、公开数据结构等操作
/// </summary>
public class MObjectId
{
    public Byte[] Hex { get; private set; }
    public Int32 Timestamp { get; private set; }
    public Int32 Machine { get; private set; }
    public Int32 ProcessId { get; private set; }
    public Int32 Increment { get; private set; }

    public Boolean Equals(MObjectId other) => CompareTo(other) == 0;

    public static bool operator <(MObjectId a, MObjectId b) => a.CompareTo(b) < 0;
    public static bool operator <=(MObjectId a, MObjectId b) => a.CompareTo(b) <= 0;
    public static bool operator ==(MObjectId a, MObjectId b) => a.Equals(b);
    public static bool operator !=(MObjectId a, MObjectId b) => !(a == b);
    public static bool operator >=(MObjectId a, MObjectId b) => a.CompareTo(b) >= 0;
    public static bool operator >(MObjectId a, MObjectId b) => a.CompareTo(b) > 0;

    public static implicit operator string(MObjectId objectId) => objectId.ToString();
    public static implicit operator MObjectId(string objectId) => new MObjectId(objectId);

    private readonly static ObjectIdFactory factory = new ObjectIdFactory();

    public static MObjectId NewId() => factory.NewId();
    public static MObjectId Empty { get { return new MObjectId("000000000000000000000000"); } }

    public MObjectId(Byte[] hexData)
    {
        Hex = hexData;
        ReverseHex();
    }

    public MObjectId(String value)
    {
        if (value.IsNullOrEmpty()) throw new ArgumentNullException("value");
        if (value.Length != 24) throw new ArgumentNullException("value should be 24 characters");

        Hex = new Byte[12];
        for (var i = 0; i < value.Length; i += 2)
        {
            try
            {
                Hex[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            }
            catch
            {
                Hex[i / 2] = 0;
            }
        }
        ReverseHex();
    }

    private void ReverseHex()
    {
        var copyIdx = 0;
        var time = new Byte[4];
        Array.Copy(Hex, copyIdx, time, 0, 4);
        Array.Reverse(time);
        Timestamp = BitConverter.ToInt32(time, 0);
        copyIdx += 4;

        var mid = new Byte[4];
        Array.Copy(Hex, copyIdx, mid, 0, 3);
        Machine = BitConverter.ToInt32(mid, 0);
        copyIdx += 3;

        var pids = new Byte[4];
        Array.Copy(Hex, copyIdx, pids, 0, 2);
        Array.Reverse(pids);
        ProcessId = BitConverter.ToInt32(pids, 0);
        copyIdx += 2;

        var inc = new Byte[4];
        Array.Copy(Hex, copyIdx, inc, 0, 3);
        Array.Reverse(inc);
        Increment = BitConverter.ToInt32(inc, 0);
    }

    public Int32 CompareTo(MObjectId other)
    {
        if (other is null)
            return 1;

        for (Int32 i = 0; i < Hex.Length; i++)
        {
            if (Hex[i] < other.Hex[i])
                return -1;
            else if (Hex[i] > other.Hex[i])
                return 1;
        }

        return 0;
    }

    public override string ToString()
    {
        if (Hex == null)
        {
            Hex = new Byte[12];
        }

        var hexText = Pool.StringBuilder.Get();
        for (var i = 0; i < Hex.Length; i++)
        {
            hexText.Append(Hex[i].ToString("x2"));
        }
        return hexText.Put(true);
    }

    public override int GetHashCode() => ToString().GetHashCode();

    public override bool Equals(object obj) => base.Equals(obj);
}