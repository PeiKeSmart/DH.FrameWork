﻿using ACMESharp.Crypto;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ACMESharp.DotNetCore;

public static class EcToolExtensions {
    /// <summary>
    /// Returns a DER-encoded PKCS#10 Certificate Signing Request for the given ECDsa parametes
    /// and the given hash algorithm.
    /// </summary>
    public static byte[] GenerateCsr(this EcTool tool, IEnumerable<string> dnsNames,
        ECDsa dsa, HashAlgorithmName? hashAlgor = null)
    {
        if (hashAlgor == null)
            hashAlgor = HashAlgorithmName.SHA256;

        string firstName = null;
        var sanBuilder = new SubjectAlternativeNameBuilder();
        foreach (var n in dnsNames)
        {
            sanBuilder.AddDnsName(n);
            if (firstName == null)
                firstName = n;
        }
        if (firstName == null)
            throw new ArgumentException("Must specify at least one name");

        var dn = new X500DistinguishedName($"CN={firstName}");
        var csr = new CertificateRequest(dn, dsa, hashAlgor.Value);
        csr.CertificateExtensions.Add(sanBuilder.Build());

        return csr.CreateSigningRequest();
    }
}