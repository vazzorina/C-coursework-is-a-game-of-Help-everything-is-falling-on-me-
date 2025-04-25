using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public class Heart
    {
        private static string path = "C:\\Users\\Vasilina\\Desktop\\конспекты\\ИТИП\\2 курс 1 семестр\\курсовая\\game\\assets\\";
        public Button heart_box;

        public Heart(Point location)
        {
            heart_box = new Button
            {
                BackgroundImage = Image.FromFile(path + "heart.png"),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent,
                ForeColor = Color.Transparent,
                Size = new Size(80, 80),
                Location = location,
                FlatStyle = FlatStyle.Flat
            };
            heart_box.FlatAppearance.BorderSize = 0;
            heart_box.FlatAppearance.MouseOverBackColor = Color.Transparent;
            heart_box.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }

        public void LossHeart()
        {
            heart_box.BackgroundImage = Image.FromFile(path + "heart_lost.png");
        }
        public void ResetGame()
        {
            heart_box.BackgroundImage = Image.FromFile(path + "heart.png");
        }
    }
    public class UserInterface
    {
        private PrivateFontCollection fonts = new PrivateFontCollection();
        private static string path = "C:\\Users\\Vasilina\\Desktop\\конспекты\\ИТИП\\2 курс 1 семестр\\курсовая\\game\\assets\\";
        public Button pause_btn = new Button
        {
            BackgroundImage = Image.FromFile(path + "pause_btn.png"),
            BackgroundImageLayout = ImageLayout.Stretch,
            BackColor = Color.Transparent,
            ForeColor = Color.Transparent,
            Location = new Point(25, 25),
            Size = new Size(80, 80),
            FlatStyle = FlatStyle.Flat
        };

        public Button return_btn = new Button
        {
            BackgroundImage = Image.FromFile(path + "return_btn.png"),
            BackgroundImageLayout = ImageLayout.Stretch,
            BackColor = Color.Transparent,
            ForeColor = Color.Transparent,
            Location = new Point(115, 25),
            Size = new Size(80, 80),
            FlatStyle = FlatStyle.Flat
        };

        public List<Heart> hearts = new List<Heart>
        {
            new Heart(new Point(900, 25)),
            new Heart(new Point(990, 25)),
            new Heart(new Point(1080, 25)),
        };
        public int counter = 0;
        public int cnt_hearts = 3;
        public Label counterLabel = new Label
        {
            Text = "",
            Location = new Point(600-360/2, 25),
            Size = new Size(360, 80),
            BackColor = Color.Transparent,
            TextAlign = ContentAlignment.MiddleCenter,
        };
        public Label MessageLabel = new Label
        {
            Text = "",
            Location = new Point(600 - 600/2, 130),
            Size = new Size(600, 200),
            BackColor = Color.Transparent,
            TextAlign = ContentAlignment.MiddleCenter,
        };
        public UserInterface()
        {
            pause_btn.FlatAppearance.BorderSize = 0;
            pause_btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            pause_btn.FlatAppearance.MouseDownBackColor = Color.Transparent;

            return_btn.FlatAppearance.BorderSize = 0;
            return_btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            return_btn.FlatAppearance.MouseDownBackColor = Color.Transparent;

            fonts.AddFontFile(path + "fonts\\ofont.ru_Unutterable.ttf");
            counterLabel.Font = new Font(fonts.Families[0], 48, FontStyle.Regular);
            counterLabel.Text = counter.ToString();
            MessageLabel.Font = new Font(fonts.Families[0], 32, FontStyle.Regular);
        }

        public void ResetGame()
        {
            foreach (var heart in hearts) heart.ResetGame();
            counter = 0;
            counterLabel.Text = "0";
            cnt_hearts = 3;
        }

        public void Visible(bool visible)
        {
            return_btn.Visible = visible;
            pause_btn.Visible = visible;
            counterLabel.Visible = visible;
            foreach (var heart in hearts) heart.heart_box.Visible = visible;
        }
    }
}
