using System;
using Models;

namespace Logic
{
    public class UserLogin : IUserLogin
    {
        /// <summary>
        ///     Given 
        /// </summary>
        /// <param name="???"></param>
        /// <returns></returns>
        public (bool, AuthUser) LoginUser(AuthUser user)
        {
            // TODO : Check to see if user exists
            throw new NotImplementedException();
        }
    }
}