using System;
using System.Drawing;
using System.Windows.Forms;

namespace WFA_Chess_Game
{


    public class MyPictureBox : PictureBox
    {
        public event EventHandler<Button> MyCustomEvent;

        public void RaiseMyCustomEvent()
        {
            MyCustomEvent?.Invoke(this,new Button());
        }
    }

    public partial class Form2 : Form
    {
        private MyPictureBox pictureBox1;

        public Form2()
        {
            InitializeComponent();
            InitializeMyPictureBox();
        }

        private void InitializeMyPictureBox()
        {
            pictureBox1 = new MyPictureBox();
            pictureBox1.Size = new Size(100, 100);
            pictureBox1.Location = new Point(50, 50);
            pictureBox1.BackColor = Color.LightBlue;

            pictureBox1.MyCustomEvent += PictureBox1_MyCustomEvent;

            pictureBox1.Click += (s, e) =>
            {
                pictureBox1.RaiseMyCustomEvent();
            };

            this.Controls.Add(pictureBox1);
        }

        private void PictureBox1_MyCustomEvent(object sender, Button e)
        {
            MessageBox.Show("مرحبًا محمد! 👋", "رسالة مخصصة");
        }
    }
}
