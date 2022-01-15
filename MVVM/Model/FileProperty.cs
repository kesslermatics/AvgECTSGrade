using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVGECTSGrade.MVVM.Model
{
    public class FileProperty
    {
        public string Name { get; set; }
        public string StudyName { get; set; }

        public ObservableCollection<Subject> SubjectList { get; set; }

        public FileProperty(string name, string studyName, ObservableCollection<Subject> subjectList)
        {
            this.Name = name;
            this.StudyName = studyName;
            this.SubjectList = subjectList;
        }
    }
}
