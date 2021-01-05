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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;



namespace thread_animali
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private double inizio;
        private double fine;
        private double altezza;

        Thread t1;
        Thread t2;
        Thread t3;
        Thread t4;
        private Stopwatch Cronometro1;
        private Stopwatch Cronometro2;
        private Stopwatch Cronometro3;
        private List<Tuple<string, double>> tempo;

        public MainWindow()
        {
            InitializeComponent();

            inizio = 10;
            fine = 750;
            altezza = 30;


            BitmapImage bitmap1 = new BitmapImage();
            bitmap1.BeginInit();
            bitmap1.UriSource = new Uri("img1.png", UriKind.RelativeOrAbsolute);
            bitmap1.EndInit();
            img_1.Source = bitmap1;
            img_1.Margin = new Thickness(inizio,altezza,0,0);
                        

            BitmapImage bitmap2 = new BitmapImage();
            bitmap2.BeginInit();
            bitmap2.UriSource = new Uri("img2.png", UriKind.RelativeOrAbsolute);
            bitmap2.EndInit();
            img_2.Source = bitmap2;
            img_2.Margin = new Thickness(inizio, altezza + 120, 0, 0);

            BitmapImage bitmap3 = new BitmapImage();
            bitmap3.BeginInit();
            bitmap3.UriSource = new Uri("img3.png", UriKind.RelativeOrAbsolute);
            bitmap3.EndInit();
            img_3.Source = bitmap3;
            img_3.Margin = new Thickness(inizio, altezza + 240, 0, 0);


            tempo = new List<Tuple<string, double>>();
            tempo.Add(new Tuple<string, double>("thread 1",0));
            tempo.Add(new Tuple<string, double>("thread 2", 0));
            tempo.Add(new Tuple<string, double>("thread 3", 0));

            t1 = new Thread(new ThreadStart(Metodo1));
            t2 = new Thread(new ThreadStart(Metodo2));
            t3 = new Thread(new ThreadStart(Metodo3));
            t4 = new Thread(new ThreadStart(Metodo4));

            Cronometro1 = new Stopwatch();
            Cronometro2 = new Stopwatch();
            Cronometro3 = new Stopwatch();



        }

       

        private  void Metodo1()
        {
            Cronometro1.Start();
            Sposta1();
           

        }

        private void Metodo2()
        {
            Cronometro2.Start();
            Sposta2();
            

        }

        private void Metodo3()
        {
            Cronometro3.Start();
            Sposta3();
           

        }

        private void Metodo4()
        {
            bool bl = true;
            int a = 0,b = 0,c = 0;
            while(bl)
            {
                

                if(t1.IsAlive == false && a == 0)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Cronometro1.Stop();
                        tempo[0] = new Tuple<string, double>(tempo[0].Item1, Cronometro1.Elapsed.TotalSeconds);
                        classifica_lbl.Content = classifica_lbl.Content + tempo[0].Item1 + ": " + tempo[0].Item2 + "\n";
                    }));
                    a++;
                }

                if (t2.IsAlive == false && b == 0)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Cronometro2.Stop();
                        tempo[1] = new Tuple<string, double> (tempo[1].Item1, Cronometro2.Elapsed.TotalSeconds);
                        classifica_lbl.Content = classifica_lbl.Content + tempo[1].Item1 + ": " + tempo[1].Item2 + "\n";
                    }));
                    b++;
                }
                
                if (t3.IsAlive == false && c == 0)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Cronometro3.Stop();
                        tempo[2] = new Tuple<string, double>(tempo[2].Item1, Cronometro3.Elapsed.TotalSeconds);
                        classifica_lbl.Content = classifica_lbl.Content + tempo[2].Item1 + ": " + tempo[2].Item2 + "\n";
                    }));
                    c++;
                }

                if (a+b+c == 3)
                    bl = false;
            }
        }

        private void Sposta1()
        {

            Random rnd = new Random();
            double ml = 10;


            while (ml < fine)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ml = img_1.Margin.Left;
                    int tmp = rnd.Next(10, 51);
                    img_1.Margin = new Thickness(ml + tmp, img_1.Margin.Top, 0, 0);

                }));
                Thread.Sleep(rnd.Next(500, 1001));

            }
        }

        private void Sposta2()
        {
            Random rnd = new Random();
            double ml = 10;

            while (ml < fine)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ml = img_2.Margin.Left;
                    int tmp = rnd.Next(10, 51);
                    img_2.Margin = new Thickness(ml + tmp, img_2.Margin.Top, 0, 0);

                }));
                Thread.Sleep(rnd.Next(500, 1001));

            }
        }

        private void Sposta3()
        {
            Random rnd = new Random();
            double ml = 10;

            while (ml < fine)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ml = img_3.Margin.Left;
                    int tmp = rnd.Next(10, 51);
                    img_3.Margin = new Thickness(ml + tmp, img_3.Margin.Top, 0, 0);

                }));
                Thread.Sleep(rnd.Next(500,1001));

            }
        }

        private void btn_init_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Start));
            t.Start();
        }

        private void Start()
        {            
            
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();

        }
    }
}
