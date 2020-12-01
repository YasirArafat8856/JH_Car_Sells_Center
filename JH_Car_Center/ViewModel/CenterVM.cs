using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JH_Car_Center.ViewModel
{
    public class CenterVM
    {
        public int ID { get; set; }
        public string TypeOfVehicle { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNO { get; set; }
        public int ManufactureYear { get; set; }
        public int CC { get; set; }
        public String Colour { get; set; }
        public int LoadCapacity { get; set; }
        public string Accessories { get; set; }
        public decimal UnitPrice { get; set; }
        public string PaymentMethod { get; set; }
        public int Delivery { get; set; }
        public int Vaidity { get; set; }
        public int CustomerId
        {
            get; set;
        }
        public string Name { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int ChallanNo { get; set; }
        public string Description { get; set; }
        public int ReceiptNo { get; set; }
        public int BillNO { get; set; }
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public int SerialNo { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentType { get; set; }
    }
}
