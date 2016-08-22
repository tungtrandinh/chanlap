using ChanLap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanLap.Web.Models
{
    public class RoleViewModel : BaseEntity
    {
        public string Name { get; set; }
        public string Summarize { get; set; }
    }
}