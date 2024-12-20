using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using пробник.Model;
using пробник.View;

namespace пробник.ViewModel
{
    public class MainVM : BaseViewModel
    {
        public ApplicationContext db = new ApplicationContext();
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        private Note selectedNote;
        public Note SelectedNote
        {
            get => selectedNote;
            set { selectedNote = value; OnPropertyChanged("SelectedNote"); }
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged();
                LoadNotesForSelectedDate(selectedDate); // Загружаем заметки при изменении даты
            }
        }

        MainWindow mw {  get; set; }

        public MainVM()
        {
            db.Database.EnsureCreated();
            db.Notes.Load();
            LoadNotesForSelectedDate(selectedDate);
        }


        private RelayCommand addNote;
        public RelayCommand AddNote
        {
            get
            {
                return addNote ??
                (addNote = new RelayCommand(obj =>
                {
                    AddNote addNoteDialog = new AddNote(new Note());
                    if (addNoteDialog.ShowDialog() == true)
                    {
                        Note note = addNoteDialog.Note;
                        note.CreatedAt = SelectedDate; // Устанавливаем дату заметки
                        db.Notes.Add(note);
                        db.SaveChanges();
                        LoadNotesForSelectedDate(SelectedDate); // Обновляем список заметок
                    }
                }));
            }
        }

        private RelayCommand deleteNote;
        public RelayCommand DeleteNote
        {
            get
            {
                return deleteNote ??
                (deleteNote = new RelayCommand(obj =>
                {
                    if(SelectedNote != null)
                    {
                        db.Notes.Remove(SelectedNote);
                        db.SaveChanges();
                        LoadNotesForSelectedDate(SelectedDate);
                    }
                }));
            }
        }

        private RelayCommand editNote;
        public RelayCommand EditNote
        {
            get
            {
                return editNote ?? (editNote = new RelayCommand(obj =>
                {
                    if (SelectedNote != null)
                    {
                        // Создаем диалог для редактирования заметки
                        EditNote editNoteDialog = new EditNote(SelectedNote);
                        if (editNoteDialog.ShowDialog() == true)
                        {
                            db.SaveChanges(); // Сохраняем изменения в базе данных
                            LoadNotesForSelectedDate(SelectedDate); // Обновляем список заметок
                        }
                    }
                }, obj => SelectedNote != null)); // Команда активна только если есть выбранная заметка
            }
        }

        private void LoadNotesForSelectedDate(DateTime date)
        {
            Notes.Clear(); // Очищаем текущий список заметок

            // Загружаем заметки из базы данных на выбранную дату
            var notesOnSelectedDate = db.Notes.Where(n => n.CreatedAt.Date == date.Date).ToList();

            foreach (var note in notesOnSelectedDate)
            {
                Notes.Add(note); // Добавляем заметки в коллекцию
            }
        }

    }
}
