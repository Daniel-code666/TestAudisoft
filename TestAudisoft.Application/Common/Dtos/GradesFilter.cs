using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAudisoft.Application.Common.Dtos
{
    public class GradesFilter
    {
        public string? Name { get; set; }
        public double? Grade { get; set; }

        public DateTime? CreationDateFrom { get; set; }
        public DateTime? CreationDateTo { get; set; }

        public DateTime? ModificationDateFrom { get; set; }
        public DateTime? ModificationDateTo { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
