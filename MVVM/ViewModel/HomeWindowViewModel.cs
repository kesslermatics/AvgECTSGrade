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
        private ICommand editExistingFileCommand;
        private ICommand addCommand;
        private ICommand editCommand;
        private ICommand deleteCommand;
        private ICommand checkBoxClickedCommand;

        private string shownName;
        private string shownStudyName;
        private ObservableCollection<Subject> shownSubjectList;

        private FileProperty FileProperty;
        private Subject selectedSubject;
        private string filePath;

        private string averageGrade;
        private int totalSubjects;
        private int totalSubjectsInCalculation;
        private int totalECTS;
        private int totalECTSInCalculation;

        public HomeWindowViewModel()
        {
            InitialDialogVisibility = Visibility.Visible;
            HomeViewVisibility = Visibility.Hidden;
        }

        #region Public Properties
        public ICommand CreateNewFileCommand
        {
            get
            {
                return createNewFileCommand ?? (createNewFileCommand = new CommandHandler(() => CreateNewFileCommandExecute(), () => true));
            }
        }
        public ICommand EditExistingFileCommand
        {
            get
            {
                return editExistingFileCommand ?? (editExistingFileCommand = new CommandHandler(() => EditExistingCommandExecute(), () => true));
            }
        }
        public ICommand OpenExistingFileCommand
        {
            get
            {
                return openExistingFileCommand ?? (openExistingFileCommand = new CommandHandler(() => OpenExistingFileCommandExecute(), () => true));
            }
        }
        public ICommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new CommandHandler(() => AddCommandExecute(), () => true));
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return editCommand ?? (editCommand = new CommandHandler(() => EditCommandExecute(), () => { return (selectedSubject != null); }));
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new CommandHandler(() => DeleteCommandExecute(), () => { return (selectedSubject != null); }));
            }
        }
        public ICommand CheckBoxClickedCommand
        {
            get
            {
                return checkBoxClickedCommand ?? (checkBoxClickedCommand = new CommandHandler(() => CheckBoxClickedCommandExecute(), () => true));
            }
        }
        public Subject SelectedSubject
        {
            get { return selectedSubject; }

            set
            {
                selectedSubject = value;
                NotifyPropertyChanged("SelectedSubject");
            }
        }
        public string FilePath
        {
            get { return filePath; }

            set
            {
                filePath = value;
                NotifyPropertyChanged("FilePath");
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
        public string AverageGrade
        {
            get { return averageGrade; }

            set
            {
                averageGrade = value;
                NotifyPropertyChanged("AverageGrade");
            }
        }
        public int TotalSubjects
        {
            get { return totalSubjects; }

            set
            {
                totalSubjects = value;
                NotifyPropertyChanged("TotalSubjects");
            }
        }
        public int TotalSubjectsInCalculation
        {
            get { return totalSubjectsInCalculation; }

            set
            {
                totalSubjectsInCalculation = value;
                NotifyPropertyChanged("TotalSubjectsInCalculation");
            }
        }
        public int TotalECTS
        {
            get { return totalECTS; }

            set
            {
                totalECTS = value;
                NotifyPropertyChanged("TotalECTS");
            }
        }
        public int TotalECTSInCalculation
        {
            get { return totalECTSInCalculation; }

            set
            {
                totalECTSInCalculation = value;
                NotifyPropertyChanged("TotalECTSInCalculation");
            }
        }
        #endregion

        #region Command Executes
        public void CreateNewFileCommandExecute()
        {
            FileSettingsWindow newFileWindow = new FileSettingsWindow("", "", "");
            newFileWindow.Focus();
            if (newFileWindow.ShowDialog() == true)
            {
                FilePath = newFileWindow.FilePath;
                AnalyzeFile(FilePath);
            }
        }
        public void EditExistingCommandExecute()
        {
            FileSettingsWindow newFileWindow = new FileSettingsWindow(shownName, shownStudyName, filePath);
            newFileWindow.Focus();
            if (newFileWindow.ShowDialog() == true)
            {
                FilePath = newFileWindow.FilePath;
                AnalyzeFile(FilePath);
            }
        }
        public void OpenExistingFileCommandExecute()
        {
            OpenFileDialog folderBrowserDialog = new OpenFileDialog();
            folderBrowserDialog.Filter = "avgGrade files(*.avgGrade) | *.avgGrade";
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FilePath = folderBrowserDialog.FileName;
                AnalyzeFile(FilePath);
            }
        }
        private void AddCommandExecute()
        {
            SubjectSettingsWindow subjectSettingsWindow = new SubjectSettingsWindow(new Subject("", 0, 0, false));
            if (subjectSettingsWindow.ShowDialog() == true)
            {
                ShownSubjectList.Add(subjectSettingsWindow.Subject);
                SaveAll();
                if (subjectSettingsWindow.AddAnotherIsChecked == true)
                {
                    AddCommandExecute();
                }
            }
        }
        private void EditCommandExecute()
        {
            SubjectSettingsWindow subjectSettingsWindow = new SubjectSettingsWindow(SelectedSubject);
            if (subjectSettingsWindow.ShowDialog() == true)
            {
                var found = ShownSubjectList.FirstOrDefault(x => x.Name == SelectedSubject.Name);
                int i = ShownSubjectList.IndexOf(found);
                ShownSubjectList[i] = subjectSettingsWindow.Subject;
                SaveAll();
            }
        }
        private void DeleteCommandExecute()
        {
            var found = ShownSubjectList.FirstOrDefault(x => x.Name == SelectedSubject.Name);
            ShownSubjectList.Remove(found);
            SaveAll();
        }
        private void CheckBoxClickedCommandExecute()
        {
            SaveAll();
        }
        #endregion
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
            SaveAll();
        }

        private void SaveAll()
        {
            UpdateView();
            FileProperty fileProperty = new FileProperty(this.ShownName, this.ShownStudyName, ShownSubjectList);
            string jsonString = JsonConvert.SerializeObject(fileProperty);
            File.WriteAllText(FilePath, jsonString);
        }
        private void UpdateView()
        {
            TotalSubjects = ShownSubjectList.Count;
            var onlyCalculatedSubjects = ShownSubjectList.Where(subject => subject.IsCalculated == true).ToList();
            TotalSubjectsInCalculation = onlyCalculatedSubjects.Count;
            TotalECTS = ShownSubjectList.Sum(x => Convert.ToInt32(x.ECTS));
            TotalECTSInCalculation = onlyCalculatedSubjects.Sum(x => Convert.ToInt32(x.ECTS));
            if (TotalECTS != 0)
            {
                float total = 0;
                foreach (var subject in onlyCalculatedSubjects)
                {
                    total += subject.Grade * subject.ECTS;
                }
                AverageGrade = (total / TotalECTSInCalculation).ToString("0.00");
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
