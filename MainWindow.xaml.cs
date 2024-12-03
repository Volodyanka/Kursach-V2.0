using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace CalendarWithNotes
{
    public partial class MainWindow : Window
    {
        private Dictionary<DateTime, string> notes = new Dictionary<DateTime, string>();
        private const string connectionString = "Data Source=notes.db;Version=3;";

        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Notes (Date TEXT PRIMARY KEY, Note TEXT)", connection);
                command.ExecuteNonQuery();
                LoadNotes();
            }
        }

        private void LoadNotes()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("SELECT Date, Note FROM Notes", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime date = DateTime.Parse(reader["Date"].ToString());
                        string note = reader["Note"].ToString();
                        notes[date] = note;
                    }
                }
            }
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = calendar.SelectedDate.Value;
                if (notes.TryGetValue(selectedDate, out string note))
                {
                    txtNote.Text = note;
                }
                else
                {
                    txtNote.Text = "";
                }
            }
        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = calendar.SelectedDate.Value;
                string noteText = txtNote.Text;

                // Добавляем или обновляем заметку в словаре и базе данных
                notes[selectedDate] = noteText;
                SaveNoteToDatabase(selectedDate, noteText);
            }
        }

        private void SaveNoteToDatabase(DateTime date, string note)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT OR REPLACE INTO Notes (Date, Note) VALUES (@Date, @Note)", connection);
                command.Parameters.AddWithValue("@Date", date);
                command.Parameters.AddWithValue("@Note", note);
                command.ExecuteNonQuery();
            }
        }

        private void btnRemoveNote_Click(object sender, RoutedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = calendar.SelectedDate.Value;
                if (notes.ContainsKey(selectedDate))
                {
                    notes.Remove(selectedDate);
                    DeleteNoteFromDatabase(selectedDate);
                    txtNote.Text = "";
                }
            }
        }

        private void DeleteNoteFromDatabase(DateTime date)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("DELETE FROM Notes WHERE Date = @Date", connection);
                command.Parameters.AddWithValue("@Date", date);
                command.ExecuteNonQuery();
            }
        }
    }
}
