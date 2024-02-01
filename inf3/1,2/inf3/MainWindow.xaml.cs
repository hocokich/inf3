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

        private static float StrToFloat(string strdec )
        {
            float dec = float.Parse(strdec);
            return dec;
        }

        private static byte[] GetBytes(float dec )
        {
            byte[] tmp = BitConverter.GetBytes(dec);
            return tmp;
        }

        string BytesToStr(byte[] tmp )
        {
            string cifrii = "";
            for (byte num = 0; num < tmp.Length; num++)
            {
                cifrii += "[" + tmp[num] + "]";
            }

            string str1 = "GetBytes result ->" + cifrii + "\n";
            string str2 = "3 byte[" + tmp[3] + "] ->" + BinToStr(IntToBin(tmp[3])) + "\n";
            string str3 = "2 byte[" + tmp[2] + "] ->" + BinToStr(IntToBin(tmp[2])) + "\n";
            string str4 = "1 byte[" + tmp[1] + "] ->" + BinToStr(IntToBin(tmp[1])) + "\n";
            string str5 = "0 byte[" + tmp[0] + "] ->" + BinToStr(IntToBin(tmp[0])) + "\n";
            string bits = BinToStr(IntToBin(tmp[3])) + " " + BinToStr(IntToBin(tmp[2])) + " " + BinToStr(IntToBin(tmp[1])) + " " + BinToStr(IntToBin(tmp[0]));
            string str6 = "result ->" + bits;

            bin = bits;

            string str = str1 + str2 + str3 + str4 + str5 + str6;

            return str;
        }


        //Это для числа в массив
        static int[] IntToBin(int n)// преобразование числа в массив
        {
            int n1 = n;//создаем вторую переменную чтобы не потерять минус
            if (n1 < 0) n1 = n1 * -1;
            int[] bin = new int[8];
            for (int i = 7; i >= 0; i--)
            {
                bin[i] = n1 % 2;//записываем остаток от деления на 2 
                n1 = n1 / 2;//целочисленное деление на 2
            }
            if (n < 0) bin = AddOne(Invers(bin));
            return bin;
        }
        static int[] Invers(int[] n) //функция инвертирования значений
        {
            for (int i = 7; i >= 0; i--)
            {
                if (n[i] == 0) n[i] = 1;
                else n[i] = 0;
            }
            return n;
        }
        static int[] AddOne(int[] n) //функция добавления единицы к числу
        {
            for (int i = 7; i >= 0; i--)
            {
                if (n[i] == 1) n[i] = 0;// -5^10 != 1111 101[0]^2 != [1]
                else                    // ||
                {                       // \/
                    n[i] = 1;           // 1111 101[1] = 1
                    break; //прерываем цикл отправляем на золото
                }
            }
            return n;
        }


        //Это для массива в число
        static int BinToInt(int[] n) //преобразование массива целых чисел в целое число
        {
            int[] cloneN = n;
            int num = BinToDecimal(cloneN);

            if (num > 127) // для отрицательного числа
            {
                num = BinToDecimal(Inversion(cloneN)); // n - 1
                num++;                         // n - 1 + 1  =  n
                num *= -1;                     // n * -1 = -n
            }
            return num;
        }
        static int BinToDecimal(int[] n)// алгоритм преобразования числа
        {
            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                sum += n[i] * (int)(Math.Pow(2, 8 - i - 1));
            }
            return sum;
        }
        static int[] Inversion(int[] n) //функция инвертирования значений
        {
            for (int i = 7; i >= 0; i--)
            {
                if (n[i] == 0) n[i] = 1;
                else n[i] = 0;
            }
            return n;
        }



        static string BinToStr(int[] n) //преобразуем массив в строку
        {
            string str = String.Join("", n); //https://learn.microsoft.com/ru-ru/dotnet/api/system.string.join?view=net-7.0
            return str;
        }
        static int[] StrToBin(string n)//преобразовываем строку в массив
        {
            int[] bin = new int[32];
            for (int i = 0; i < 32; i++)
            {
                 bin[i] = int.Parse(Char.ToString(n[i]));
            }
            return bin;
        }

        string BinToFloat(int[] n)
        {
            int[] prikol0 = new int[8];
            int[] prikol1 = new int[8];
            int[] prikol2 = new int[8];
            int[] prikol3 = new int[8];

            for(int i = 0; i < 8; i++)
            {
                prikol3[i] = n[i];
            }

            for (int i = 0; i< 8; i++)
            {
                prikol2[i] = n[i+8];
            }

            for (int i = 0; i < 8; i++)
            {
                prikol1[i] = n[i + 16];
            }

            for (int i = 0; i < 8; i++)
            {
                prikol0[i] = n[i + 24];
            }

            int num0 = BinToInt(prikol0);
            int num1 = BinToInt(prikol1);
            int num2 = BinToInt(prikol2);
            int num3 = BinToInt(prikol3);

            byte[] tmp = BitConverter.GetBytes(10);

            tmp[0] = (byte)num0;
            tmp[1] = (byte)num1;
            tmp[2] = (byte)num2;
            tmp[3] = (byte)num3;

            string str1 = "string ->" + BinToStr(IntToBin(tmp[3])) + " " + BinToStr(IntToBin(tmp[2])) + " " + BinToStr(IntToBin(tmp[1])) + " " + BinToStr(IntToBin(tmp[0])) + "\n";
            string str2 = "3 byte[" + BinToStr(IntToBin(tmp[3])) + "]-> " + tmp[3] + "\n";
            string str3 = "2 byte[" + BinToStr(IntToBin(tmp[2])) + "]-> " + tmp[2] + "\n";
            string str4 = "1 byte[" + BinToStr(IntToBin(tmp[1])) + "]-> " + tmp[1] + "\n";
            string str5 = "0 byte[" + BinToStr(IntToBin(tmp[0])) + "]-> " + tmp[0] + "\n";
            string str6 = "result -> [" + tmp[0] + "]" + "[" + tmp[1] + "]" + "[" + tmp[2] + "]" + "[" + tmp[3] + "]" + "\n";
            float f = BitConverter.ToSingle(tmp, 0);
            string str7 = "[" + tmp[0] + "]" + "[" + tmp[1] + "]" + "[" + tmp[2] + "]" + "[" + tmp[3] + "]-> ToSingle -> " + f;

            dec = f.ToString();

            string str = str1 + str2 + str3 + str4 + str5 + str6 + str7;

            return str;
        }

        private void Dec_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Bin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Do_Click(object sender, RoutedEventArgs e)//Dec to bin
        {
            dec = Dec.Text;
            Plan.Text = BytesToStr(GetBytes(StrToFloat(dec)));
            Bin.Text = bin;

        }

        private void Undo_Click(object sender, RoutedEventArgs e)//Bin to dec
        {
            bin = Bin.Text;
            Plan.Text = BinToFloat(StrToBin(bin));
            Dec.Text = dec;
        }

        private void Plan_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
