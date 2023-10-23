using ACMESharp.Crypto.JOSE;

using PKISharp.SimplePKI;

namespace DH.ACME.Common.PKI;

public class PkiJwsTool : IJwsTool {
    private PkiKeyPair _keys;

    private object _jwk;

    public PkiJwsTool(int bits)
    {
        Bits = bits;
    }

    public int Bits { get; }

    public string JwsAlg => $"ES{Bits}";

    public PkiKeyPair KeyPair => _keys;

    public void Init()
    {
        _jwk = null;
        _keys = PkiKeyPair.GenerateEcdsaKeyPair(Bits);
    }

    public void Dispose()
    {
        _keys = null;
        _jwk = null;
    }

    public string Export()
    {
        using (var ms = new MemoryStream())
        {
            _keys.Save(ms);
            return Convert.ToBase64String(ms.ToArray());
        }
    }

    public void Import(string exported)
    {
        _jwk = null;
        using (var ms = new MemoryStream(Convert.FromBase64String(exported)))
        {
            _keys = PkiKeyPair.Load(ms);
        }
    }

    public object ExportJwk(bool canonical = false)
    {
        // Note, we only produce a canonical form of the JWK
        // for export therefore we ignore the canonical param

        if (_jwk == null)
        {
            _jwk = _keys.ExportJwk();
        }

        return _jwk;
    }

    public byte[] Sign(byte[] raw)
    {
        return _keys.Sign(raw);
    }

    public bool Verify(byte[] raw, byte[] sig)
    {
        return _keys.Verify(raw, sig);
    }
}