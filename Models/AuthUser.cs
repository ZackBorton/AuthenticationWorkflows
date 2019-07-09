using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    ///     
    /// </summary>
    public class AuthUser
    {
        [Required] public string FirstName { get; set; }
        
        [Required] public string LastName { get; set; }
        
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
    }
}