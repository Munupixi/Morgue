using Microsoft.EntityFrameworkCore;
using Morgue.Models;
using System.Windows;
using System.Windows.Controls;

namespace Morgue
{
    /// <summary>
    /// Interaction logic for ViewPage.xaml
    /// </summary>
    public partial class ViewPage : Page
    {
        private List<Request> requests;
        private List<Request> viewRequest = new List<Request>();

        public ViewPage()
        {
            InitializeComponent();
            MorgueContext morgueContext = new MorgueContext();
            StatusComboBox.ItemsSource = morgueContext.Statuses.Select(s => s.Title).ToList();
            RequestsCountLabel.Content = morgueContext.Requests.Count();
            if (User.ActiveUser.RoleId == 1)
            {
                requests = morgueContext.Requests
                    .Include(r => r.Status)
                    .ToList();
            }
            else
            {
                requests = morgueContext.Requests
                    .Where(r => r.ExecutorId == User.ActiveUser.UserId)
                    .Include(r => r.Status)
                    .ToList();
                CreateButton.IsEnabled = false;
            }
            UpdateView();
        }

        private void UpdateView()
        {
            viewRequest.Clear();
            foreach (Request request in requests)
            {
                if (request.Title.ToLower().Contains(SearchTextBox.Text.ToLower()) &&
                    (StatusComboBox.SelectedIndex + 1 == request.StatusId || StatusComboBox.SelectedIndex == -1))
                {
                    viewRequest.Add(request);
                }
            }
            ViewRequestsCountLabel.Content = viewRequest.Count();
            MainDataGrid.ItemsSource = null;
            MainDataGrid.ItemsSource = viewRequest;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Content = new ManipulationPage((sender as FrameworkElement).DataContext as Request);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateView();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Content = new ManipulationPage();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            StatusComboBox.SelectedIndex = -1;
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateView();
        }
    }
}