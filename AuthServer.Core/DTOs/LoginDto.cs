﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Core.DTOs
{
   public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
