namespace Pdf2Image.Views
{
    partial class ShowImage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Image = new PictureBox();
            button1 = new Button();
            vScrollBar1 = new VScrollBar();
            ((System.ComponentModel.ISupportInitialize)Image).BeginInit();
            SuspendLayout();
            // 
            // Image
            // 
            Image.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Image.BorderStyle = BorderStyle.FixedSingle;
            Image.Location = new Point(12, 12);
            Image.Name = "Image";
            Image.Size = new Size(900, 501);
            Image.SizeMode = PictureBoxSizeMode.AutoSize;
            Image.TabIndex = 0;
            Image.TabStop = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(856, 519);
            button1.Name = "button1";
            button1.Size = new Size(69, 26);
            button1.TabIndex = 1;
            button1.Text = "Aceptar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Dock = DockStyle.Right;
            vScrollBar1.Location = new Point(928, 0);
            vScrollBar1.Maximum = 300;
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(29, 557);
            vScrollBar1.TabIndex = 2;
            vScrollBar1.Scroll += vScrollBar1_Scroll;
            // 
            // ShowImage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(957, 557);
            Controls.Add(vScrollBar1);
            Controls.Add(button1);
            Controls.Add(Image);
            Name = "ShowImage";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Image).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        public PictureBox Image;
        private VScrollBar vScrollBar1;
    }
}