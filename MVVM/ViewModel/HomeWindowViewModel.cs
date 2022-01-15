using AVGECTSGrade.MVVM.Model;
using AVGECTSGrade.MVVM.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace AVGECTSGrade.MVVM.ViewModel
{
    internal class HomeWindowViewModel : INotifyPropertyChanged
    {
        private Visibility initialDialogVisibility;
        private Visibility homeViewVisibility;

        private ICommand createNewFileCommand;
        private ICommand openExistingFileCommand;

        private string shownName;
        private string shownStudyName;
        private ObservableCollection<Subject> shownSubjectList;

        public FileProperty FileProperty;
        private string filePath;

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
        public Visibility HomeViewVisibility
        {
            get { return homeViewVisibility; }

            set
            {
                homeViewVisibility = value;
                NotifyPropertyChanged("HomeViewVisibility");
            }
        }
        public string ShownName
        {
            get { return shownName; }
            set
            {
                shownName = value;
                NotifyPropertyChanged("ShownName");
            }
        }
        public string ShownStudyName
        {
            get { return shownStudyName; }
            set
            {
                shownStudyName = value;
                NotifyPropertyChanged("ShownStudyName");
            }
        }
        public ObservableCollection<Subject> ShownSubjectList
        {
            get { return shownSubjectList; }
            set
            {
                shownSubjectList = value;
                NotifyPropertyChanged("ShownSubjectList");
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
            HomeViewVisibility = Visibility.Hidden;
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
            if (newFileWindow.ShowDialog() == true)
            {
                this.filePath = newFileWindow.FilePath;
                AnalyzeFile(this.filePath);
            }
        }
        public void OpenExistingFileCommandExecute()
        {
            OpenFileDialog folderBrowserDialog = new OpenFileDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.filePath = folderBrowserDialog.FileName;
                AnalyzeFile(this.filePath);
            }
        }

        private void AnalyzeFile(string path)
        {
            string json;
            using (StreamReader r = new StreamReader(path))
            {
                json = r.ReadToEnd();
            }
            FileProperty = JsonConvert.DeserializeObject<FileProperty>(json);

            ShownName = FileProperty.Name;
            ShownStudyName = FileProperty.StudyName;
            ShownSubjectList = FileProperty.SubjectList;

            InitialDialogVisibility = Visibility.Hidden;
            HomeViewVisibility = Visibility.Visible;
        }
    }
}
