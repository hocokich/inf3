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

namespace inf3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string dec;
        public string bin;
        public MainWindow()
        {
            InitializeComponent();
        }

        //Для числа в массив
        static string arrtostr(int[] a)
        {
            string s = "";
            foreach (int i in a)
            {
                s = s + i;
            }
            return s;
        }
        static int[] dual10v2(int n)
        {
            int[] a = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 15; i >= 0; i--)
            {
                a[i] = n & 1;
                n = n >> 1;
            }
            return a;
        }
        static int[] _10v2(int n)
        {
            int[] a = { 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 7; i >= 0; i--)
            {
                a[i] = n & 1;
                n = n >> 1;
            }
            return a;
        }



        //Для массива в число
        static int[] strtoarr(string s)
        {
            int[] a = new int[s.Length];
            for (int i = s.Length - 1; i >= 0; i--)
            {
                a[i] = int.Parse(char.ToString(s[i]));
            }
            return a;
        }
        static int _2v10(int[] a)
        {
            int n = 0;
            for (int i = 7; i >= 0; i--)
            {
                n = (int)(n + (a[i] * Math.Pow(2, (7 - i))));
            }

            return n;
        }
        static float mant2v10(int[] a)
        {
            float n = 0;
            for (int i = 23; i >= 0; i--)
            {
                n = (float)(n + (a[i] * Math.Pow(2, -i - 1)));
            }
            return n;
        }
        static float ce2v10(int[] a)
        {
            float n = 0;
            for (int i = 23; i >= 0; i--)
            {
                n = (float)(n + (a[i] * Math.Pow(2, (23 - i))));
            }
            return n;
        }



        void ToDec_Click(object sender, RoutedEventArgs e)
        {
            //первый символ выбирается для знака
            string znak = Num.Text.Substring(0, 1);

            //со второго по 8 берутся символы, которые относятся к степени
            string ex = Num.Text.Substring(1, 8);

            //остальное это мантиса
            string mant = Num.Text.Substring(9);
            int exp = _2v10(strtoarr(ex)) - 127;
            string c = "1";
            float ce;
            float d;

            //если степень больше положительная
            if (exp >= 0)
            {
                //цикл отделяет целую часть от дробной
                for (int i = 0; i < exp; i++)
                {
                    c += mant.Substring(0, 1);
                    mant = mant.Remove(0, 1);
                    mant += "0";
                }
                while (mant.Length < 24) mant += "0";
                while (c.Length < 24) c = "0" + c;
                d = mant2v10(strtoarr(mant));
                ce = ce2v10(strtoarr(c));
            }
            //дальше в случае если степень отрицательная
            else
            {
                mant = "1" + mant;
                exp += 1;
                c = "";
                d = mant2v10(strtoarr(mant)) * (float)Math.Pow(2, exp);
                ce = 0;
            }
            ce += d;
            ce *= (float)Math.Pow(-1, int.Parse(znak));

            Result.Text = "";
            Result.Text = ce.ToString();
        }

        void ToBin_Click(object sender, RoutedEventArgs e)
        {
            //записывает число в виде дробного
            double a = double.Parse(Num.Text);

            //здесь определяем знак числа
            string znak = a < 0 ? "1" : "0";
            a = Math.Abs(a);

            //записывает только целую часть
            string c = arrtostr(dual10v2((int)a));

            //убирает лишние 0
            while (c.Length != 0 && c[0] == '0')
            {
                c = c.Remove(0, 1);
            }

            //оставляет дробную часть
            a = Math.Abs(a - (int)a);
            string b = "";

            //переводит дробную часть в двоичную сс (наверное)
            for (int i = 0; i < 24; i++)
            {
                a *= 2;
                if (a > 1)
                {
                    b += "1";
                    a -= 1;
                }
                else b += "0";
            }

            //это на тот случай, если чисел в верхнем цикле не хватит
            string zapas = "";
            for (int i = 0; i < 24; i++)
            {
                a *= 2;
                if (a > 1)
                {
                    zapas += "1";
                    a -= 1;
                }
                else zapas += "0";
            }
            string mant = "";

            //это степень
            int p = 0;

            //проверка какаого вида число
            //это выполняется, если оно больше 0
            if (c != "")
            {
                //убирается единица, которая не пишится в числе
                c = c.Remove(0, 1);
                mant = c + b;
                p = c.Length;
                if (mant.Length > 23) mant = mant.Remove(23);
            }

            //это если меньше
            else
            {
                while (b[0] == '0')
                {
                    //убирается единица, которая не пишится в числе
                    b = b.Remove(0, 1);
                    p -= 1;
                }
                b = b.Remove(0, 1);
                p -= 1;
                mant = b + zapas;
                if (mant.Length > 23) mant = mant.Remove(23);
            }
            //степень переводится в двоичную
            string ex = arrtostr(_10v2(p + 127));

            //записывается число, где znak - это, что логично знак, ех - это степень, и остальное мантиса
            Result.Text += (znak + ex + mant);
            Sign.Text = znak;
            Exp.Text = ex;
            Mantissa.Text = mant;




        }

        private void Num_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Result_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Sign_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Exp_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Mantissa_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
