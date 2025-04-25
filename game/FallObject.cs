using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public class FallObject
    {
        public PictureBox picture_box { get; private set; }
        public int speed { get; set; }
        public int amount_cnt { get; set; }
        public bool down_stop = false;
        public bool plus_amount = false;
        public FallObject(int _speed, int _amount_cnt, Size size, Image image)
        {
            picture_box = new PictureBox
            {
                Size = size,
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            speed = _speed;
            amount_cnt = _amount_cnt;
        }
        public void MoveFallObject()
        {
            if(down_stop == false) picture_box.Top += speed;
            if (down_stop == true)
            {
                picture_box.Image = Image.FromFile("C:\\Users\\Vasilina\\Desktop\\конспекты\\ИТИП\\2 курс 1 семестр\\курсовая\\game\\assets\\fall_objects\\result_trash.png");
                picture_box.Invalidate();
            }
        }
    }
}
