using System.ComponentModel.DataAnnotations;
namespace WebApiShop
{
    public class User
    {
        [Required(ErrorMessage = "חובה להזין אימייל")]
        [EmailAddress(ErrorMessage = "כתובת אימייל לא חוקית")]
        public string userName { get; set; } = string.Empty;
        
        

        public string fName { get; set; } = string.Empty;


        public string lName { get; set; } = string.Empty;

        [Required(ErrorMessage = "חובה להזין סיסמה")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "הסיסמה חייבת להיות באורך של 8 עד 20 תווים")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&]).+$",
        ErrorMessage = "הסיסמה חייבת להכיל לפחות אות גדולה, אות קטנה, מספר ותו מיוחד")]
        public string passWord { get; set; } = string.Empty;
        public int id { get; set; }
    }
}
