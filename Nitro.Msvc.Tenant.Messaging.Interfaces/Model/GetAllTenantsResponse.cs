namespace Nitro.Msvc.Tenant.Messaging.Interfaces.Model;

public class GetAllTenantsResponse
{
    public bool Success { get; set; }

    public string[] Errors { get; set; }

    public Entities.Tenant[] Tenants { get; set; }
}