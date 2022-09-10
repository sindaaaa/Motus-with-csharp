using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Motus
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MotusGameDBEntities mg = new MotusGameDBEntities();

        Player p = new Player();
        Dictionnary d = new Dictionnary();
        public Login()
        {
            InitializeComponent();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void btn__quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {/////Button load
            if((loadPlayer.Text=="")  & (newPlayer.Text == ""))
            {
                MessageBox.Show("Please enter your name!!!");
            }
            else if (chercher(loadPlayer.Text)=="")
            {
                
                MessageBox.Show("Player name does not exist!!! You can add new player name!!!!");
                loadPlayer.Text = "";
            }
            else
            {
                GameInterface gm = new GameInterface();
                gm.Show();
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {////button new
            if ((loadPlayer.Text == "") & (newPlayer.Text == ""))
            {
                MessageBox.Show("Please enter your name!!!");
            }
            else if(chercher(newPlayer.Text) !="")
            {
                MessageBox.Show("This name already exist!!! Please add an other name!!!!!");
                newPlayer.Text = "";
            }
            else if (chercher(newPlayer.Text) == "")
            {
                Player p1 = new Player();
                p1.PlayerName = newPlayer.Text;
                p1.Score = 0;
                mg.Players.Add(p1);
                newPlayer.Text = "";
                GameInterface gm1 = new GameInterface();
                gm1.Show();
                this.Close();
            }
            else
            {
                GameInterface gm1 = new GameInterface();
                gm1.Show();
                this.Close();
            }

        }
        private string chercher(string name)
        {
            var query = from n in mg.Players
                        where n.PlayerName == name
                        select new
                        {
                            id = n.Id,
                        };
            int playerId = query.Count();
            if (playerId == 0)
            {
                return "";
            }
            else
            {
                return name;
            }
            
            
           
        }

        private void ReturnToMain(object sender, RoutedEventArgs e)
        {
            MainWindow mm = new MainWindow();
            mm.Show();
            this.Close();

        }
        private void InsertIntoDB(string name)
        {
            MotusGameDBEntities mg1 = new MotusGameDBEntities();
            Player p1 = new Player();
            
            p1.PlayerName = name;
            p1.Score = 0;
            
            mg1.SaveChanges();
        }
    }
}
