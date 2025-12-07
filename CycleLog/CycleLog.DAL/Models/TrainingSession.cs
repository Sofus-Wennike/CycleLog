using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleLog.DAL.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public double DistanceKm { get; set; }
    }
}
