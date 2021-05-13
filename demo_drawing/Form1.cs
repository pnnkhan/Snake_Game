using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo_drawing
{
    public partial class frmMain : Form
    {
        //khởi tạo độ dài của rắn ban đầu
        int doDai = 3;
        // mảng các tọa độ của rắn
        int[] x;
        int[] y;
        //khởi tạo ma trận giao diện
        int[,] matrix = new int[20, 20];
        //sử lý phím di chuyển
        public static int GO_UP = 1;
        public static int GO_DOWN = -1;
        public static int GO_LEFT = 2;
        public static int GO_RIGHT = -2;
        bool status_vector = false;
        //biến xét vị trí chạy mặc định khi vào game
        int vector = GO_UP;
        int diem = 0;
        //trạng thái game
        bool playGame = true;
        public frmMain()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            x = new int[400];
            y = new int[400];
            //khởi tạo 3 vi trí ban đầu của rắn
            x[0] = 5;
            y[0] = 5;

            x[1] = 5;
            y[1] = 6;

            x[2] = 5;
            y[2] = 7;
            //
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    matrix[i, j] = 0;
            //khởi tạo vị trí mồi đầu tiên
            matrix[10, 10] = 2;
            //
            label6.BackColor = Color.Yellow;
            label7.BackColor = Color.White;
            label8.BackColor = Color.White;
            label9.BackColor = Color.White;
            timer1.Interval = 400;

        }
        private void Form1_Click(object sender, EventArgs e)
        {
            //this.Paint =new PaintEventHandler(this.Form1_Paint);

        }
        //sự kiện vẽ khung chơi game, rắn và mồi ban đầu
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Black);
            SolidBrush sb = new SolidBrush(Color.White);
            Graphics g = this.CreateGraphics();
            //vẽ khung hình
            g.DrawRectangle(p, 0, 0, 401, 401);
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    //vẽ ma trận giao diện
                    g.FillRectangle(sb, i * 20 + 1, j * 20 + 1, 18, 18);
                    //vẽ mồi
                    if (matrix[i, j] == 2)
                        g.FillRectangle(new SolidBrush(Color.Black), i * 20 + 1, j * 20 + 1, 18, 18);

                }
            //vẽ rắn
            for (int z = 1; z < doDai; z++)
                g.FillRectangle(new SolidBrush(Color.Blue), x[z] * 20 + 1, y[z] * 20 + 1, 18, 18);
            g.FillRectangle(new SolidBrush(Color.Green), x[0] * 20 + 1, y[0] * 20 + 1, 18, 18);
            g.Dispose();
            sb.Dispose();

        }

        private void frmDay_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        //đây là sự kiện bắt phím di chuyển phím chức năng của game
        private void frmDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) {
                if (vector != -GO_UP && status_vector) {
                    vector = GO_UP;
                    status_vector = false;
                }
            }
            if (e.KeyCode == Keys.Down) {
                if (vector != -GO_DOWN && status_vector) {
                    vector = GO_DOWN;
                    status_vector = false;
                }
            }
            if (e.KeyCode == Keys.Left) {
                if (vector != -GO_LEFT && status_vector) {
                    vector = GO_LEFT;
                    status_vector = false;
                }
            }
            if (e.KeyCode == Keys.Right) {
                if (vector != -GO_RIGHT && status_vector) {
                    vector = GO_RIGHT;
                    status_vector = false;
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                //playGame = true;
                if (playGame)
                {
                    timer1.Enabled = true;
                    playGame = false;
                }
            }
            if (e.KeyCode == Keys.F1)
            {
                Application.Restart();
            }
            if (e.KeyCode == Keys.F2)
            {
                if (label3.Text == "F2: Pause")
                {
                    timer1.Enabled = false;
                    label3.Text = "F2: Resume";
                }
                else
                {
                    timer1.Enabled = true;
                    label3.Text = "F2: Pause";
                }
            }
        }
        int X()
        {
            Random rd = new Random();
            return rd.Next(0, 19);
        }
        int Y()
        {
            Random rd = new Random();
            return rd.Next(0, 19);
        }
        bool testXY(int m, int n)
        {
            if (m == 0 && n == 0) return false;
            for (int i = 0; i < doDai; i++)
                if (m == x[i] && n == y[i]) return false;
            return true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //khởi tạo 1 timer tuần hoàn chu trình chạy của chương trình
            //dùng set thời gian timer để lập lever cho game
            status_vector = true;
            //dịch chuyển đuôi
            for (int i = doDai - 1; i > 0; i--)
            {
                x[i] = x[i - 1];
                y[i] = y[i - 1];
            }
            //dịch đầu
            if (vector == GO_DOWN) y[0]++;
            if (vector == GO_UP) y[0]--;
            if (vector == GO_LEFT) x[0]--;
            if (vector == GO_RIGHT) x[0]++;
            //thiết lập khi đâm vào biên
            if (x[0] < 0) x[0] = 19;
            if (x[0] > 19) x[0] = 0;
            if (y[0] < 0) y[0] = 19;
            if (y[0] > 19) y[0] = 0;

            SolidBrush sb = new SolidBrush(Color.White);
            //Pen pen = new Pen(Color.Red);
            Graphics g = this.CreateGraphics();
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    g.FillRectangle(sb, i * 20 + 1, j * 20 + 1, 18, 18);
                    if (matrix[i, j] == 2)
                        g.FillRectangle(new SolidBrush(Color.Black), i * 20 + 1, j * 20 + 1, 18, 18);

                }
            //vẽ rắn
            for (int z = 1; z < doDai; z++)
                g.FillRectangle(new SolidBrush(Color.Blue), x[z] * 20 + 1, y[z] * 20 + 1, 18, 18);
            g.FillRectangle(new SolidBrush(Color.Green), x[0] * 20 + 1, y[0] * 20 + 1, 18, 18);
            //kiểm tra rắn ăn mồi chưa
            if (matrix[x[0], y[0]] == 2)
            {
                doDai++;
                diem += 10;
                lbDiem.Text = diem.ToString();
                g.FillRectangle(new SolidBrush(Color.White), 1, 1, 18, 18);
                matrix[x[0], y[0]] = 0;
                //vẽ lại rắn
                for (int z = 1; z < doDai; z++)
                    g.FillRectangle(new SolidBrush(Color.Blue), x[z] * 20 + 1, y[z] * 20 + 1, 18, 18);
                g.FillRectangle(new SolidBrush(Color.Green), x[0] * 20 + 1, y[0] * 20 + 1, 18, 18);
                int m, n;
                do
                {
                    m = X();
                    n = Y();
                } while (testXY(m, n) == false);//kiểm tra random trên thân rắn không

                // MessageBox.Show(m.ToString() + "   " + n.ToString());
                matrix[m, n] = 2;
                g.FillRectangle(new SolidBrush(Color.Black), m * 20 + 1, n * 20 + 1, 18, 18);
            }
            //xử lý đâm vào thân
            for (int i = 1; i < doDai; i++)
                if (x[0] == x[i] && y[0] == y[i])
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Bạn đâm vào đuôi mình!\nĐiểm:  " + diem.ToString(), "Thông báo");
                    Application.Restart();
                    //DialogResult dg;
                    //dg = MessageBox.Show("Bạn có muốn chơi lại không", "Thông báo", MessageBoxButtons.YesNo);
                    //if (dg == DialogResult.Yes)
                    //    Application.Restart();
                    //else
                    //{

                    //}
                }

            g.Dispose();
            sb.Dispose();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //DialogResult dg;
            //dg = MessageBox.Show("Bạn có muốn chơi lại không", "Thông báo", MessageBoxButtons.YesNo);
            //if (dg == DialogResult.Yes)
            //{
            //    Application.Exit();
            //}
            //else
            //{
            //    timer1.Enabled = true;
            //}
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            //if (btnPause.Text == "Pause")
            //{
            //    timer1.Enabled = false;
            //    btnPause.Text = "Resume";
            //}
            //else
            //{
            //    timer1.Enabled = true;
            //    btnPause.Text = "Pause";
            //}
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //newGame();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (comboBox1.Text)
            //{
            //    case "1":
            //        timer1.Interval = 500;
            //        break;
            //    case "2":
            //        timer1.Interval = 400;
            //        break;
            //    case "3":
            //        timer1.Interval = 300;
            //        break;
            //    case "4":
            //        timer1.Interval = 200;
            //        break;
            //    case "5":
            //        timer1.Interval = 100;
            //        break;
        }
        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
           
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
            
        }
        //phần dưới là các sự kiện mình bắt để chọn lever cho game
        private void label6_Click_1(object sender, EventArgs e)
        {

            label6.BackColor = Color.Yellow;
            label7.BackColor = Color.White;
            label8.BackColor = Color.White;
            label9.BackColor = Color.White;
            timer1.Interval = 400;
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            label6.BackColor = Color.White;
            label7.BackColor = Color.Yellow;
            label8.BackColor = Color.White;
            label9.BackColor = Color.White;
            timer1.Interval = 300;
        }

        private void label8_Click_1(object sender, EventArgs e)
        {

            label6.BackColor = Color.White;
            label7.BackColor = Color.White;
            label8.BackColor = Color.Yellow;
            label9.BackColor = Color.White;
            timer1.Interval = 200;
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            label6.BackColor = Color.White;
            label7.BackColor = Color.White;
            label8.BackColor = Color.White;
            label9.BackColor = Color.Yellow;
            timer1.Interval = 100;
        }
    }
}
