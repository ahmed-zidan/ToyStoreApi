using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyStore.Core.Models
{
    public class Test:BaseEntity
    {
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime LeaveTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime AttendTime { get; set; }

    }
}
