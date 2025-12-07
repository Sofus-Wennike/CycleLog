using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleLog.DAL.DTO
{
    public class TrainingSessionDTO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public double DistanceKm { get; set; }
    }
}
