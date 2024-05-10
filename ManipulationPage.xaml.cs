using Morgue.Models;
using System.Windows;
using System.Windows.Controls;

namespace Morgue
{
    public partial class ManipulationPage : Page
    {
        public ManipulationPage()
        {
            InitializeComponent();
            SetUp();
            MorgueContext morgueContext = new MorgueContext();
            if (morgueContext.Requests.Count() == 0)
                RequestId.Text = "1";
            else
                RequestId.Text = (morgueContext.Requests.Max(r => r.RequestId) + 1).ToString();
        }

        public ManipulationPage(Request request)
        {
            InitializeComponent();
            SetUp();
            if (User.ActiveUser.RoleId == 2)
                CreationDate.IsEnabled = ExecutorComboBox.IsEnabled = TitleTextBox.IsEnabled = true;

            RequestId.Text = request.RequestId.ToString();
            CreationDate.Text = request.CreationDate.ToString();
            StatusComboBox.SelectedIndex = request.StatusId - 1;
            ExecutorComboBox.SelectedItem = request.ExecutorId;
            TitleTextBox.Text = request.Title;
        }

        private void SetUp()
        {
            MorgueContext morgueContext = new MorgueContext();
            StatusComboBox.ItemsSource = morgueContext.Statuses.Select(s => s.Title).ToList();
            ExecutorComboBox.ItemsSource = morgueContext.Users.Where(u => u.RoleId == 2).Select(u => u.UserId).ToList();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatusComboBox.SelectedIndex != -1 && TitleTextBox.Text.Length < 100 && DateOnly.TryParse(CreationDate.Text, out _))
            {
                MorgueContext morgueContext = new MorgueContext();
                Request? request = morgueContext.Requests.FirstOrDefault(r => r.RequestId == Convert.ToInt32(RequestId.Text));
                if (request != null)
                {
                    morgueContext.Requests.Remove(request);
                }
                request = new Request(Convert.ToInt32(RequestId.Text),
                    StatusComboBox.SelectedIndex + 1,
                    ExecutorComboBox.SelectedItem != null ? Convert.ToInt32(ExecutorComboBox.SelectedItem.ToString()) : null,
                    DateOnly.Parse(CreationDate.Text),
                    TitleTextBox.Text);
                morgueContext.Requests.Add(request);
                morgueContext.SaveChanges();
                MainWindow.Frame.Content = new ViewPage();
            }
            else
            {
                MessageBox.Show("Данные введены с ошибками!");
            }
        }

        private void ResetExecutorButton_Click(object sender, RoutedEventArgs e)
        {
            ExecutorComboBox.SelectedIndex = -1;
        }
    }
}