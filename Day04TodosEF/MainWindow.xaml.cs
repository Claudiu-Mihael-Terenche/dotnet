using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day04TodosEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new TodoDbContext(); // Exceptions
                LvToDos.ItemsSource = Globals.dbContext.Todos.ToList(); // equivalent of SELECT * FROM people
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // FIXME: Handle DueDate being null gracefully, ensure helpful message is shown to the user
                if (DueDate.SelectedDate == null)
                {
                    throw new ArgumentException("Please select a due date");
                }
                Todo newToDo = new Todo(TaskInput.Text, (int)DifficultySlider.Value, (DateTime)DueDate.SelectedDate, (Todo.StatusEnum)StatusComboBox.SelectedIndex);  // ArgumentException
                Globals.dbContext.Todos.Add(newToDo);
                Globals.dbContext.SaveChanges(); // SystemException
                LvToDos.ItemsSource = Globals.dbContext.Todos.ToList();
                //clear inputs
                ResetFields();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Database error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Todo currSelectedTodo = LvToDos.SelectedItem as Todo;
            if (currSelectedTodo == null) return;
            try
            {
                currSelectedTodo.Task = TaskInput.Text; // ArgumentException
                currSelectedTodo.Difficulty = (int)DifficultySlider.Value; // ArgumentException
                currSelectedTodo.DueDate = (DateTime)DueDate.SelectedDate; // ArgumentException
                currSelectedTodo.Status = (Todo.StatusEnum)StatusComboBox.SelectedIndex; // ArgumentException
                Globals.dbContext.SaveChanges(); // SystemException
                LvToDos.ItemsSource = Globals.dbContext.Todos.ToList(); // SystemException
                ResetFields();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (SystemException ex)
            {
                MessageBox.Show(this, "Unable to access the database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Todo currSelectedTodo = LvToDos.SelectedItem as Todo;
            if (currSelectedTodo == null) return;
            var result = MessageBox.Show(this, "Are you sure you want to delete this item?", "Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            try
            {
                Globals.dbContext.Todos.Remove(currSelectedTodo);
                Globals.dbContext.SaveChanges(); // ex
                LvToDos.ItemsSource = Globals.dbContext.Todos.ToList(); // ex, equivalent of SELECT * FROM People
                ResetFields();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database:\n" + ex.Message, "Database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LvTodos_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Todo currSelectedTodo = LvToDos.SelectedItem as Todo;
            if (currSelectedTodo == null)
            {
                ResetFields();
            }
            else
            {
                BtnDelete.IsEnabled = true;
                BtnUpdate.IsEnabled = true;
                TaskInput.Text = currSelectedTodo.Task;
                DifficultySlider.Value = currSelectedTodo.Difficulty;
                DueDate.SelectedDate = currSelectedTodo.DueDate;
                StatusComboBox.SelectedIndex = (int)currSelectedTodo.Status;
            }
        }

        private void ResetFields()
        {
            TaskInput.Text = "";
            DifficultySlider.Value = 1;
            DueDate.SelectedDate = DateTime.Today;
            StatusComboBox.SelectedIndex = 0;
            BtnDelete.IsEnabled = false;
            BtnUpdate.IsEnabled = false;
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() != true) return; // cancelled
            //
            List<Todo> toDoExport = Globals.dbContext.Todos.ToList();
            List<string> lines = new List<string>();
            foreach (Todo p in toDoExport)
            {
                lines.Add($"{p.Task};{p.Difficulty};{p.DueDate};{p.Status}");
            }
            try
            {
                File.WriteAllLines(saveFileDialog.FileName, lines);
                MessageBox.Show(this, "Export complete!", "Export Status", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) when (ex is IOException || ex is SystemException)
            {
                MessageBox.Show(this, "Export failed\n" + ex.Message, "Export Status", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
