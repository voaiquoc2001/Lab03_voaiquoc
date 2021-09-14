
namespace Lab03_voaiquoc
{
    partial class frmTuyChon
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
            this.rdMaSV = new System.Windows.Forms.RadioButton();
            this.rdHoTen = new System.Windows.Forms.RadioButton();
            this.rdNgaySinh = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rdMaSV
            // 
            this.rdMaSV.AutoSize = true;
            this.rdMaSV.Location = new System.Drawing.Point(75, 83);
            this.rdMaSV.Name = "rdMaSV";
            this.rdMaSV.Size = new System.Drawing.Size(85, 17);
            this.rdMaSV.TabIndex = 0;
            this.rdMaSV.TabStop = true;
            this.rdMaSV.Text = "Mã sinh viên";
            this.rdMaSV.UseVisualStyleBackColor = true;
            // 
            // rdHoTen
            // 
            this.rdHoTen.AutoSize = true;
            this.rdHoTen.Location = new System.Drawing.Point(232, 83);
            this.rdHoTen.Name = "rdHoTen";
            this.rdHoTen.Size = new System.Drawing.Size(57, 17);
            this.rdHoTen.TabIndex = 1;
            this.rdHoTen.TabStop = true;
            this.rdHoTen.Text = "Họ tên";
            this.rdHoTen.UseVisualStyleBackColor = true;
            // 
            // rdNgaySinh
            // 
            this.rdNgaySinh.AutoSize = true;
            this.rdNgaySinh.Location = new System.Drawing.Point(389, 83);
            this.rdNgaySinh.Name = "rdNgaySinh";
            this.rdNgaySinh.Size = new System.Drawing.Size(72, 17);
            this.rdNgaySinh.TabIndex = 2;
            this.rdNgaySinh.TabStop = true;
            this.rdNgaySinh.Text = "Ngày sinh";
            this.rdNgaySinh.UseVisualStyleBackColor = true;
            // 
            // frmTuyChon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rdNgaySinh);
            this.Controls.Add(this.rdHoTen);
            this.Controls.Add(this.rdMaSV);
            this.Name = "frmTuyChon";
            this.Text = "Tuỳ chọn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdMaSV;
        private System.Windows.Forms.RadioButton rdHoTen;
        private System.Windows.Forms.RadioButton rdNgaySinh;
    }
}