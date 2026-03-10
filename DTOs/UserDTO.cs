using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record UserDTO
 (

      [EmailAddress]
      [Required]
      string UserName,

      string FirstName,

      string LastName,

      string Password
 
    );
}
