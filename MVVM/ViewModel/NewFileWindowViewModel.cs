using AVGECTSGrade.MVVM.Model;
using AVGECTSGrade.MVVM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace AVGECTSGrade.MVVM.ViewModel
{
    internal class NewFileWindowViewModel : INotifyPropertyChanged
    {
        private ICommand enterFilePathButtonCommand;
        private ICommand cancelButtonCommand;
        private ICommand finishButtonCommand;
        private String nameText;
        private String studyText;
        private String fileNameText;
        private String filePathText;
        private NewFileWindow newFileWindow;

        public NewFileWindowViewModel(NewFileWindow newFileWindow)
        {
            this.newFileWindow = newFileWindow;
            NameText = "";
            StudyText = "";
            FileNameText = "";
            FilePathText = "";
        }

        public ICommand EnterFilePathButtonCommand
        {
            get
            {
                return enterFilePathButtonCommand ?? (enterFilePathButtonCommand = new CommandHandler(() => EnterFilePathCommandExecute(), () => CanExecuteTrue));
            }
        }
        public ICommand CancelButtonCommand
        {
            get
            {
                return cancelButtonCommand ?? (cancelButtonCommand = new CommandHandler(() => CancelButtonCommandExecute(), () => CanExecuteTrue));
            }
        }
        public ICommand FinishButtonCommand
        {
            get
            {
                return finishButtonCommand ?? (finishButtonCommand = new CommandHandler(() => FinishButtonCommandExecute(), () => FinishCanExecute));
            }
        }
        public bool CanExecuteTrue
        {
            get
            {
                return true;
            }
        }
        public bool FinishCanExecute
        {
            get
            {
                return (!NameText.Equals("") && !StudyText.Equals("") && !FileNameText.Equals("") && !FilePathText.Equals(""));
            }
        }
        public String NameText
        {
            get { return nameText; }

            set
            {
                nameText = value;
                NotifyPropertyChanged("NameText");
            }
        }
        public String StudyText
        {
            get { return studyText; }

            set
            {
                studyText = value;
                NotifyPropertyChanged("StudyText");
            }
        }
        public String FileNameText
        {
            get { return fileNameText; }

            set
            {
                fileNameText = value;
                NotifyPropertyChanged("FileNameText");
            }
        }
        public String FilePathText
        {
            get { return filePathText; }

            set
            {
                filePathText = value;
                NotifyPropertyChanged("FilePathText");
            }
        }

        public void EnterFilePathCommandExecute()
        {
            FolderBrowserDialog  folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FilePathText = folderBrowserDialog.SelectedPath;
            }
        }

        public void CancelButtonCommandExecute()
        {
            newFileWindow.Close();
        }
        public void FinishButtonCommandExecute()
        {

        }

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
