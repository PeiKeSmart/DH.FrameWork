﻿using System.Text;

namespace DH.FileZilla.Api;

/// <summary>
/// Used during connect to FileZilla server.
/// </summary>
internal class Authentication : IBinarySerializable
{
    public bool NoPasswordRequired { get; set; }
    byte[] _nonce1;
    byte[] _nonce2;

    /// <summary>
    /// Deserialise FileZilla binary data into object
    /// </summary>
    /// <param name="reader">Binary reader to read data from</param>
    /// <param name="protocolVersion">Current FileZilla protocol version</param>
    /// <param name="index">The 0 based index of this item in relation to any parent list</param>
    public void Deserialize(BinaryReader reader, int protocolVersion, int index)
    {
        if (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            NoPasswordRequired = reader.ReadBoolean();
            reader.ReadLength(
                reader.ReadInt32(),
                r2 =>
                {
                    _nonce1 = r2.ReadArray(r3 => r3.ReadByte());
                    _nonce2 = r2.ReadArray(r3 => r3.ReadByte());
                    return true;
                });
        }
        else
            NoPasswordRequired = true;
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

    internal byte[] HashPassword(string password)
    {
        var cryptoServiceProvider = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] dataToHash = _nonce1.Concat(Encoding.UTF8.GetBytes(password)).Concat(_nonce2).ToArray();
        var hash = cryptoServiceProvider.ComputeHash(dataToHash);
        return hash;
    }
}