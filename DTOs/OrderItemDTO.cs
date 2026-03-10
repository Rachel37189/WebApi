using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record OrderItemDTO
    (
        // int OrderItemId ,

         int ProductId ,
         //string ProductName ,

         int OrderId ,

         [Range(0.01, double.MaxValue, ErrorMessage = "הכמות חייבת להיות גדולה מאפס")]
         double? Quantity

       // Order Order ,
     
    );
}
