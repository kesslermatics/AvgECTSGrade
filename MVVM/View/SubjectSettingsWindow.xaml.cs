using AVGECTSGrade.MVVM.Model;
using AVGECTSGrade.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AVGECTSGrade.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für NewSubjectWindow.xaml
    /// </summary>
    public partial class SubjectSettingsWindow : Window
    {
        public Subject Subject;
        public bool AddAnotherIsChecked;
        public SubjectSettingsWindow(Subject subject)
        {
            InitializeComponent();
            this.DataContext = new SubjectSettingsWindowViewModel(this, subject);
        }
    }
}
