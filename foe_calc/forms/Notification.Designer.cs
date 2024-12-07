using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace foe_calc
{
    partial class Notification
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

        public Notification(string text)
        {
            InitializeComponent();
            this.label1.Text = text;
            this.Location = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y+10);
            //this.label1.Location = new System.Drawing.Point((this.Width - this.label1.Width) / 2, (this.Width - this.label1.Width) / 2);
            this.Show();
            this.WaitSomeTime();
        }

        public async void WaitSomeTime()
        {
            this.Opacity = 1.0;
            for(var i=2000; i > 0; i -= 100)
            {
                this.Opacity -= 0.025;
                await Task.Delay(100);
            }
            //await Task.Delay(3000);
            this.Close();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Wheat;
            this.label1.Location = new System.Drawing.Point(125, 10);
            this.label1.Name = "label1";
            this.label1.Text = "text";
            this.label1.Size = new System.Drawing.Size(250, 13);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(500, 21);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageForm";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

        }

        #endregion

        private System.Windows.Forms.Label label1;
    }
}