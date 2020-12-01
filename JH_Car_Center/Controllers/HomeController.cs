using ceTe.DynamicPDF.HtmlConverter;
using JH_Car_Center.Models;
using JH_Car_Center.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JH_Car_Center.Controllers
{
    public class HomeController : Controller
    {
        
        public readonly DbModel _db;

        public HomeController(DbModel db)
        {
            this._db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View(_db.Offers.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Quatation(int id)
        {
            var data = (from c in _db.Customers
                        join o in _db.Offers
                        on c.ID equals o.CustomerId
                        where o.ID ==id
                        select new CenterVM
                        {
                            Name = c.Name,
                            Address = c.Address,
                            Date = o.Date,
                            TypeOfVehicle = o.TypeOfVehicle,
                            ChassisNo = o.ChassisNo,
                            EngineNO = o.EngineNO,
                            ManufactureYear = o.ManufactureYear,
                            CC = o.CC,
                            Colour = o.Colour,
                            LoadCapacity = o.LoadCapacity,
                            Accessories = o.Accessories,
                            Delivery = o.Delivery,
                            Vaidity = o.Vaidity,
                            UnitPrice = o.UnitPrice
                        }).FirstOrDefault();
            string tkInWord = NumberToWords(Convert.ToInt32(data.UnitPrice));

            ViewBag.word = tkInWord;

            return View(data);

        }
        [HttpPost]
        public IActionResult Create( /*FormCollection formval,*/ CenterVM vM)
        {
            
            //ViewBag.lstSkills = (from a in _db.Offers select a).ToList();

            using (var transection = _db.Database.BeginTransaction())
            {
                try
                {
                    var customer = new Customer
                    {
                        ID=0,
                        Name = vM.Name,
                        Address = vM.Address
                    };
                    _db.Customers.Add(customer);
                    _db.SaveChanges();

                    var offer = new Offer
                    {
                        ID=0,
                        TypeOfVehicle = vM.TypeOfVehicle,
                        ChassisNo = vM.ChassisNo,
                        EngineNO = vM.EngineNO,
                        ManufactureYear = vM.ManufactureYear,
                        CC = vM.CC,
                        Colour = vM.Colour,
                        LoadCapacity = vM.LoadCapacity,
                        Accessories = vM.Accessories,
                        UnitPrice = vM.UnitPrice,
                        PaymentMethod = vM.PaymentMethod,
                        Delivery = vM.Delivery,
                        Vaidity = vM.Vaidity,
                        Date=DateTime.Now.Date,
                        CustomerId = customer.ID
                    };
                    _db.Offers.Add(offer);
                    _db.SaveChanges();
                    transection.Commit();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    transection.Rollback();
                    throw e;
                }
            }
            
        }
        [HttpGet]
        public IActionResult EditOffer(int id)
        {
            return View(_db.Offers.Find(id));
        }
        [HttpPost]
        public IActionResult EditOffer(int id, Offer offer)
        {
            
                    _db.Entry(offer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteOffer(int id)
        {
            var offer= _db.Offers.Find(id);
            _db.Offers.Remove(offer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult GetOrder(int id)
        {
            var offer = _db.Offers.Find(id);

            var data = (from ord in _db.Orders
                        join o in _db.Offers
                        on ord.OferId equals o.ID
                        join c in _db.Customers
                        on o.CustomerId equals  c.ID
                        select new CenterVM
                        {
                            Name=c.Name,
                            Address=c.Address,
                            Date=o.Date,
                            TypeOfVehicle=o.TypeOfVehicle,
                            ChassisNo=o.ChassisNo,
                            EngineNO=o.EngineNO,
                            ManufactureYear=o.ManufactureYear,
                            CC=o.CC,
                            Colour=o.Colour,
                            LoadCapacity=o.LoadCapacity,
                            Accessories=o.Accessories,
                            UnitPrice=o.UnitPrice,
                            
                        }).SingleOrDefault();

             
            //AssignedEmployees = new string[] { "Ian", "Danny", "Mikey" }
            return View(data);
        }
        [HttpGet]
        public IActionResult CreateOrder(int id)
        {
            var oferid = _db.Offers.Find(id);
            ViewBag.offerId = oferid.ID;
            return View();
        }
        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            int chlln = 123;
            int bl = 456;
            int rcpt = 789;

            var existchallan = (from u in _db.Orders
                                orderby u.ChallanNo descending
                                select u).Take(1).FirstOrDefault();

            if (existchallan != null)
            {
                chlln = existchallan.ChallanNo + 1;
            }

            if (existchallan != null)
            {
                bl = existchallan.BillNO + 1;
            }

            if (existchallan != null)
            {
                rcpt = existchallan.ReceiptNo + 1;
            }
            using (var transection = _db.Database.BeginTransaction())
            {
                try
                {
                    var obj = new Order
                    {
                        ID = 0,
                        Date = DateTime.Now.Date,
                        Remarks = order.Remarks,
                        Description=order.Description,
                        ChallanNo = chlln,
                        ReceiptNo = rcpt,
                        BillNO= bl,
                        Quantity = order.Quantity,
                        SerialNo = 34523,
                        paymentype=order.paymentype,
                        OferId=order.OferId,
                        
                    };
                    _db.Orders.Add(obj);
                    
                    _db.SaveChanges();

                    var offer = _db.Offers.Find(obj.OferId);

                    //Order o = new Order();
                    obj.TotalPrice = offer.UnitPrice * obj.Quantity;
                    _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                    transection.Commit();
                    return RedirectToAction("Index");

                    //var report = Converter.Convert(new Uri("https://localhost:44330/Home/Quatation/" + id));
                    //return File(report, "application/pdf");
                }
                catch(Exception e)
                {
                    transection.Rollback();
                    throw e;
                }
            }
                
        }
        [HttpGet]
        public ActionResult GetChallan(int id)
        {
            try
            {
                var data = (from ord in _db.Orders
                            join o in _db.Offers
                            on ord.OferId equals o.ID
                            join c in _db.Customers
                            on o.CustomerId equals c.ID
                            where o.ID == id
                            select new ChalanVM
                            {
                                Name = c.Name,
                                Address = c.Address,
                                Date = o.Date,
                                Description = ord.Description,
                                ChallanNo = 1232134,
                                Quantity = ord.Quantity,
                                Remarks = ord.Remarks,
                                TypeOfVehicle = o.TypeOfVehicle,
                                ChassisNo = o.ChassisNo,
                                EngineNO = o.EngineNO,
                                ManufactureYear = o.ManufactureYear,
                                CC = o.CC,
                                Colour = o.Colour,
                                LoadCapacity = o.LoadCapacity,
                                Accessories = o.Accessories,
                                UnitPrice = o.UnitPrice,

                            }).FirstOrDefault();
                return View(data);
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
        public ActionResult PrintQuataions(int id)
        {
            var report = Converter.Convert(new Uri("https://localhost:44330/Home/Quatation/" + id));
            return File(report, "application/pdf");
        }
        public ActionResult PrintChallan(int id)
        {
            var report = Converter.Convert(new Uri("https://localhost:44330/Home/GetChallan/" + id));
            return File(report, "application/pdf");
        }
        [HttpGet]
        public IActionResult GetReceipt(int id)
        {
            var data = (from ord in _db.Orders
                        join o in _db.Offers
                        on ord.OferId equals o.ID
                        join c in _db.Customers
                        on o.CustomerId equals c.ID
                        where o.ID == id
                        select new CenterVM
                        {
                            SerialNo = ord.SerialNo,
                            Date = ord.Date,
                            Name = c.Name,
                            PaymentType = ord.paymentype,
                            TotalPrice = ord.TotalPrice,
                            PaymentMethod = o.PaymentMethod
                        }).FirstOrDefault();
            return View(data);
        }
        public ActionResult PrintReceipt(int id)
        {
            var report = Converter.Convert(new Uri("https://localhost:44330/Home/GetReceipt/" + id));
            return File(report, "application/pdf");
        }
        public string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}
