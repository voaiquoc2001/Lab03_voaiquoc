using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_voaiquoc
{
    public partial class frmSinhVien : Form
    {
        QuanLySinhVien qlsv;
        

        public frmSinhVien()
        {
            InitializeComponent();
        }
        #region Phương thức bổ trợ 
        //Lấy thông tin từ controls thông tin SV
        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            bool gt = true;
            List<string> cn = new List<string>();
            sv.MaSo = this.mtxtMaSo.Text;
            sv.HoTen = this.txtHoTen.Text;
            sv.NgaySinh = this.dtpNgaySinh.Value;
            sv.DiaChi = this.txtDiaChi.Text;
            sv.Lop = this.cboLop.Text;
            sv.Hinh = this.txtHinh.Text;
            if (rdNu.Checked)
                gt = false;
            sv.GioiTinh = gt;
            for (int i = 0; i < this.clbChuyenNganh.Items.Count; i ++)
                if (clbChuyenNganh.GetItemChecked(i))
                    cn.Add(clbChuyenNganh.Items[i].ToString());
            sv.ChuyenNganh = cn;
            return sv;
        }
        //Lấy thông tin sinh viên từ dòng item của ListView
        private SinhVien GetSinhVienLV(ListViewItem lvitem)
        {
            SinhVien sv = new SinhVien();
            sv.MaSo = lvitem.SubItems[0].Text;
            sv.HoTen = lvitem.SubItems[1].Text;
            sv.NgaySinh = DateTime.Parse(lvitem.SubItems[2].Text);
            sv.DiaChi = lvitem.SubItems[3].Text;
            sv.Lop = lvitem.SubItems[4].Text;
            sv.GioiTinh = false;
            if (lvitem.SubItems[5].Text == "Nam")
                sv.GioiTinh = true;
            List<string> cn = new List<string>();
            string[] s = lvitem.SubItems[6].Text.Split(',');
            foreach (string t in s)
                cn.Add(t);
            sv.ChuyenNganh = cn;
            sv.Hinh = lvitem.SubItems[7].Text;
            return sv;
        }
        //Thiết lập các thông tin lên controls sinh viên
        private void ThietLapThongTin(SinhVien sv)
        {
            this.mtxtMaSo.Text = sv.MaSo;
            this.txtHoTen.Text = sv.HoTen;
            this.dtpNgaySinh.Value = sv.NgaySinh;
            this.txtDiaChi.Text = sv.DiaChi;
            this.cboLop.Text = sv.Lop;
            this.txtHinh.Text = sv.Hinh;
            this.pbHinh.ImageLocation = sv.Hinh;
            if (sv.GioiTinh)
                this.rdNam.Checked = true;
            else
                this.rdNu.Checked = true;

            for (int i = 0; i < this.clbChuyenNganh.Items.Count; i++)
                this.clbChuyenNganh.SetItemChecked(i, false);

            foreach (string s in sv.ChuyenNganh)
            {
                for (int i = 0; i < this.clbChuyenNganh.Items.Count; i++)
                    if (s.CompareTo(this.clbChuyenNganh.Items[i]) == 0)
                        this.clbChuyenNganh.SetItemChecked(i, true);
            }    
        }


        //Thêm sinh viên vào ListView
        private void ThemSV(SinhVien sv)
        {
            ListViewItem lvitem = new ListViewItem(sv.MaSo);
            lvitem.SubItems.Add(sv.HoTen);
            lvitem.SubItems.Add(sv.NgaySinh.ToShortDateString());
            lvitem.SubItems.Add(sv.DiaChi);
            lvitem.SubItems.Add(sv.Lop);
            string gt = "Nữ";
            if (sv.GioiTinh)
                gt = "Nam";
            lvitem.SubItems.Add(gt);
            string cn = "";
            foreach (string s in sv.ChuyenNganh)
                cn += s + ",";
            cn = cn.Substring(0, cn.Length - 1);
            lvitem.SubItems.Add(cn);
            lvitem.SubItems.Add(sv.Hinh);
            this.lvSinhVien.Items.Add(lvitem);
        }
        //Hiển thị các sinh viên trong qlsv lên ListView
        private void LoadListView()
        {
            this.lvSinhVien.Items.Clear();
            foreach (SinhVien sv in qlsv.DanhSach) 
            {
                ThemSV(sv);
            }
            toolStripStatusLabel3.Text = "Tổng số sinh viên: " + qlsv.DanhSach.Count;
        }
        #endregion
        #region Các sự kiện
        //sự kiện Load form



        private void Form1_Load(object sender, EventArgs e)
        {
            qlsv = new QuanLySinhVien();
            qlsv.DocTuFile();
            LoadListView();
        }
        //Khi chọn dòng sinh viên bên ListView 
        //thực hiện gán thông tin lên các control
        private void lvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.lvSinhVien.SelectedItems.Count;
            if (count > 0)
            {
                ListViewItem lvitem = this.lvSinhVien.SelectedItems[0];
                SinhVien sv = GetSinhVienLV(lvitem);
                ThietLapThongTin(sv);
            }    
        }
        //Chức năng thêm sinh viên
        private void btnThem_Click(object sender, EventArgs e)
        {
            SinhVien sv = GetSinhVien();
            SinhVien kq = qlsv.Tim(sv.MaSo, delegate (object obj1, object obj2)
            {
                return (obj2 as SinhVien).MaSo.CompareTo(obj1.ToString());

            });
            if (kq !=null)
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi thêm dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else 
            {
                this.qlsv.Them(sv);
                this.LoadListView();
            }                 
        }
        //Thoát chương trình
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Xóa tất cả sinh viên được chọn trên ListView
        private void btnXoa_Click(object sender, EventArgs e)
        {
            int count, i;
            ListViewItem lvitem;
            count = this.lvSinhVien.Items.Count - 1;

            for (i = count; i >= 0; i--)
            {
                lvitem = this.lvSinhVien.Items[i];
                if (lvitem.Checked)
                    qlsv.Xoa(lvitem.SubItems[0].Text, SoSanhTheoMa);
            }
            this.LoadListView();
            this.btnMacDinh.PerformClick();
        }
        //Sửa thông tin sinh viên được chọn
        private void btnSua_Click(object sender, EventArgs e)
        {
            SinhVien sv = GetSinhVien();
            bool kqsua;
            kqsua = qlsv.Sua(sv, sv.MaSo, SoSanhTheoMa);
            if (kqsua)
            {
                this.LoadListView();

            }    
        }
        //Để các control ở giá trị mặc định
        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            this.mtxtMaSo.Text = "";
            this.txtHoTen.Text = "";
            this.dtpNgaySinh.Value = DateTime.Now;
            this.txtDiaChi.Text = "";
            this.cboLop.Text = this.cboLop.Items[0].ToString();
            this.txtHinh.Text = "";
            this.pbHinh.ImageLocation = "";
            this.rdNam.Checked = true;
            for (int i = 0; i < this.clbChuyenNganh.Items.Count - 1; i++)
                this.clbChuyenNganh.SetItemChecked(i, false);
        }
        private int SoSanhTheoMa(object obj1, object obj2)
        {
            SinhVien sv = obj2 as SinhVien;
            return sv.MaSo.CompareTo(obj1);
        }
        #endregion

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = openFileDialog1;
            fileDialog.Filter = "Image Files (JPEG, GIF, BMP, etc.)|"
        + "*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff;*.png|"
        + "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|"
        + "GIF files (*.gif)|*.gif| BMP files (*.bmp) | *.bmp | "
         + "TIFF files (*.tif;*.tiff)|*.tif;*.tiff|"
        + "PNG files (*.png)|*.png| All files (*.*)|*.*";
            fileDialog.InitialDirectory = Environment.CurrentDirectory;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = fileDialog.FileName;
                txtHinh.Text = filename;
                pbHinh.Load(filename);
            }
        }

        private void lvSinhVien_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
           
        }
        // Thiết lập các sự kiện File và Edit
        private void mnOpenFile_Click(object sender, EventArgs e)
        {
            btnBrowse.PerformClick();
        }

        private void mnExit_Click(object sender, EventArgs e)
        {
            btnThoat.PerformClick();
        }

        private void mnAdd_Click(object sender, EventArgs e)
        {
            btnThem.PerformClick();
        }

        private void mnDelete_Click(object sender, EventArgs e)
        {
            btnXoa.PerformClick();
        }

        private void mnChange_Click(object sender, EventArgs e)
        {
            btnSua.PerformClick();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = fontDialog1;
            var isOK = fontDialog.ShowDialog();

            if (isOK == DialogResult.OK)
            {
                lvSinhVien.Font = fontDialog.Font;
            }    
        }

        private void màuChữToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            var isOK = colorDialog.ShowDialog();

            if (isOK == DialogResult.OK)
            {
                lvSinhVien.ForeColor = colorDialog.Color;
            }
        }

        private void sắpXếpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tìmKiếmToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
    }
}
