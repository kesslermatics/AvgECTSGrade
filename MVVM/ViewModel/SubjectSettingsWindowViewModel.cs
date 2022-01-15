using AVGECTSGrade.MVVM.Model;
using AVGECTSGrade.MVVM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AVGECTSGrade.MVVM.ViewModel
{
    public class SubjectSettingsWindowViewModel : INotifyPropertyChanged
    {
        private string subjectNameText;
        private int subjectECTS;
        private bool isCalculated;
        private SubjectSettingsWindow subjectSettingsWindow;
        private ICommand cancelCommand;
        private ICommand saveCommand;
        public SubjectSettingsWindowViewModel(SubjectSettingsWindow subjectSettingsWindow, Subject subject) 
        {
            SubjectNameText = subject.Name;
            SubjectECTS = subject.ECTS;
            IsCalculated = subject.IsCalculated;
            this.subjectSettingsWindow = subjectSettingsWindow;
        }

        #region Public Properties

        public String SubjectNameText
        {
            get { return subjectNameText; }

            set
            {
                subjectNameText = value;
                NotifyPropertyChanged("SubjectNameText");
            }
        }
        public int SubjectECTS
        {
            get { return subjectECTS; }

            set
            {
                subjectECTS = value;
                NotifyPropertyChanged("SubjectECTS");
            }
        }
        public bool IsCalculated
        {
            get { return isCalculated; }

            set
            {
                isCalculated = value;
                NotifyPropertyChanged("IsCalculated");
            }
        }
        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new CommandHandler(() => CancelCommandExecute(), () => true));
            }
        }
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new CommandHandler(() => SaveCommandExecute(), () => true));
            }
        }

        #endregion

        #region Command Executes

        public void CancelCommandExecute()
        {
            this.subjectSettingsWindow.DialogResult = false;
            this.subjectSettingsWindow.Close();
        }
        public void SaveCommandExecute()
        {
            this.subjectSettingsWindow.Subject = new Subject(SubjectNameText, SubjectECTS, IsCalculated);
            this.subjectSettingsWindow.DialogResult = true;
            this.subjectSettingsWindow.Close();
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
