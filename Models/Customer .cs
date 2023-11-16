using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Display(Name = "名稱")]
        [Required(ErrorMessage ="此欄位必填")]
        public string? Name { get; set; }

        public int Age { get; set; }

        public bool IsMale { get; set; }
        public string? Email { get; set; }
        public string? Remark { get; set; }

    }
}
