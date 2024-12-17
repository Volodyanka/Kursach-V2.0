using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace пробник
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Note> Notes { get; set; }
        private Note _selectedNote;
        private string _newNoteContent;

        public string NewNoteContent
        {
            get { return _newNoteContent; }
            set
            {
                _newNoteContent = value;
                OnPropertyChanged(nameof(NewNoteContent));
            }
        }

        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }

        public ICommand AddNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand LoadNotesCommand { get; }

        public MainViewModel()
        {
            Notes = new ObservableCollection<Note>();
            AddNoteCommand = new RelayCommand(AddNote);
            DeleteNoteCommand = new RelayCommand(DeleteNote);
            LoadNotesCommand = new RelayCommand(LoadNotes);

            LoadNotes(); // Загрузить заметки при инициализации
        }

        private void LoadNotes()
        {
            using (var context = new NotesContext())
            {
                Notes.Clear();
                foreach (var note in context.Notes.ToList())
                {
                    Notes.Add(note);
                }
            }
        }

        private void AddNote()
        {
            if (!string.IsNullOrWhiteSpace(NewNoteContent))
            {
                var newNote = new Note { Content = NewNoteContent, CreatedAt = DateTime.Now };

                using (var context = new NotesContext())
                {
                    context.Notes.Add(newNote);
                    context.SaveChanges();
                }

                Notes.Add(newNote);
                NewNoteContent = string.Empty; // Очистить текст после добавления
            }
        }

        private void DeleteNote()
        {
            if (SelectedNote != null)
            {
                using (var context = new NotesContext())
                {
                    context.Notes.Remove(context.Notes.Find(SelectedNote.Id));
                    context.SaveChanges();
                }

                Notes.Remove(SelectedNote);
                SelectedNote = null; // Сбросить выделение
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
