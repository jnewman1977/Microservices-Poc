namespace Nitro.Msvc.Tenant.Messaging.Abstraction.Model
{
    public class AddTenantResponse
    {
        public bool Success { get; set; }

        public string[] Errors { get; set; }
    }
}
