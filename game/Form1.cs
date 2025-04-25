using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace game
{
    public partial class Form1 : Form
    {
        private string path = "C:\\Users\\Vasilina\\Desktop\\конспекты\\ИТИП\\2 курс 1 семестр\\курсовая\\game\\assets\\";
        private bool start_game = false;
        private UserInterface userInterface;
        private Menu menu;
        private Hero hero;

        private Timer timer;
        private Timer timer_for_objects;

        private List<FallObject> activeObjects = new List<FallObject>();
        private Timer spawnTimer;
        private Random random = new Random();
        private bool hero_is_dead = false;

        private int volume_music = 50;
        private bool on_bg_music = true;
        private bool on_minus_heart_music = true;

        private FileFunctions fileFunctions;
        private string file_results = "C:\\Users\\Vasilina\\Desktop\\конспекты\\ИТИП\\2 курс 1 семестр\\курсовая\\game\\results.txt";
        private List<ResultLine> results = new List<ResultLine>();

        private int speed_fall_object = 1;
        private int min_timer = 1000;
        private int max_timer = 3000;

        public Form1()
        { 
            InitializeComponent();
            this.Height = 900;
            this.Width = 1200;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Помогите, на меня падают вещи!";
            this.MaximizeBox = false;
            this.BackgroundImage = Image.FromFile(path + "background.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            back_music.URL = path + "sounds\\bg-music.mp3"; // Укажите путь к файлу
            back_music.settings.volume = volume_music; // Установите громкость
            back_music.Visible = false;
            back_music.TabStop = false;
            back_music.settings.setMode("loop", true);

            music_minus_heart.URL = path + "sounds\\minus-heart.mp3";
            music_minus_heart.settings.volume = volume_music;
            music_minus_heart.Visible = false;
            music_minus_heart.TabStop = false;
            music_minus_heart.Ctlcontrols.stop();
            

            hero = new Hero(10, new Point(this.ClientSize.Width/2 - 50, this.ClientSize.Height/20*17 - 100), new Size(100, 200), Image.FromFile(path + "hero.png"));
            this.Controls.Add(hero.picture_box);
          
            userInterface = new UserInterface();
            this.Controls.Add(userInterface.pause_btn);
            this.Controls.Add(userInterface.return_btn);
            this.Controls.Add(userInterface.counterLabel);
            this.Controls.Add(userInterface.MessageLabel);
            userInterface.MessageLabel.Visible = false;

            foreach (var heart in userInterface.hearts) this.Controls.Add(heart.heart_box);

            hero.picture_box.Visible = false;
            userInterface.Visible(false);
            menu = new Menu(this);
            this.Controls.Add(menu.menuPanel);
            
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;

            timer_for_objects = new Timer();
            timer_for_objects.Interval = 10;
            timer_for_objects.Tick += TimerFO_Tick;

            spawnTimer = new Timer();
            spawnTimer.Interval = random.Next(500, 2500);
            spawnTimer.Tick += SpawnTimer_Tick;

            fileFunctions = new FileFunctions();

            this.KeyDown += MainForm_KeyDown;
            this.KeyDown += MainForm_Menu_KeyDown;
            this.KeyUp += MainForm_KeyUp;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (hero.left_stop == 1 && hero.picture_box.Left > 0) hero.MoveHeroLeft();
            if (hero.right_stop == 1 && hero.picture_box.Right < this.ClientSize.Width) hero.MoveHeroRight();

            if (hero.picture_box.Left < 0) hero.left_stop = 0;
            if (hero.picture_box.Right > this.ClientSize.Width) hero.right_stop = 0;
        }

        private void TimerFO_Tick(object sender, EventArgs e)
        {
            foreach (var obj in activeObjects.ToList())
            {
                if (obj.picture_box.Bottom >= this.ClientSize.Height - 30)
                {
                    obj.down_stop = true;
                    if (obj.plus_amount == false)
                    {
                        userInterface.counter += obj.amount_cnt;
                        userInterface.counterLabel.Text = userInterface.counter.ToString();
                        obj.plus_amount = true;
                    }    
                    
                    obj.MoveFallObject();
                    obj.picture_box.Refresh();

                    Task.Delay(1500).ContinueWith(_ =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.Controls.Remove(obj.picture_box);
                            activeObjects.Remove(obj);
                        }));
                    });
                }
                else if(obj.picture_box.Bottom >= hero.picture_box.Top && obj.picture_box.Bottom <= hero.picture_box.Top + 50 && obj.picture_box.Left > hero.picture_box.Left - 80 && obj.picture_box.Right < hero.picture_box.Right + 80)
                {
                    this.Controls.Remove(obj.picture_box);
                    activeObjects.Remove(obj);
                    if (userInterface.cnt_hearts > 0)
                    {
                        userInterface.hearts[userInterface.cnt_hearts - 1].LossHeart();
                        userInterface.cnt_hearts--;
                        if(on_minus_heart_music == true) music_minus_heart.Ctlcontrols.play();
                        if (userInterface.cnt_hearts == 0)
                        {
                            fileFunctions.AddNewBestResult(file_results, results, userInterface.counter);
                            
                            timer.Stop();
                            timer_for_objects.Stop();
                            spawnTimer.Stop();
                            hero_is_dead = true;
                            int result = 0;
                            if (results.Count > 0) result = results[0].result;
                            userInterface.MessageLabel.Text = "Игра окончена! \n Ваш рекорд: " + userInterface.counter + "\n Лучший рекорд: " + result;
                            userInterface.MessageLabel.Visible = true;
                        }
                    }
                }
                else obj.MoveFallObject();
            }
        }
        
        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            
            if (userInterface.counter % 100 >= 0 && userInterface.counter % 100 <= 5)
            {
                if (max_timer > 800) max_timer -= 150;
                if (min_timer > 500)
                {
                    hero.speed++;
                    min_timer -= 150;
                    speed_fall_object += 3;
                }    
            }

            List<FallObject> fallObjects = new List<FallObject>
            {
                new FallObject(speed_fall_object, 5, new Size(100, 100), Image.FromFile(path + "fall_objects\\trash.png")),
                new FallObject(speed_fall_object, 2, new Size(100, 100), Image.FromFile(path + "fall_objects\\pillow.png")),
                new FallObject(speed_fall_object, 10, new Size(100, 100), Image.FromFile(path + "fall_objects\\brick.png")),
                new FallObject(speed_fall_object, 7, new Size(100, 100), Image.FromFile(path + "fall_objects\\flower.png")),
                new FallObject(speed_fall_object, 9, new Size(100, 100), Image.FromFile(path + "fall_objects\\chair.png"))
            };

            FallObject newObject = fallObjects[random.Next(fallObjects.Count)];
            newObject.picture_box.Location = new Point(random.Next(0, this.ClientSize.Width - newObject.picture_box.Width), -50);

            this.Controls.Add(newObject.picture_box);
            activeObjects.Add(newObject);

            spawnTimer.Interval = random.Next(min_timer, max_timer); // Новый интервал (1-3 секунды)
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) hero.left_stop = 1; // Персонаж двигается влево
            else if (e.KeyCode == Keys.D) hero.right_stop = 1; // Персонаж двигается вправо
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) hero.left_stop = 0; // Останавливаем движение влево
            else if (e.KeyCode == Keys.D) hero.right_stop = 0; // Останавливаем движение вправо
        }

        private void MainForm_Menu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) StopGame(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            userInterface.pause_btn.Click += Pause_Click;
            userInterface.return_btn.Click += Return_Click;

            menu.playButton.Click += PlayBtn_Click;
            menu.soundButton.Click += SoundBtn_Click;
            menu.musicButton.Click += MusicBtn_Click;
            menu.recordsButton.Click += TableShowBtn_Click;
        }
        private void PlayBtn_Click(object sender, EventArgs e)
        {
            if (start_game == false && hero_is_dead == false) StopGame();
            if (hero_is_dead == true)
            {
                OverGame();
            }
        }

        private void TableShowBtn_Click(object sender, EventArgs e)
        {
            menu.RecordsTable.DataSource = menu.table_results;
            fileFunctions.ReadResults(file_results, results);
            fileFunctions.WriteTableResults(menu.table_results, results);
        }

        private void SoundBtn_Click(object sender, EventArgs e)
        {
            on_minus_heart_music = !on_minus_heart_music;
        }

        private void MusicBtn_Click(object sender, EventArgs e)
        {
            on_bg_music = !on_bg_music;
            if(on_bg_music == true) back_music.Ctlcontrols.play();
            else back_music.Ctlcontrols.stop();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            StopGame();
        }
        private void Return_Click(object sender, EventArgs e)
        {
            OverGame();
        }

        private void StopGame()
        {
            if (hero_is_dead == false)
            {
                menu.menuPanel.Visible = false;
                start_game = !start_game;
                if (start_game == true)
                {
                    userInterface.MessageLabel.Visible = false;
                    hero.picture_box.Visible = true;
                    userInterface.Visible(true);
                    foreach (var obj in activeObjects) obj.picture_box.Visible = true;
                    timer.Start();
                    timer_for_objects.Start();
                    spawnTimer.Start();
                }
                else
                {
                    userInterface.MessageLabel.Text = "Игра на паузе!";
                    userInterface.MessageLabel.Visible = true;
                    timer.Stop();
                    timer_for_objects.Stop();
                    spawnTimer.Stop();
                }
            }
            else
            {
                hero.picture_box.Visible = false;
                userInterface.Visible(false);
                userInterface.MessageLabel.Visible = false;
                foreach (var obj in activeObjects) obj.picture_box.Visible = false;
                menu.menuPanel.Visible = true;
            }
        }

        private void OverGame()
        {
            hero.picture_box.Visible = true;
            userInterface.Visible(true);
            userInterface.MessageLabel.Visible = false;
            foreach (var obj in activeObjects) obj.picture_box.Visible = true;
            menu.menuPanel.Visible = false;
            foreach (var obj in activeObjects)
            {
                this.Controls.Remove(obj.picture_box);
            }
            activeObjects.Clear();

            hero.picture_box.Location = new Point(this.ClientSize.Width / 2 - 50, this.ClientSize.Height / 20 * 17 - 100);
            hero.left_stop = 0;
            hero.right_stop = 0;

            userInterface.ResetGame();
            start_game = false;
            speed_fall_object = 1;
            min_timer = 1000;
            max_timer = 3000;
            hero.speed = 10;

            timer.Start();
            timer_for_objects.Start();
            spawnTimer.Start();
            foreach (Control control in this.Controls) if (control is PictureBox) control.Visible = true;

            hero_is_dead = false;
        }
    }
}