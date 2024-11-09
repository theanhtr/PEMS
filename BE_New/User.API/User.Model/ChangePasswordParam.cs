namespace User.Model
{
    public class ChangePasswordParam
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
    }
}
