using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChanLap.Model
{
    public class User : BaseEntity
    {
        public User() { }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsLocked { get; set; }
        public string Salt { get; set; }
    }
}
