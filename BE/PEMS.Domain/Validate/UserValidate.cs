namespace PEMS.Domain
{
    public class UserValidate : BaseValidate<User>, IUserValidate
    {
        public UserValidate(IUserRepository userRepository) : base(userRepository)
        {
        }
    }
}
