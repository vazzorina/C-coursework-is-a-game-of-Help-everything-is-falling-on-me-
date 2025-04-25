using System;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace game
{
    public class Menu
    {
        public Panel menuPanel; // Панель для меню
        public Button playButton;
        public Button soundButton;
        public Button musicButton;
        public Button recordsButton;
        public Button howToPlayButton;
        public Button aboutButton;
        public Button exitButton;
        public Button backButton;
        public Label text_rules_or_about_game = new Label();
        public DataTable table_results = new DataTable();
        public DataGridView RecordsTable = new DataGridView();

        private PrivateFontCollection fonts = new PrivateFontCollection();
        private bool isMusicOn = true; // Флаг состояния музыки
        private bool isSoundOn = true; // Флаг состояния звуков

        public Menu(Form parentForm)
        {
            fonts.AddFontFile("C:\\Users\\Vasilina\\Desktop\\конспекты\\ИТИП\\2 курс 1 семестр\\курсовая\\game\\assets\\fonts\\ofont.ru_Unutterable.ttf");

            // Основные цвета
            Color backgroundColor = Color.FromArgb(255, 249, 244);
            Color borderColor = Color.FromArgb(220, 204, 182);
            Color buttonColor = Color.FromArgb(219, 242, 202);

            menuPanel = new Panel
            {
                Size = new Size(500, 700),
                BackColor = backgroundColor,
                Location = new Point(
                    (parentForm.ClientSize.Width - 500) / 2,
                    (parentForm.ClientSize.Height - 700) / 2
                ),
                BorderStyle = BorderStyle.FixedSingle,
                Visible = true // Панель видима по умолчанию
            };

            // Кнопки "Звуки" и "Музыка"
            soundButton = CreateButton("звуки", new Point(50, 50), new Size(190, 50));
            soundButton.Click += (s, e) => ToggleButtonText(soundButton, ref isSoundOn);

            musicButton = CreateButton("музыка", new Point(260, 50), new Size(190, 50));
            musicButton.Click += (s, e) => ToggleButtonText(musicButton, ref isMusicOn);

            // Кнопка "ИГРАТЬ"
            playButton = CreateButton("играть", new Point(50, 195), new Size(400, 100));
            playButton.BackColor = buttonColor;
            playButton.Font = new Font(fonts.Families[0], 36, FontStyle.Regular);

            // Кнопки остальных действий
            recordsButton = CreateButton("таблица рекордов", new Point(50, 390), new Size(400, 50));
            recordsButton.Click += (s, e) => ShowTableRecords();

            howToPlayButton = CreateButton("как играть?", new Point(50, 460), new Size(400, 50));
            howToPlayButton.Click += (s, e) => HowToPlay();

            aboutButton = CreateButton("об авторе", new Point(50, 530), new Size(400, 50));
            aboutButton.Click += (s, e) => AboutGame();

            exitButton = CreateButton("выйти", new Point(50, 600), new Size(400, 50));
            exitButton.Click += (s, e) => Application.Exit();

            backButton = CreateButton("назад", new Point(50, 600), new Size(400, 50));
            backButton.Click += (s, e) => BackMenu();
            backButton.Visible = false;

            text_rules_or_about_game.Location = new Point(50, 50);
            text_rules_or_about_game.Size = new Size(400, 530);
            text_rules_or_about_game.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);
            text_rules_or_about_game.Visible = false;


            table_results.Columns.Add("Дата и время");
            table_results.Columns.Add("Рекорд");
            RecordsTable.Visible = false;
            RecordsTable.TabStop = false;
            RecordsTable.Location = new Point(50, 50);
            RecordsTable.Size = new Size(400, 530);
            RecordsTable.RowHeadersVisible = false;
            RecordsTable.ReadOnly = true;
            RecordsTable.ScrollBars = ScrollBars.Both;
            RecordsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            RecordsTable.DefaultCellStyle.Font = new Font(fonts.Families[0], 14, FontStyle.Regular);
            RecordsTable.BackgroundColor = Color.White;
            RecordsTable.ColumnHeadersDefaultCellStyle.Font = new Font(fonts.Families[0], 14, FontStyle.Regular);
            RecordsTable.RowTemplate.Height = 30;
            RecordsTable.ColumnHeadersHeight = 40;
            RecordsTable.AllowUserToResizeRows = false;
            RecordsTable.DefaultCellStyle.SelectionBackColor = RecordsTable.DefaultCellStyle.BackColor;
            RecordsTable.DefaultCellStyle.SelectionForeColor = RecordsTable.DefaultCellStyle.ForeColor;
            RecordsTable.EnableHeadersVisualStyles = false;
            RecordsTable.ColumnHeadersDefaultCellStyle.SelectionBackColor = RecordsTable.ColumnHeadersDefaultCellStyle.BackColor;
            RecordsTable.ColumnHeadersDefaultCellStyle.SelectionForeColor = RecordsTable.ColumnHeadersDefaultCellStyle.ForeColor;



            menuPanel.Controls.Add(text_rules_or_about_game);
            menuPanel.Controls.Add(RecordsTable);

            // Добавление кнопок на панель
            menuPanel.Controls.Add(soundButton);
            menuPanel.Controls.Add(musicButton);
            menuPanel.Controls.Add(playButton);
            menuPanel.Controls.Add(recordsButton);
            menuPanel.Controls.Add(howToPlayButton);
            menuPanel.Controls.Add(aboutButton);
            menuPanel.Controls.Add(exitButton);
            menuPanel.Controls.Add(backButton);
        }

        private Button CreateButton(string text, Point location, Size size)
        {
            return new Button
            {
                Text = text,
                Size = size,
                Location = location,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(fonts.Families[0], 24, FontStyle.Regular),
                FlatAppearance = { BorderSize = 2, BorderColor = Color.FromArgb(220, 204, 182) }
            };
        }

        private void ToggleButtonText(Button button, ref bool isEnabled)
        {
            isEnabled = !isEnabled;
            button.Font = new Font(button.Font, isEnabled ? FontStyle.Regular : FontStyle.Strikeout);
        }

        private void ShowTableRecords()
        {
            
            RecordsTable.Visible = true;
            soundButton.Visible = false;
            musicButton.Visible = false;
            playButton.Visible = false;
            recordsButton.Visible = false;
            aboutButton.Visible = false;
            exitButton.Visible = false;
            howToPlayButton.Visible = false;
            backButton.Visible = true;
        }

        private void BackMenu()
        {
            soundButton.Visible = true;
            musicButton.Visible = true;
            playButton.Visible = true;
            recordsButton.Visible = true;
            aboutButton.Visible = true;
            exitButton.Visible = true;
            howToPlayButton.Visible = true;

            RecordsTable.Visible = false;
            backButton.Visible = false;
            text_rules_or_about_game.Visible = false;
        }

        private void HowToPlay()
        {
            text_rules_or_about_game.Text = "Правила игры:\n\nПередвижение: \n A - влево \n D - вправо\n\nЗадача игрока - уворачиваться от падающих предметов." +
                "\n\nУ игрока есть только 3 жизни. За каждый упавший на голову предмет минус 1 жизнь. " +
                "\n\nЗа каждый предмет, упавший на землю, к счетчику прибавляется значение, соответсвущее упавшему предмету." +
                "\n\nКирпич - 10 очков \nСтул - 9 очков \nГоршок с цветком - 7 очков \nМусорный пакет - 5 очков \nПодушка - 2 очка" +
                "\n\nВо время игры меню не открывается! Игру можно поставить только на паузу!";
            text_rules_or_about_game.Visible = true;
            soundButton.Visible = false;
            musicButton.Visible = false;
            playButton.Visible = false;
            recordsButton.Visible = false;
            aboutButton.Visible = false;
            exitButton.Visible = false;
            howToPlayButton.Visible = false;
            backButton.Visible = true;
        }

        private void AboutGame()
        {
            text_rules_or_about_game.Text = "Автор:\nАнтипова Василина" +
                                            "\n\nВерсия:\n0.0.0.1" +
                                            "\n\nГод выпуска:\n2024";
            text_rules_or_about_game.Visible = true;
            soundButton.Visible = false;
            musicButton.Visible = false;
            playButton.Visible = false;
            recordsButton.Visible = false;
            aboutButton.Visible = false;
            exitButton.Visible = false;
            howToPlayButton.Visible = false;
            backButton.Visible = true;
        }
    }
}
