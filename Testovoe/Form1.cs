using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;


namespace Testovoe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int count = 0;
        public void ChooseFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            ChooseFolder();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Счетчик
            count++;

            //забираем инфу из формы
            int maxNumbOfWords = (int)numericUpDown2.Value;
            int minNumbOfWords = (int)numericUpDown3.Value;
            int numbOfFiles = (int)numericUpDown1.Value;
            //рандом 0.0
            Random myRandom = new Random();
            //определяем, сколько все таки файлов у нас будет. До конца рандом не доходит, поэтому +1
            int N = myRandom.Next(minNumbOfWords, maxNumbOfWords + 1); 

            //путь к файлу со словами
            string fileName = @"..\..\Resources\RUS.txt";

            //строка, которая хранит все наши слова. Говнокод? Да! Я и не спорю)))
            string[] myWords;
            string signs = ".,''?!-;:";


            try
            {
                //Создание файлов
                for (int i = 0; i < numbOfFiles; i++)
                {

                    //Создание части имени. Попытка сделать его уникальным
                    string name = Convert.ToChar(myRandom.Next(65, 91)).ToString();
                    for (int j = 0; j < 10; j++)
                    {

                        name += Convert.ToChar(myRandom.Next(97, 122));
                    }
                    //полный путь
                    string path = textBox1.Text + @"/" + count + "_" + name + i;
                    string myString = "";

                    //пробовала .docx, создает, но открыть не получается, поврежденнное содержимое пишет ((
                    FileInfo fi1 = new FileInfo(path );


                    if (!fi1.Exists)
                    {

                        //записть в файл
                        using (StreamWriter sw = fi1.CreateText())
                        {
                            if (System.IO.File.Exists(fileName))
                            {
                                myWords = System.IO.File.ReadAllLines(fileName, Encoding.Default);

                                //Рандаомные слова из файла
                                for (int j = 0; j < N; j++)
                                {
                                    int index = myRandom.Next(0, myWords.Length);


                                    //Время страдать со знаками препинания. 


                                    myString += myWords[index];

                                    int mySign = myRandom.Next(0, signs.Length + 11);

                                    if (mySign > 10)
                                    {
                                        myString += signs[myRandom.Next(0, signs.Length)];
                                    }
                                    myString += " ";

                                }
                                sw.Write(myString);
                            }
                            else
                            {
                                throw new Exception("Проблемы с созданием файлов. Извините ((");
                            }


                        }
                    }

                }

                System.Diagnostics.Process.Start("explorer", textBox1.Text);



            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }



        //максимум всегда больше минимума. Логично же?)
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Minimum = numericUpDown3.Value;
        }
    }
}

