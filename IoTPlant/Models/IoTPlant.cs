﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTPlant.Models
{
    public class IoTPlant
    {
        public int Umidade { get; set; }
        public int Temperatura { get; set; }
        public string Solo { get; set; }
        public string ImgUrl { get; set; }
        public string Color { get; set; }
        public string Condicao { get; set; }
    }
}
