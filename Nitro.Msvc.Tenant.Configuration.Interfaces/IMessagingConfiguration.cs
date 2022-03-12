﻿namespace Nitro.Msvc.Tenant.Configuration.Interfaces;

public interface IMessagingConfiguration
{
    int? ConcurrentMessageLimit { get; }

    string Host { get; }

    string VirtualHost { get; }

    string ReceiveEndpoint { get; }
    
    string UserName { get; }

    string Password { get; }
}