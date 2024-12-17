using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using пробник;


namespace пробник
{
   public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent ();
            DataContext = new MainViewModel();
        }

        private void AddNote_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtNote1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
