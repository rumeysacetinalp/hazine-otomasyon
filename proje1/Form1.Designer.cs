namespace proje1
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtHaritaBoyutu = new System.Windows.Forms.TextBox();
            this.btnHaritaBoyutuBelirle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Haritanın boyutu :\r\n";
            // 
            // txtHaritaBoyutu
            // 
            this.txtHaritaBoyutu.Location = new System.Drawing.Point(191, 44);
            this.txtHaritaBoyutu.Name = "txtHaritaBoyutu";
            this.txtHaritaBoyutu.Size = new System.Drawing.Size(206, 22);
            this.txtHaritaBoyutu.TabIndex = 1;
            // 
            // btnHaritaBoyutuBelirle
            // 
            this.btnHaritaBoyutuBelirle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnHaritaBoyutuBelirle.Location = new System.Drawing.Point(426, 66);
            this.btnHaritaBoyutuBelirle.Name = "btnHaritaBoyutuBelirle";
            this.btnHaritaBoyutuBelirle.Size = new System.Drawing.Size(112, 56);
            this.btnHaritaBoyutuBelirle.TabIndex = 2;
            this.btnHaritaBoyutuBelirle.Text = "Oyunu Başlat";
            this.btnHaritaBoyutuBelirle.UseVisualStyleBackColor = false;
            this.btnHaritaBoyutuBelirle.Click += new System.EventHandler(this.btnHaritaBoyutuBelirle_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(77, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(191, 123);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(206, 22);
            this.textBox1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 216);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHaritaBoyutuBelirle);
            this.Controls.Add(this.txtHaritaBoyutu);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHaritaBoyutu;
        private System.Windows.Forms.Button btnHaritaBoyutuBelirle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

