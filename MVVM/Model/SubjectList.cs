using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVGECTSGrade.MVVM.Model
{
    internal sealed class SubjectList
    {
        private ObservableCollection<Subject> subjectList;
        SubjectList()
        {
            subjectList = new ObservableCollection<Subject>();
        }
        public void addSubject(Subject subject)
        {
            this.subjectList.Add(subject);
        }

        public ObservableCollection<Subject> getSubjectList()
        {
            return this.subjectList;
        }

        public static SubjectList Instance
        {
            get { return Lazy.Value; }
        }

        private static readonly Lazy<SubjectList> Lazy = new Lazy<SubjectList>(() => new SubjectList());
    }
}
