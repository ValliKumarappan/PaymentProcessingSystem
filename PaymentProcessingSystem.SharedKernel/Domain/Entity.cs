using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessingSystem.SharedKernel.Domain
{
    public class Entity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.MinValue;
    }
}
