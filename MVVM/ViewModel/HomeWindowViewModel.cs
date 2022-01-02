using AVGECTSGrade.MVVM.Model;
using AVGECTSGrade.MVVM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace AVGECTSGrade.MVVM.ViewModel
{
    internal class HomeWindowViewModel : INotifyPropertyChanged
    {
        private Visibility initialDialogVisibility;

        private ICommand createNewFileCommand;
        private ICommand openExistingFileCommand;

        public ICommand CreateNewFileCommand
        {
            get
            {
                return createNewFileCommand ?? (createNewFileCommand = new CommandHandler(() => CreateNewFileCommandExecute(), () => CanExecuteTrue));
            }
        }
        public ICommand OpenExistingFileCommand
        {
            get
            {
                return openExistingFileCommand ?? (openExistingFileCommand = new CommandHandler(() => OpenExistingFileCommandExecute(), () => CanExecuteTrue));
            }
        }

        public Visibility InitialDialogVisibility
        {
            get { return initialDialogVisibility; }

            set
            {
                initialDialogVisibility = value;
                NotifyPropertyChanged("InitialDialogVisibility");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public HomeWindowViewModel()
        {
            InitialDialogVisibility = Visibility.Visible;
        }
        public bool CanExecuteTrue
        {
            get
            {
                return true;
            }
        }

        public void CreateNewFileCommandExecute()
        {
            NewFileWindow newFileWindow = new NewFileWindow();
            newFileWindow.Focus();
            newFileWindow.ShowDialog();
        }
        public void OpenExistingFileCommandExecute()
        {
            OpenFileDialog folderBrowserDialog = new OpenFileDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SubjectList.Instance.addSubject(new Subject("AI", 10));
                SubjectList.Instance.addSubject(new Subject("SE", 15));
                SubjectList.Instance.addSubject(new Subject("MI", 1));
            }
        }
    }
}
