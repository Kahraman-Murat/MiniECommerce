﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application.DTOs.User
{
    public class CreateUserResponse
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
    }
}