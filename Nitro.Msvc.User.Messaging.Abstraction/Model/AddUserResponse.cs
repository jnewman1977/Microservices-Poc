namespace Nitro.Msvc.User.Messaging.Abstraction.Model
{
    public class AddUserResponse
    {
        public bool Success { get; set; }

        public string[] Errors { get; set; }
    }
}
