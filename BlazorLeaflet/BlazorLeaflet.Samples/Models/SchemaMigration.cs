using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorLeaflet.Samples.Models
{
    public partial class SchemaMigration
    {
        public long Version { get; set; }
        public DateTime? InsertedAt { get; set; }
    }
}
