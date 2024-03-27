using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    internal class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CardNumber {  get; set; }
        public DateOnly ExpirationDate {  get; set; }
        public string CVV {  get; set; }
        public string PIN { get; set; }
        public string CardHolderName { get; set; }
        public string AccountLinkedTo { get; set; }
        public bool IsBlocked {  get; set; }
    }
}