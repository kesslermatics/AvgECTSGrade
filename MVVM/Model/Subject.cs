using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVGECTSGrade.MVVM.Model
{
    internal class Subject
    {
        private String name;
        private int ects;

        public Subject(String name, int ects)
        {
            this.name = name;
            this.ects = ects;   
        }
    }
}
