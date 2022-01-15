﻿using AVGECTSGrade.MVVM.Model;
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
        private String filePathText;
        private NewFileWindow newFileWindow;

        public NewFileWindowViewModel(NewFileWindow newFileWindow)
        {
            this.newFileWindow = newFileWindow;
            NameText = "";
            StudyText = "";
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
                return (!NameText.Equals("") && !StudyText.Equals("") && !FilePathText.Equals(""));
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "json files (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePathText = saveFileDialog.FileName;
            }
        }

        public void CancelButtonCommandExecute()
        {
            newFileWindow.Close();
            newFileWindow.DialogResult = false;
        }
        public void FinishButtonCommandExecute()
        {
            newFileWindow.DialogResult = true;

            var list = new ObservableCollection<Subject>();
            list.Add(new Subject("Programmieren 1", 9));
            list.Add(new Subject("Programmieren 2", 12));
            FileProperty fileProperty = new FileProperty(this.nameText, this.studyText, list);
            string jsonString = JsonConvert.SerializeObject(fileProperty);
            File.WriteAllText(filePathText, jsonString);

            newFileWindow.FilePath = filePathText;

            newFileWindow.Close();
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
