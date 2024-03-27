using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    internal class Bill
    {
        public Guid Id {  get; set; }
        public BillType Type { get; set; }
        public string Name { get; set; }
        public float Ammount { get; set; }
        public bool IsPaid { get; set; }

        public Bill(BillType type, string name, float ammount)
        {
            Id = Guid.NewGuid();
            Type = type;
            Name = name;
            Ammount = ammount;
            IsPaid = false;
        }

        public static List<Bill> GetUnpaiedBills(List<Bill> bills)
        {
            List<Bill> result = new List<Bill>();
            Console.WriteLine("The unpaide bills are:");
            foreach (Bill bill in bills)
            {
                if (!bill.IsPaid)
                {
                    result.Add(bill);
                    Console.WriteLine($"{bill.Type}, {bill.Name}, {bill.Ammount}");
                }
            }
            return result;
        }
    }
}
