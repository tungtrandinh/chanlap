using ChanLap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanLap.Web.Models
{
    public class UserRoleViewModel : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}