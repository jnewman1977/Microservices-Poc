namespace Nitro.Msvc.User.Messaging.Abstraction.Model;

public class GetUserByUserIdResponse
{
    public bool Success { get; set; }

    public string[] Errors { get; set; }

    public Entities.User? User { get; set; }
}