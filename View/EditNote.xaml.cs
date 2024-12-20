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
    /// Логика взаимодействия для EditNote.xaml
    /// </summary>
    public partial class EditNote : Window
    {
        Note Note { get; set; }
        
        public EditNote(Note note)
        {
            InitializeComponent();
            Note = note;
            ContentTextBox.Text = Note.Content;
        }
        
        public EditNote()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Note.Content = ContentTextBox.Text; // Обновляем содержимое заметки
            DialogResult = true; // Закрываем диалог с результатом "True"
            Close();
        }
    }
}
