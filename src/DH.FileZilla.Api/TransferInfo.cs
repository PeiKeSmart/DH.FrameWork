﻿namespace DH.FileZilla.Api;

/// <summary>
/// Server message informing about amount of data currently being transfered.
/// </summary>
public class TransferInfo : IBinarySerializable
{
    /// <summary>
    /// Type of event
    /// </summary>
    public TransferType Type { get; set; }
    /// <summary>
    /// Number of bytes transfered
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Deserialise FileZilla binary data into object
    /// </summary>
    /// <param name="reader">Binary reader to read data from</param>
    /// <param name="protocolVersion">Current FileZilla protocol version</param>
    /// <param name="index">The 0 based index of this item in relation to any parent list</param>
    public void Deserialize(BinaryReader reader, int protocolVersion, int index)
    {
        Type = (TransferType)reader.ReadByte();
        Count = reader.ReadInt32();
    }

    /// <summary>
    /// Serialise object into FileZilla binary data
    /// </summary>
    /// <param name="writer">Binary writer to write data to</param>
    /// <param name="protocolVersion">Current FileZilla protocol version</param>
    /// <param name="index">The 0 based index of this item in relation to any parent list</param>
    public void Serialize(BinaryWriter writer, int protocolVersion, int index)
    {
        throw new NotImplementedException();
    }
}