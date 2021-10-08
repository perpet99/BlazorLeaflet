﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorLeaflet.Samples.Models
{
    public partial class Update
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Version { get; set; }
        public short CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
