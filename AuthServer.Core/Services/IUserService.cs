﻿using AuthServer.Core.DTOs;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
   public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto userDto);
        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);


    }
}