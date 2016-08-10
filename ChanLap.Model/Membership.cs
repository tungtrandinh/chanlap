using System.Security.Principal;

namespace ChanLap.Model
{
    public class Membership
    {
        public IPrincipal Principal { get; set; }
        public User User { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}
