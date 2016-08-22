﻿using ChanLap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanLap.Web.Models
{
    public class UserViewModel : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsLocked { get; set; }
        public string Salt { get; set; }
    }
}