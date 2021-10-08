using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorLeaflet.Samples.Models
{
    public partial class Setting
    {
        public long Id { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string BaseUrl { get; set; }
        public string GrafanaUrl { get; set; }
        public string Language { get; set; }
    }
}
