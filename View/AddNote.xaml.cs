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
using пробник.Model;

namespace пробник.View
{
    /// <summary>
    /// Логика взаимодействия для AddNote.xaml
    /// </summary>
    public partial class AddNote : Window
    {
        public Note Note { get; private set; }
        public AddNote(Note note)
        {
            InitializeComponent();
            Note = note;
            DataContext = Note;
        }

        public AddNote()
        {
            InitializeComponent();
        }

        private void AcceptSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
