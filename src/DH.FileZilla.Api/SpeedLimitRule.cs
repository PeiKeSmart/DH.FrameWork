﻿namespace DH.FileZilla.Api;

/// <summary>
/// Class representing a FileZilla Speed limit rule
/// </summary>
public class SpeedLimitRule : IBinarySerializable
{
    /// <summary>
    /// Speed limit in kB/s
    /// </summary>
    public uint SpeedLimit { get; set; }
    /// <summary>
    /// Speed limit active on date
    /// </summary>
    public DateTime? Date { get; set; }
    /// <summary>
    /// Speed limit active from time
    /// </summary>
    public TimeSpan? FromTime { get; set; }
    /// <summary>
    /// Speed limit active to time
    /// </summary>
    public TimeSpan? ToTime { get; set; }
    /// <summary>
    /// Speed limit active on weekdays
    /// </summary>
    public Days Days { get; set; }

    /// <summary>
    /// Default constructor (sets defaults as in FileZilla server interface)
    /// </summary>
    public SpeedLimitRule()
    {
        SpeedLimit = 8;
        Days = Days.All;
    }

    /// <summary>
    /// Deserialise FileZilla binary data into object
    /// </summary>
    /// <param name="reader">Binary reader to read data from</param>
    /// <param name="protocolVersion">Current FileZilla protocol version</param>
    /// <param name="index">The 0 based index of this item in relation to any parent list</param>
    public void Deserialize(BinaryReader reader, int protocolVersion, int index)
    {
        SpeedLimit = reader.ReadBigEndianInt32();
        Date = reader.ReadDate();
        FromTime = reader.ReadTime();
        ToTime = reader.ReadTime();
        Days = (Days)reader.ReadByte();
    }

    /// <summary>
    /// Serialise object into FileZilla binary data
    /// </summary>
    /// <param name="writer">Binary writer to write data to</param>
    /// <param name="protocolVersion">Current FileZilla protocol version</param>
    /// <param name="index">The 0 based index of this item in relation to any parent list</param>
    public void Serialize(BinaryWriter writer, int protocolVersion, int index)
    {
        writer.WriteBigEndianInt32(SpeedLimit);
        writer.WriteDate(Date);
        writer.WriteTime(FromTime);
        writer.WriteTime(ToTime);
        writer.Write((byte)Days);
    }
}