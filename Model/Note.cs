using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using пробник.Commands;

namespace пробник.Model
{
    public class Note : Property
    {
        public int Id { get; set; }

        private string _content;
        public string Content
        {
            get => _content;
            set { _content = value; OnPropertyChanged("Content"); }
        }

        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get => _createdAt;
            set { _createdAt = value; OnPropertyChanged("CreatedAt"); }
        }

        public Note(int id, string content, DateTime createdAt)
        {
            Id = id;
            Content = content;
            CreatedAt = createdAt;
        }
        public Note() { }
    }
}
