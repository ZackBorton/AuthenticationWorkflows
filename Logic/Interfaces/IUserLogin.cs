using Models;

namespace Logic
{
    public interface IUserLogin
    {
        /// <summary>
        ///     Given 
        /// </summary>
        /// <param name="???"></param>
        /// <returns></returns>
        (bool, AuthUser) LoginUser(AuthUser user);
    }
}