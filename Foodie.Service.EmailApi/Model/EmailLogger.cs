using System.ComponentModel.DataAnnotations;

namespace Foodie.Service.EmailApi.Model
{
    public class EmailLogger
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public DateTime? EmailSent { get; set; }
    }
}
