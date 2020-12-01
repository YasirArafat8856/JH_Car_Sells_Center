using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using JH_Car_Center.ViewModel;

namespace JH_Car_Center.Models
{
    public class DbModel: DbContext
    {
        public DbModel(DbContextOptions<DbModel> options) : base(options) { }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<JH_Car_Center.ViewModel.CenterVM> CenterVM { get; set; }
    }

    public class Offer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }
        public string TypeOfVehicle { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNO { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int ManufactureYear { get; set; }
        public int CC { get; set; }
        public String Colour { get; set; }
        public int LoadCapacity { get; set; }
        public string Accessories { get; set; }
        public decimal UnitPrice { get; set; }
        public string PaymentMethod { get; set; }
        public int Delivery { get; set; }
        public int Vaidity { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

    }

    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Offer>  Offers { get; set; }
    }

    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int ChallanNo { get; set; }
        public int ReceiptNo { get; set; }
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public string paymentype { get; set; }
        public int BillNO { get; set; }
        public int SerialNo { get; set; }
        public decimal TotalPrice { get; set; }
        [ForeignKey("Offer")]
        public int OferId { get; set; }
        public virtual Offer Offer { get; set; }

    }
}
