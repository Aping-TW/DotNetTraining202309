using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Components
    {
        public int Id { get; set; }

        public int WP { get; set; }

        public string? PartName { get; set; }

        public int Quantity { get; set; }

        public string? SurfaceFinishing { get; set; }

        public DateTimeOffset ReceiveDate { get; set; }

        public DateTimeOffset DeliveryDate { get; set; }


    }
}
