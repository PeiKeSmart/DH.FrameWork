﻿namespace DH.ServerSentEvents;

/// <summary>
/// Contract for store which keeps identifiers of <see cref="IServerSentEventsClient"/> which shouldn't be allowed to reconnect.
/// </summary>
public interface IServerSentEventsNoReconnectClientsIdsStore {
    /// <summary>
    /// Adds client identifier.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    Task AddClientIdAsync(Guid clientId);

    /// <summary>
    /// Determines whether the store contains client identifier.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>True if the store contains client identifier; otherwise, false.</returns>
    Task<bool> ContainsClientIdAsync(Guid clientId);

    /// <summary>
    /// Removes client identifier.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    Task RemoveClientIdAsync(Guid clientId);
}