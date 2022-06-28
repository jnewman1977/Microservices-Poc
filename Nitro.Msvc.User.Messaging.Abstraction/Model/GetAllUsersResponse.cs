namespace Nitro.Msvc.User.Messaging.Abstraction.Model;

public class GetAllUsersResponse
{
    public bool Success { get; set; }

    public string[] Errors { get; set; }

    public Entities.User[] Users { get; set; }
}