﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wulkanizacja.Desktop.Enums;

namespace Wulkanizacja.Desktop.Models
{
    public class TireModel
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public string SpeedIndex { get; set; }
        public string LoadIndex { get; set; }
        public TireType TireType { get; set; }
        public DateTimeOffset? ManufactureDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ManufactureWeekYear { get; set; }
        public DateTimeOffset? CreateDate { get; set; }
        public DateTimeOffset? EditDate { get; set; }
        public string? Comments { get; set; }
        public int QuantityInStock { get; set; }
    }
}
