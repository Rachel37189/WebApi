using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User
    {
        [Required(ErrorMessage = "חובה להזין אימייל")]
        [EmailAddress(ErrorMessage = "כתובת אימייל לא חוקית")]
        public string UserName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        
        public int Id { get; set; }
    }
}
