using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class RespEventDTO
    {
        public int IdEvent { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AnimalShortInfoDTO> Animals { get; set; }
        public List<RespEventTimestampDTO> Schedules { get; set; }
    }
}
