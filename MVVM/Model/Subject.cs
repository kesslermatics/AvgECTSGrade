﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVGECTSGrade.MVVM.Model
{
    public class Subject
    {
        public String Name { get; set; }
        public int ECTS { get; set; }
        public bool IsCalculated { get; set; }
        public Subject(String name, int ects, bool isCalculated)
        {
            this.Name = name;
            this.ECTS = ects;
            this.IsCalculated = isCalculated;
        }
    }
}
