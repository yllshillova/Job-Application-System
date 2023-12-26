using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs
{
    public class SkillDto
    {
        public Guid Id { get; set; }
        public string SkillName { get; set; }
        public string SkillDescription { get; set; }
    }
}