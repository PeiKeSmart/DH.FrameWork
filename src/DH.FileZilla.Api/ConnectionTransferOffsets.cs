﻿namespace DH.FileZilla.Api;

/// <summary>
/// A connection has been added
/// </summary>
public class ConnectionTransferOffsets : IBinarySerializable
{
    /// <summary>
    /// Connection Id
    /// </summary>
    public int ConnectionId { get; set; }
    /// <summary>
    /// Connection IP
    /// </summary>
    public long Offset { get; set; }

    /// <summary>
    /// Deserialise FileZilla binary data into object
    /// </summary>
    /// <param name="reader">Binary reader to read data from</param>
    /// <param name="protocolVersion">Current FileZilla protocol version</param>
    /// <param name="index">The 0 based index of this item in relation to any parent list</param>
    public void Deserialize(BinaryReader reader, int protocolVersion, int index)
    {
        ConnectionId = reader.ReadInt32();
        Offset = reader.ReadInt64();
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