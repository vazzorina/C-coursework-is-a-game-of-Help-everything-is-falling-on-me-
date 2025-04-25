using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public class Hero
    {
        public PictureBox picture_box { get; private set; }
        public int speed { get; set; }
        public int left_stop = 0;
        public int right_stop = 0;
        private bool rotate_image = false; //false - герой смотрит влево, true - герой смотрит вправо
        public Hero(int _speed, Point start_position, Size size, Image image)
        {
            picture_box = new PictureBox
            {
                Location = start_position,
                Size = size,
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            speed = _speed;
        }
        public void MoveHeroRight()
        {
            picture_box.Left += speed*right_stop;
            if(rotate_image == false)
            {
                picture_box.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                picture_box.Invalidate();
                rotate_image = true;
            }
        }

        public void MoveHeroLeft()
        {
            picture_box.Left += -speed*left_stop;
            if (rotate_image == true)
            {
                picture_box.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                picture_box.Invalidate();
                rotate_image = false;
            }
        }

    }
}
