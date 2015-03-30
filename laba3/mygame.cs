//#undef DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace laba3
{
    public partial class mygame : UserControl
    {

        int freeplace;
        int Widthp ;
        int Heightp ;
        bool _legend = true;
        string _picture = null;
        Color _labelcolor = Color.Red;
        int _countstep;
        public int Countstep
        {
            private set { _countstep = value; }
            get { return _countstep; }
        }
        public Color LabelColor
        {
            set { _labelcolor = value; }
            get { return _labelcolor; }
        }

        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
        public bool Legeng
        {
            set
            {
                if (panel1.Controls.Count > 0)
                {
                    _legend = value;
                    for (int i = 0; i < 15; i++)
                    { panel1.Controls[i].Controls[0].Visible = value;
                    panel1.Controls[i].Controls[0].ForeColor = LabelColor;
                    }

                }
            }
            get
            {
                return _legend;
            }
        }

        public mygame()
        {
            InitializeComponent();
            Widthp = this.Height/4;
             Heightp =this.Height / 4;
        }

        private bool isEnd()
        {
            bool result = true;
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                mypanel p = panel1.Controls[i] as mypanel;
                if (p.Pos != p.Realpos)
                { result = false; break; }
            }
            return result;
        }
        private void my_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Widthp * 4, Heightp * 4);
        }

        private int[] Shake()
        {
            int[] mas = new int[15];
            for (int i = 0; i < 15; i++)
            {
                int ran;
                do
                {
                    ran = new Random().Next(-1, 15);
                }
                while (Array.IndexOf(mas, ran) >= 0);
                mas[i] = ran;
            }
            mas[Array.IndexOf(mas, -1)] = 0;
            return mas;
        }



        private Image[] Trimimage(string path)
        {
            
            Image original = Image.FromFile(path);
            Image original2 = new Bitmap(Widthp*4, Heightp*4);
            Graphics org2 = Graphics.FromImage(original2);
            org2.ScaleTransform((float)original2.Width / original.Width, (float)original2.Height / original.Height);
                        org2.DrawImageUnscaled(original, 0, 0);

            Image[] pics = new Image[16];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    Image im2 = new Bitmap(Widthp, Height);
                    Graphics g = Graphics.FromImage(im2);
                    g.DrawImageUnscaled(original2, new Rectangle(-j * Heightp, -i * Widthp, Widthp, Height));
                    pics[j + i * 4] = im2;
                }
            return pics;
        }

        Image[] pics;
        public void init()
        {
            mypanel pl;
            Label lb;
            panel1.Controls.Clear();
            freeplace = 15;
            Countstep = 0;
            pics = new Image[16];
            if (Picture != null)
                pics = Trimimage(Picture);
            int[] mas = Shake();
            for (int i = 0; i < 15; i++)
            {
                pl = new mypanel();

                pl.Size = new Size(Widthp, Heightp);
                pl.BorderStyle = BorderStyle.FixedSingle;
                #if (!DEBUG)
                pl.ToPos(i, false);
                #else
                pl.ToPos(mas[i], false);
                #endif
                pl.Realpos = i;
                if (Picture != null)
                    pl.BackgroundImage = pics[i];
                else
                    pl.BackColor = Color.Cyan;

                lb = new Label();
                lb.Text = (i + 1).ToString();
                lb.BackColor = Color.Transparent;
                lb.ForeColor = LabelColor;

                lb.Font = new Font(lb.Font.FontFamily, (float)15, FontStyle.Bold);
                pl.Controls.Add(lb);
                //lb.Name = "lb_" + i;
                lb.TextAlign = ContentAlignment.MiddleCenter;

                lb.Size = pl.Size;
                lb.MouseDown += new MouseEventHandler(pl_MouseDown);
               // lb.MouseDoubleClick += new MouseEventHandler(lb_MouseDoubleClick);
                pl.MouseDown += new MouseEventHandler(pl_MouseDown);
                panel1.Controls.Add(pl);

            }
        }
        
        void pl_MouseDown(object sender, MouseEventArgs e)
        {
            mypanel p;
            if (sender is Label)
                p = (sender as Label).Parent as mypanel;
            else
                p = sender as mypanel;
            if (p.Havefree(freeplace))
            {
                int temp;
                temp = p.Pos;
                p.ToPos(freeplace, true);
                freeplace = temp;
                Countstep++;
                if (MoveKvadrat != null)
                    MoveKvadrat(this, EventArgs.Empty);
                if (isEnd())
                {
                    if (Finish != null)
                        Finish(this, EventArgs.Empty);
                    mypanel pl = new mypanel();
                    pl.Size = new Size(Widthp, Heightp);
                    pl.BorderStyle = BorderStyle.FixedSingle;

                    pl.Realpos = 15;
                    pl.ToPos(15, false);
                    pl.BackgroundImage = pics[15];
                    panel1.Controls.Add(pl);
                    freeplace = 100;
                    Legeng = false;

                }
            }
        }
     
        public event EventHandler Finish;
        public event EventHandler MoveKvadrat;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class mypanel : Panel
    {
        public int Realpos;
        public int Pos;
        public bool Havefree(int freeblock)
        {
            int x = Pos % 4;
            int y = Pos / 4;
            return (POinttoPos(x - 1, y) == freeblock && POinttoPos(x - 1, y) / 4 == POinttoPos(x, y) / 4) || (POinttoPos(x + 1, y) == freeblock && POinttoPos(x +1, y) / 4 == POinttoPos(x, y) / 4) || POinttoPos(x, y - 1) == freeblock || POinttoPos(x, y + 1) == freeblock;
        }
        public void ToPos(int index, bool withEffect)
        {
            Point Nextpoint = new Point(index % 4 * Width, index / 4 * Height);
            if (withEffect)
            {
                int speed = 12;
                Timer timer1 = new Timer();
                timer1.Interval = 15;
                timer1.Tick += delegate
                {
                    if (Math.Abs(this.Location.X - Nextpoint.X) > speed || Math.Abs(this.Location.Y - Nextpoint.Y) > speed)
                    {
                        if (Math.Abs(this.Location.X - Nextpoint.X) < speed / 2)
                        {
                            int pos = this.Location.Y > Nextpoint.Y ? -speed : speed;
                            this.Location = new Point(this.Location.X, this.Location.Y + pos);
                        }
                        else
                        {
                            int pos = this.Location.X > Nextpoint.X ? -speed : speed;
                            this.Location = new Point(this.Location.X + pos, this.Location.Y);
                        }
                    }
                    else
                    {
                        timer1.Stop();
                        this.Location = Nextpoint;
                    }
                    
                };
                timer1.Start();

            }
            else
                this.Location = Nextpoint;
            Pos = index;
        }
        private int POinttoPos(int x, int y)
        {
            return y * 4 + x;
        }
    }
}
