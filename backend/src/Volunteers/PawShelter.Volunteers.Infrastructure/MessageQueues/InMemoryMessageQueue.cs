﻿using System.Threading.Channels;
using PawShelter.Core.Messaging;

namespace PawShelter.Volunteers.Infrastructure.MessageQueues;

public class InMemoryMessageQueue<TMessage> : IMessageQueue<TMessage>
{
    private readonly Channel<TMessage> _channel = Channel.CreateUnbounded<TMessage>();

    public async Task WriteAsync(TMessage message, CancellationToken ct)
    {
        await _channel.Writer.WriteAsync(message, ct);
    }

    public async Task<TMessage> ReadAsync(CancellationToken ct)
    {
        return await _channel.Reader.ReadAsync(ct);
    }
}