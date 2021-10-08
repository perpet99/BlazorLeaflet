﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorLeaflet.Samples.Models
{
    public partial class Drife
    {
        public Drife()
        {
            Positions = new HashSet<Position>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? OutsideTempAvg { get; set; }
        public short? SpeedMax { get; set; }
        public short? PowerMax { get; set; }
        public short? PowerMin { get; set; }
        public decimal? StartIdealRangeKm { get; set; }
        public decimal? EndIdealRangeKm { get; set; }
        public double? StartKm { get; set; }
        public double? EndKm { get; set; }
        public double? Distance { get; set; }
        public short? DurationMin { get; set; }
        public short CarId { get; set; }
        public decimal? InsideTempAvg { get; set; }
        public int? StartAddressId { get; set; }
        public int? EndAddressId { get; set; }
        public decimal? StartRatedRangeKm { get; set; }
        public decimal? EndRatedRangeKm { get; set; }
        public int? StartPositionId { get; set; }
        public int? EndPositionId { get; set; }
        public int? StartGeofenceId { get; set; }
        public int? EndGeofenceId { get; set; }

        public virtual Car Car { get; set; }
        public virtual Address EndAddress { get; set; }
        public virtual Geofence EndGeofence { get; set; }
        public virtual Position EndPosition { get; set; }
        public virtual Address StartAddress { get; set; }
        public virtual Geofence StartGeofence { get; set; }
        public virtual Position StartPosition { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
