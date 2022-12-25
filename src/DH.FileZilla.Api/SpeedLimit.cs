﻿namespace DH.FileZilla.Api;

/// <summary>
/// Object representing a download or upload speed limit
/// </summary>
public class SpeedLimit : IBinarySerializable
{
    /// <summary>
    /// OptionType of speed limit
    /// </summary>
    public SpeedLimitType SpeedLimitType { get; set; }
    /// <summary>
    /// Does this limit bypasseses server limits.
    /// </summary>
    public TriState BypassServerSpeedLimit { get; set; }
    /// <summary>
    /// Constant speed limit in kB/s (requires SpeedLimitType.ConstantSpeedLimit to be operational)
    /// </summary>
    public ushort ConstantSpeedLimit { get; set; }
    /// <summary>
    /// List of speed limits (requires SpeedLimitType.UseSpeedLimitRules to be operational) 
    /// </summary>
    public List<SpeedLimitRule> SpeedLimitRules { get; set; }

    /// <summary>
    /// Default constructor (sets defaults as in FileZilla server interface)
    /// </summary>
    public SpeedLimit()
    {
        SpeedLimitType = SpeedLimitType.Default;
        BypassServerSpeedLimit = TriState.Default;
        ConstantSpeedLimit = 10;
        SpeedLimitRules = new List<SpeedLimitRule>();
    }

    /// <summary>
    /// Default constructor (sets defaults as in FileZilla server interface)
    /// </summary>
    /// <param name="isGroup">True if owning object is a group (different defaults)</param>
    public SpeedLimit(bool isGroup)
        : this()
    {
        if (isGroup)
        {
            SpeedLimitType = SpeedLimitType.NoLimit;
            BypassServerSpeedLimit = TriState.No;
        }
    }

    /// <summary>
    /// Deserialise FileZilla binary data into object
    /// </summary>
    /// <param name="reader">Binary reader to read data from</param>
    /// <param name="protocolVersion">Current FileZilla protocol version</param>
    /// <param name="index">The 0 based index of this item in relation to any parent list</param>
    public void Deserialize(BinaryReader reader, int protocolVersion, int index)
    {
        var options = reader.ReadByte();
        SpeedLimitType = (SpeedLimitType)(options & 0x3);
        BypassServerSpeedLimit = (TriState)(options >> 2);
        ConstantSpeedLimit = reader.ReadBigEndianInt16();
        SpeedLimitRules = reader.ReadList16<SpeedLimitRule>(protocolVersion);
    }

    /// <summary>
    /// Serialise object into FileZilla binary data
    /// </summary>
    /// <param name="writer">Binary writer to write data to</param>
    /// <param name="protocolVersion">Current FileZilla protocol version</param>
    /// <param name="index">The 0 based index of this item in relation to any parent list</param>
    public void Serialize(BinaryWriter writer, int protocolVersion, int index)
    {
        var options = (byte)(((byte)SpeedLimitType & 0x3) | ((byte)BypassServerSpeedLimit << 2));
        writer.Write(options);
        writer.WriteBigEndianInt16(ConstantSpeedLimit);
        writer.WriteList16(SpeedLimitRules, protocolVersion);
    }
}