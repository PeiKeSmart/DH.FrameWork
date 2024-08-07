﻿using Microsoft.AspNetCore.Connections;

using System.Security.Cryptography.X509Certificates;

namespace McMaster.AspNetCore.Kestrel.Certificates;

/// <summary>
/// Selects a certificate for incoming TLS connections.
/// </summary>
public interface IServerCertificateSelector {
    /// <summary>
    /// <para>
    /// A callback that will be invoked to dynamically select a server certificate.
    /// If SNI is not available, then the domainName parameter will be null.
    /// </para>
    /// <para>
    /// If the server certificate has an Extended Key Usage extension, the usages must include Server Authentication (OID 1.3.6.1.5.5.7.3.1).
    /// </para>
    /// </summary>
    X509Certificate2? Select(ConnectionContext context, string? domainName);
}