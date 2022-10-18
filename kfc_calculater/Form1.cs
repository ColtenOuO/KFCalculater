using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kfc_calculater
{
    public partial class Form1 : Form
    {
        public static int[,] dp = new int[105,1005];
        public static int[] w = new int[1005];
        public static int[] v = new int[1005];
        public static string[] id = new string[1005];
        public static int cnt = 1;
        public static int[] last = new int[1005];
        public void init()
        {
            for(int i=1;i<=100;i++)
            {
                for (int k = 0; k <= 1000; k++) dp[i, k] = 0;
            }
            richTextBox1.Text = "";
            cnt = 1;

            return;
        }
        public static void add(string s,int price,int happy)
        {
            id[cnt] = s;
            w[cnt] = price;
            v[cnt] = happy;
            cnt++;
            return;
        }
        public static bool check(string s)
        {
            for(int i=0;i<s.Length;i++)
            {
                if (s[i] >= '0' && s[i] <= '9') continue;
                return false;
            }

            return true;
        }
        public void dynamic_programming(int num)
        {
            for(int i=1;i<cnt;i++)
            {
                for(int k=w[i];k<=num;k++)
                {
                    if (dp[i, k] < dp[i - 1, k - w[i]] + v[i]) dp[i, k] = dp[i - 1, k - w[i]] + v[i];
                    else dp[i, k] = dp[i, k-1];
                }
            }

            string ans = "";
            int now = cnt - 1, have = dp[cnt - 1, num] , money = num;
            ans += "選擇以下方案可以帶來的總總足度是 " + dp[cnt - 1, num].ToString() + "\n";
            while( now != 0 )
            {
                for(int i=money;i>=w[now];i--)
                {
                    if( dp[now-1,i-w[now]] + v[now] == have )
                    {
                        ans += id[now];
                        ans += "\n";
                        have -= v[now];
                        money = i - w[now];
                        break;
                    }
                }

                now--;
            }

            richTextBox1.Text = ans;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // 開始計算
        {
            string have = textBox12.Text;
            bool ok7 = check(have);

            if( ok7 == false )
            {
                richTextBox2.Text = "預算輸入有誤";
            }
            else
            {
                richTextBox2.Text = "";
                dynamic_programming(Convert.ToInt32(have));
            }
        }

        private void button2_Click(object sender, EventArgs e) // 清除資料
        {
            init();
            richTextBox2.Text = "請輸入資訊";
        }

        private void button3_Click(object sender, EventArgs e) // 新增資料
        {
            string s1 = textBox1.Text, s2 = textBox2.Text, s3 = textBox3.Text , s4 = textBox4.Text;
            string idx = textBox5.Text;

            string same = textBox11.Text;

            string price = textBox10.Text;

            string happy1 = textBox9.Text;
            string happy2 = textBox8.Text;
            string happy3 = textBox7.Text;
            string happy4 = textBox6.Text;


            bool ok1 = check(happy1);
            bool ok2 = check(happy2);
            bool ok3 = check(happy3);
            bool ok4 = check(happy4);
            bool ok5 = check(price);
            bool ok6 = check(same);

            if (happy1 == "" || happy2 == "" || happy3 == "" || happy4 == "" || same == "" ) ok1 = false;
            
            if (ok1 == false || ok2 == false || ok3 == false || ok4 == false || ok5 == false || ok6 == false  )
            {
                richTextBox2.Text = "輸入有誤";
            }
            else
            {
                richTextBox2.Text = "新增成功";
                int total = Convert.ToInt32(happy1) + Convert.ToInt32(happy2) + Convert.ToInt32(happy3) + Convert.ToInt32(happy4);
                int need = Convert.ToInt32(price);

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";

                for (int i = 0; i < Convert.ToInt32(same); i++) add(idx, need, total);
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
