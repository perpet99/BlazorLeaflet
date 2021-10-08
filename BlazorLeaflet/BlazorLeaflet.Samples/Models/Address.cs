﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorLeaflet.Samples.Models
{
    public partial class Address
    {
        public Address()
        {
            ChargingProcesses = new HashSet<ChargingProcess>();
            DrifeEndAddresses = new HashSet<Drife>();
            DrifeStartAddresses = new HashSet<Drife>();
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Name { get; set; }
        public string HouseNumber { get; set; }
        public string Road { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
        public string StateDistrict { get; set; }
        public string Country { get; set; }
        public string Raw { get; set; }
        public DateTime InsertedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long? OsmId { get; set; }
        public string OsmType { get; set; }

        public virtual ICollection<ChargingProcess> ChargingProcesses { get; set; }
        public virtual ICollection<Drife> DrifeEndAddresses { get; set; }
        public virtual ICollection<Drife> DrifeStartAddresses { get; set; }
    }
}
