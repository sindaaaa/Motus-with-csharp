using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using Motus;
using System.Windows.Media.Effects;

namespace Motus
{
    /// <summary>
    /// Logique d'interaction pour GameInterface.xaml
    /// </summary>
    public partial class GameInterface : Window

    {
        int cpt=0;
        string secretWord = extraireMot();
        List<Label> gridGame = new List<Label>();
        MotusGameDBEntities mg = new MotusGameDBEntities();
        Player p = new Player();
        Dictionnary d = new Dictionnary();
        public GameInterface()
        {
            InitializeComponent();
            remplir(gridGame);
            
        }
        private void btn__quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string ch = Answer.Text;
            if (ch.Length != 5)
            {
                MessageBox.Show("Please enter a word of length 5 !!!");
                Answer.Text = "";
            }
            else 
            {
                if(cpt==35)
                {
                    MessageBox.Show("You exceeded your attempts!!!");
                }
                else 
                {
                    for (int i = cpt; i < cpt + 5; i++)
                    {
                        gridGame[i].Content = ch[i - cpt];
                        if (exist(ch[i - cpt], secretWord) & verifyChar(ch[i - cpt], secretWord, i - cpt))
                        {
                            SolidColorBrush correct = new SolidColorBrush();
                            correct.Color = Colors.Green;
                            gridGame[i].Background = correct;
                        }
                        else if (exist(ch[i - cpt], secretWord) & !verifyChar(ch[i - cpt], secretWord, i - cpt))
                        {
                            SolidColorBrush correct = new SolidColorBrush();
                            correct.Color = Colors.Yellow;
                            gridGame[i].Background = correct;
                        }
                    }
                    Answer.Text = "";
                    cpt += 5;
                    if(secretWord==ch)
                    {
                        SolidColorBrush correct = new SolidColorBrush();
                        correct.Color = Colors.GhostWhite;
                        help.Background = correct;
                        help.FontSize = 18;
                        help.Content = "Congratulations !!! You win \n \n"
                            + "Your score is:     "
                            + (70-cpt * 2);
                    }
                }
                
            }
            


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();

            mw.Show();
            this.Close();
        }
        public void remplir(List<Label> L)
        {
            L.Add(Label00);
            L.Add(Label01);
            L.Add(Label02);
            L.Add(Label03);
            L.Add(Label04);
            L.Add(Label10);
            L.Add(Label11);
            L.Add(Label12);
            L.Add(Label13);
            L.Add(Label14);
            L.Add(Label20);
            L.Add(Label21);
            L.Add(Label22);
            L.Add(Label23);
            L.Add(Label24);
            L.Add(Label30);
            L.Add(Label31);
            L.Add(Label32);
            L.Add(Label33);
            L.Add(Label34);
            L.Add(Label40);
            L.Add(Label41);
            L.Add(Label42);
            L.Add(Label43);
            L.Add(Label44);
            L.Add(Label50);
            L.Add(Label51);
            L.Add(Label52);
            L.Add(Label53);
            L.Add(Label54);
            L.Add(Label60);
            L.Add(Label61);
            L.Add(Label62);
            L.Add(Label63);
            L.Add(Label64);
        }
        public static string extraireMot()
        {
            Random aleatoire = new Random();
            int number = aleatoire.Next(5)+1;
            MotusGameDBEntities mg = new MotusGameDBEntities();
            var query = from mot in mg.Dictionnaries
                        where mot.Id == number
                        select new
                        {
                            motSecret = mot.Word,
                        };
            string word = query.First().motSecret.ToString();
            return word;
        }
        public bool exist(char c, string ch)
        {
            bool t = false;
            for (int i = 0; i < ch.Length; i++)
            {
                if (c == ch[i])
                {
                    t = true;

                }
            }
            return t;
        }
        public bool verifyChar(char c,string wordSec,int ind )
        {
            if (wordSec[ind] == c) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            
            SolidColorBrush correct = new SolidColorBrush();
            correct.Color = Colors.GhostWhite;
            help.Background = correct;
            help.FontSize = 18;
            help.Content = "The secret word is:  " + secretWord;


        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            var query = from mot in mg.Dictionnaries
                        where mot.Word == secretWord
                        select new
                        {
                            motSecret = mot.Hint,
                        };
            
            MessageBox.Show( "HELP!!!   \n\n"+query.First().motSecret.ToString());
        }
    }
}
