using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.DbModel;

namespace TaskManagement.Domain.Entities
{
    public class Report
    {
        public int UserId { get; set; }
        public int AmountDoneTasks { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
