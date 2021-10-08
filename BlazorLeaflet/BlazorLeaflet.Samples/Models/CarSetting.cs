﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorLeaflet.Samples.Models
{
    public partial class CarSetting
    {
        public long Id { get; set; }
        public int SuspendMin { get; set; }
        public int SuspendAfterIdleMin { get; set; }
        public bool ReqNotUnlocked { get; set; }
        public bool FreeSupercharging { get; set; }
        public bool? UseStreamingApi { get; set; }

        public virtual Car Car { get; set; }
    }
}
