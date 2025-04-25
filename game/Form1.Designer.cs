namespace game
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.back_music = new AxWMPLib.AxWindowsMediaPlayer();
            this.music_minus_heart = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.back_music)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.music_minus_heart)).BeginInit();
            this.SuspendLayout();
            // 
            // back_music
            // 
            this.back_music.Enabled = true;
            this.back_music.Location = new System.Drawing.Point(0, 0);
            this.back_music.Name = "back_music";
            this.back_music.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("back_music.OcxState")));
            this.back_music.Size = new System.Drawing.Size(19, 30);
            this.back_music.TabIndex = 0;
            // 
            // music_minus_heart
            // 
            this.music_minus_heart.Enabled = true;
            this.music_minus_heart.Location = new System.Drawing.Point(12, 36);
            this.music_minus_heart.Name = "music_minus_heart";
            this.music_minus_heart.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("music_minus_heart.OcxState")));
            this.music_minus_heart.Size = new System.Drawing.Size(10, 25);
            this.music_minus_heart.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.music_minus_heart);
            this.Controls.Add(this.back_music);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.back_music)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.music_minus_heart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer back_music;
        private AxWMPLib.AxWindowsMediaPlayer music_minus_heart;
    }
}

