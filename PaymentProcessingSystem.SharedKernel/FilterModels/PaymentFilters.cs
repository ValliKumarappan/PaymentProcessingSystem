using PaymentProcessingSystem.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessingSystem.SharedKernel.FilterModels
{
    public class PaymentFilters
    {
        public PaymentFilters()
        {
            if (pageSize <= 0) pageSize = 5;
            if (page <= 0) page = 1;
        }
        public int page { get; set; }
        public int pageSize { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public EntityStatusEnum Status { get; set; } 
        public DateTime TransDate { get; set; }
        public string Currency { get; set; }
    }
}
