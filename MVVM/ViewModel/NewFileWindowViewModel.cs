using AVGECTSGrade.MVVM.Model;
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
        private String filePathText;
        public ICommand EnterFilePathButtonCommand
        {
            get
            {
                return enterFilePathButtonCommand ?? (enterFilePathButtonCommand = new CommandHandler(() => EnterFilePathCommandExecute(), () => CanExecuteTrue));
            }
        }

        public bool CanExecuteTrue
        {
            get
            {
                return true;
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
