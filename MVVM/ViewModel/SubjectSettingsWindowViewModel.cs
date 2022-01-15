using AVGECTSGrade.MVVM.Model;
using AVGECTSGrade.MVVM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AVGECTSGrade.MVVM.ViewModel
{
    public class SubjectSettingsWindowViewModel : INotifyPropertyChanged
    {
        private string subjectNameText;
        private int subjectECTS;
        private float subjectGrade;
        private bool isCalculated;
        private bool addAnotherIsChecked;
        private SubjectSettingsWindow subjectSettingsWindow;
        private ICommand cancelCommand;
        private ICommand saveCommand;
        private Visibility addAnotherVisibility;
        public SubjectSettingsWindowViewModel(SubjectSettingsWindow subjectSettingsWindow, Subject subject) 
        {
            SubjectNameText = subject.Name;
            SubjectECTS = subject.ECTS;
            IsCalculated = subject.IsCalculated;
            this.subjectSettingsWindow = subjectSettingsWindow;
            if (SubjectNameText.Equals(""))
            {
                AddAnotherVisibility = Visibility.Visible;
            } else
            {
                AddAnotherVisibility = Visibility.Hidden;
            }
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
        public bool AddAnotherIsChecked
        {
            get { return addAnotherIsChecked; }

            set
            {
                addAnotherIsChecked = value;
                NotifyPropertyChanged("AddAnotherIsChecked");
            }
        }
        public Visibility AddAnotherVisibility
        {
            get { return addAnotherVisibility; }

            set
            {
                addAnotherVisibility = value;
                NotifyPropertyChanged("AddAnotherVisibility");
            }
        }
        public float SubjectGrade
        {
            get { return subjectGrade; }

            set
            {
                subjectGrade = value;
                NotifyPropertyChanged("SubjectGrade");
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
                return saveCommand ?? (saveCommand = new CommandHandler(() => SaveCommandExecute(), () => (!SubjectNameText.Equals("") && SubjectECTS != 0 && SubjectGrade != 0)));
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
            this.subjectSettingsWindow.Subject = new Subject(SubjectNameText, SubjectECTS, SubjectGrade, IsCalculated);
            this.subjectSettingsWindow.DialogResult = true;
            this.subjectSettingsWindow.AddAnotherIsChecked = AddAnotherIsChecked;
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
