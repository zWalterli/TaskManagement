using System.ComponentModel.DataAnnotations;
using TaskManagement.Domain.Enum;

namespace TaskManagement.Domain.DbModel
{
    public class BaseDbModel
    {
        [Key]
        public int? Id { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public int? UserIdCreated { get; set; }
        public int? UserIdLastUpdate { get; set; }
    }
}