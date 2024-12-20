﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savills.SIA.Models.Dto.Contact
{
    public class UpdateContactTypeRequest
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public int Seq { get; set; }

        public bool IsActive { get; set; }
    }
}
