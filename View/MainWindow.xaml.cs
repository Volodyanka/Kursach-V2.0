using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using пробник;
using пробник.ViewModel;


namespace пробник
{
   public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent ();
            DataContext = new MainVM();
        }

        private void Calendar_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataContext is MainVM viewModel)
            {
                viewModel.SelectedDate = calendar.SelectedDate ?? DateTime.Now; // Устанавливаем выбранную дату в ViewModel
            }
        }
    }

}
