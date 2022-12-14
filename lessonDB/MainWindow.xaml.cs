using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace lessonDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int idUser;
        public MainWindow()
        {
            InitializeComponent();

            get_json();
            Helper.entities = new lesson_dbEntities();

            dataGrid.ItemsSource = Helper.entities.User.ToList();
        }

        private void DataGridRow_Selected(object sender, RoutedEventArgs e)
        {
            var dgrow = (DataGridRow)sender;
            var context = dgrow.DataContext as User;

            idUser = context.id;

            tbID.Text = context.id.ToString();
            tbEmail.Text = context.email;
            tbPassword.Text = context.password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = Helper.entities.User.FirstOrDefault(x => x.id == idUser);
            user.email = tbEmail.Text;
            user.password = tbPassword.Text;

            Helper.entities.SaveChanges();

            refresh_grid();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var user = Helper.entities.User.FirstOrDefault(x => x.id == idUser);
            Helper.entities.User.Remove(user);

            Helper.entities.SaveChanges();

            refresh_grid();
        }

        private void refresh_grid() {
            //dataGrid.Items.Refresh();
            dataGrid.ItemsSource = Helper.entities.User.ToList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.email = tbNewEmail.Text;
            user.password = tbNewPassword.Text;
            user.birthdate = datePickerBD.SelectedDate;
            user.idRole = Convert.ToInt32(tbNewRole.Text);

            Helper.entities.User.Add(user);
            Helper.entities.SaveChanges();

            refresh_grid();
        }

        private async void get_json() 
        {
            string url = "https://jsonplaceholder.typicode.com/comments";
            HttpClient http = new HttpClient();

            var responce = await http.GetAsync(url);
            var responcecontent = await responce.Content.ReadAsStringAsync();

            if (responce.StatusCode == System.Net.HttpStatusCode.OK) { 
                List<CommentsModel> commentsModels = JsonConvert.DeserializeObject<List<CommentsModel>>(responcecontent);
                apiDataGrid.ItemsSource = commentsModels;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            //openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            //            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            //            "Portable Network Graphic (*.png)|*.png";
            openFileDialog.ShowDialog();

            string filepath = openFileDialog.FileName;
            string filename = System.IO.Path.GetFileName(filepath);
            string fullpath = System.IO.Path.GetFullPath("..\\Images\\") + filename;

            Helper.entities.User.FirstOrDefault(x => x.id == idUser).image = "..\\Images\\" + filename;
            Helper.entities.SaveChanges();
            refresh_grid();
            System.IO.File.Copy(filepath, fullpath, true);

            //var img = new Image();
            //img.Source = new BitmapImage(new Uri(fullpath));
            //myPanel.Children.Add(img);

        }
    }
}
