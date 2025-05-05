using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces {


    public interface IUserRepository
    {

        public Task<User> Register(UserDto user);

        public Task<string> Login(UserDto user);

    }
}