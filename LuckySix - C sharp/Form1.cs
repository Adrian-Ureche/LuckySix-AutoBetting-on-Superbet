using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckySix___C_sharp
{
    public partial class Form1 : Form
    {
        private IWebDriver driver = new FirefoxDriver();

        public Form1()
        {
            InitializeComponent();
            //driver.Url = "https://seven-web-superbet.7platform.com/#/luckysix?token=3a3572b7-4f14-418c-b349-818a7a7300b4&id=1327516&auth=b2b&lang=ro";
            driver.Url = "https://superbet.ro/virtuale/luckysix";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                IWebElement src = this.driver.FindElement(By.XPath("//iframe[@id='seven-plugin-live']"));
                driver.Url = src.GetProperty("src");
            }
            catch
            {
                MessageBox.Show("Protectia nu a fost scoasa! Verificati daca in pagina din Mozila este deschis Lucky Six din pariuri virtuale. Daca nu este, intrati manual din Mozila pe Lucky Six (Virtuale) si nu uitati sa va autentificati contul.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IList<IWebElement> betting = this.driver.FindElements(By.XPath("//html[@class='n-page n-page-games']//div[@class='betting-normal-item']"));
                IWebElement adauga = this.driver.FindElement(By.XPath("//html[@class='n-page n-page-games']//a[@class='betting-controls-button n-button important']"));


                string line;
                string[] bet;
                Thread[] thrs = new Thread[48];

                for (int j = 0; j < 48; j++)
                    thrs[j] = new Thread(new ThreadStart(betting[j].Click));

                foreach (TreeNode tn in treeView1.Nodes)
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(@"Fisiere\" + tn.Text + ".txt");
                    while ((line = file.ReadLine()) != null)
                    {
                        bet = line.Split(',');
                        foreach (string b in bet)
                            if (b != "")
                            {
                                //thrs[Convert.ToInt32(b) - 1].Start();
                                //thrs[Convert.ToInt32(b) - 1] = new Thread(new ThreadStart(betting[Convert.ToInt32(b) - 1].Click));
                                betting[Convert.ToInt32(b) - 1].Click();
                            }
                        
                        if (panel2.Controls.Count != 0)
                            foreach (TextBox b in panel2.Controls)
                                if(!bet.Contains(b.Text))
                                    betting[Convert.ToInt32(b.Text) - 1].Click();
                        /*
                        bool status = true;
                        while (status)
                        {
                            bool flag = false;
                            for (int j = 0; j < 48; j++)
                                if (thrs[j].IsAlive)
                                {
                                    flag = true;
                                    break;
                                }
                            status = flag;
                        }
                        */   
                        adauga.Click();
                    }

                    file.Close();
                    
                    pariaza(textBox1.Text, textBox2.Text);
                    System.Threading.Thread.Sleep(4800);
                }

                foreach (TreeNode tn in treeView2.Nodes)
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(@"Fisiere\" + tn.Text + ".txt");
                    while ((line = file.ReadLine()) != null)
                    {
                        bet = line.Split(',');
                        foreach (string b in bet)
                            if (b != "")
                            {
                                //thrs[Convert.ToInt32(b) - 1].Start();
                                //thrs[Convert.ToInt32(b) - 1] = new Thread(new ThreadStart(betting[Convert.ToInt32(b) - 1].Click));
                                betting[Convert.ToInt32(b) - 1].Click();
                            }

                        if (panel2.Controls.Count != 0)
                            foreach (TextBox b in panel2.Controls)
                                if (!bet.Contains(b.Text))
                                    betting[Convert.ToInt32(b.Text) - 1].Click();
                        /*
                        bool status = true;
                        while (status)
                        {
                            bool flag = false;
                            for (int j = 0; j < 48; j++)
                                if (thrs[j].IsAlive)
                                {
                                    flag = true;
                                    break;
                                }
                            status = flag;
                        }
                        */
                        adauga.Click();
                    }

                    file.Close();

                    pariaza(textBox5.Text, textBox6.Text);
                    System.Threading.Thread.Sleep(4800);
                }
            }
            catch
            {
                MessageBox.Show("Nu se pot plasa biletele. Verificati daca ati apasat butonul Refresh si ca v-ati autentificat contul.");
            }

        }


        private void pariaza(string urm, string miz)
        {
            try
            {
                IWebElement urmatoarele = this.driver.FindElement(By.XPath("//html[@class='n-page n-page-games']//input[@ng-model='ticketApi.ticket.future.value']"));
                urmatoarele.Click();
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.5);
                urmatoarele.SendKeys(urm);
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.5);

                IWebElement miza = this.driver.FindElement(By.XPath("//html[@class='n-page n-page-games']//input[@placeholder='Miză']"));
                miza.Click();
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.5);
                miza.SendKeys(miz);
               // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.5);

                System.Threading.Thread.Sleep(1000);
                IWebElement pariaza = this.driver.FindElement(By.XPath("//span[@ng-bind='::config.messages.general.payInTicket']"));
                pariaza.Click();
            }
            catch 
            {
                MessageBox.Show("Nu se pot plasa biletele. Verificati daca ati apasat butonul Refresh si ca v-ati autentificat contul.");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GenerateTexBox(6);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            GenerateTexBox(7);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            GenerateTexBox(8);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            GenerateTexBox(9);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            GenerateTexBox(10);
        }

        void GenerateTexBox(int n)
        {
            try
            {
                int pointX = 0;
                panel1.Controls.Clear();
                for (int i = 0; i < n; i++)
                {
                    TextBox a = new TextBox();
                    a.Location = new Point(pointX, 0);
                    a.AutoSize = false;
                    a.Size = new System.Drawing.Size(35, 25);
                    a.Font = new Font("Times New Roman", 12.0f);
                    panel1.Controls.Add(a);
                    panel1.Show();
                    pointX += 37;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string solution = "";
            foreach (TextBox a in panel1.Controls)
                solution += a.Text + ",";

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"Fisiere\" + comboBox1.Text + ".txt", true))
            {
                file.WriteLine(solution);
            }

            listView1.Items.Clear();
            if (File.Exists(@"Fisiere\" + comboBox1.Text + ".txt"))
                loadSolutions(@"Fisiere\" + comboBox1.Text + ".txt");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = comboBox1.Text;
            listView1.Items.Clear();
            if (File.Exists(@"Fisiere\" + comboBox1.Text + ".txt"))
                loadSolutions(@"Fisiere\" + comboBox1.Text + ".txt");
        }

        void loadSolutions(string path)
        {
            int counter = 0;
            string line;
            string[] solution;

            System.IO.StreamReader file =
                new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                solution = line.Split(',');
                var listViewItem = new ListViewItem(solution);
                listView1.Items.Add(listViewItem);
                counter++;
            }

            label2.Text = counter.ToString();
            file.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int nrLineRemoved = 0;
            if (listView1.SelectedItems != null)
                for (int i = 0; i < listView1.Items.Count; i++)
                    if (listView1.Items[i].Selected)
                    {
                        nrLineRemoved = i;
                        listView1.Items[i].Remove();
                        i--;
                    }
            label2.Text = (Convert.ToInt32(label2.Text) - 1).ToString();

            string tempFile = Path.GetTempFileName();

            int contor = 0;
            using (var sr = new StreamReader(@"Fisiere\" + comboBox1.Text + ".txt"))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (contor != nrLineRemoved)
                        sw.WriteLine(line);
                    contor++;
                }
            }
            
            File.Delete(@"Fisiere\" + comboBox1.Text + ".txt");
            File.Move(tempFile, @"Fisiere\" + comboBox1.Text + ".txt");
        }
        

        private void button5_Click(object sender, EventArgs e)
        {
            TreeNode newNode = new TreeNode(comboBox1.Text);
            treeView1.Nodes.Add(newNode);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pointX = 0;
                panel2.Controls.Clear();
                for (int i = 0; i < Convert.ToInt16(comboBox2.Text); i++)
                {
                    TextBox a = new TextBox();
                    a.Location = new Point(pointX, 0);
                    a.AutoSize = false;
                    a.Size = new System.Drawing.Size(35, 25);
                    a.Font = new Font("Times New Roman", 12.0f);
                    panel2.Controls.Add(a);
                    panel2.Show();
                    pointX += 37;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                IList<IWebElement> betting = this.driver.FindElements(By.XPath("//html[@class='n-page n-page-games']//div[@class='betting-normal-item']"));
                IWebElement adauga = this.driver.FindElement(By.XPath("//html[@class='n-page n-page-games']//a[@class='betting-controls-button n-button important']"));


                string line;
                string[] bet;

                foreach (TreeNode tn in treeView1.Nodes)
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(@"Fisiere\" + tn.Text + ".txt");
                    while ((line = file.ReadLine()) != null)
                    {
                        bet = line.Split(',');
                        foreach (string b in bet)
                            if (b != "")
                                betting[Convert.ToInt32(b) - 1].Click();

                        if (panel2.Controls.Count != 0)
                            foreach (TextBox b in panel2.Controls)
                                betting[Convert.ToInt32(b.Text) - 1].Click();

                        adauga.Click();
                    }

                    file.Close();

                    //pariaza();
                    System.Threading.Thread.Sleep(4800);
                }
            }
            catch
            {
                MessageBox.Show("Nu se pot plasa biletele. Verificati daca ati apasat butonul Refresh si ca v-ati autentificat contul.");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            switch (textBox3.Text)
            {
                case "7":
                    generare10Aleatoriu(7);
                    break;
                case "8":
                    generare10Aleatoriu(8);
                    break;
                case "9":
                    generare10Aleatoriu(9);
                    break;
                case "10":
                    generare10Aleatoriu(10);
                    break;
                default:
                    generare10Aleatoriu(6);
                    break;
            }
        }

        private void generare10Aleatoriu(int n)
        {
            var rand = new Random();
            for (int j = 0; j < 10; j++)
            {
                List<int> listNumbers = new List<int>();
                int number;
                int cnt = 0;

                if(textBox4.Text.Length != 0)
                    foreach (string s in textBox4.Text.Split(','))
                        cnt++;

                if(textBox4.Text.Length != 0)
                    foreach (string s in textBox4.Text.Split(','))
                        listNumbers.Add(Convert.ToInt32(s));

                for (int i = 0; i < n - cnt; i++)
                {
                    do
                    {
                        number = rand.Next(1, 48);
                    } while (listNumbers.Contains(number));
                    listNumbers.Add(number);
                }

                string solution = "";
                foreach (int b in listNumbers)
                    solution += b.ToString() + ",";


                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"Fisiere\" + comboBox1.Text + ".txt", true))
                {
                    file.WriteLine(solution);
                }

                listView1.Items.Clear();
                if (File.Exists(@"Fisiere\" + comboBox1.Text + ".txt"))
                    loadSolutions(@"Fisiere\" + comboBox1.Text + ".txt");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            treeView2.Nodes.Remove(treeView2.SelectedNode);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TreeNode newNode = new TreeNode(comboBox1.Text);
            treeView2.Nodes.Add(newNode);
        }
    }

}
