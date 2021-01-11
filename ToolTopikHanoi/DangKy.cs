using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolTopikHanoi.EF;
using ToolTopikHanoi.IIS.Domain;

namespace ToolTopikHanoi
{
   
    public partial class DangKy : Form
    {
        public string id { get; set; }
        public readonly ICustomer custor;
        public DangKy()
        {
            custor = new Customer();
            InitializeComponent();
            
        }

        private void DangKy_Load(object sender, EventArgs e)
        {
            Combobox();
            if (id != null)
            {
                int idRecord = Convert.ToInt32(id);
                var updateRecord = custor.GetInfoId(idRecord);
                if (updateRecord != null)
                {
                    cbTopik.SelectedValue = updateRecord.Topik;
                    txtEmail.Enabled = true;
                    txtEmail.Text = updateRecord.Email;
                    txtPassword.Text = updateRecord.Password;
                    txtNameEng.Text = updateRecord.NameEng;
                    txtNameKorean.Text = updateRecord.NameKor;
                    cbDate.SelectedValue = updateRecord.DateId;
                    cbMonth.SelectedValue = updateRecord.MonthId;
                    cbYear.SelectedValue = updateRecord.YearId;
                    cbAge.SelectedValue = updateRecord.AgeId;
                    if (updateRecord.Sex == true)
                    {
                        rdWomen.Checked = true;
                    }
                    else
                    {
                        rdWomen.Checked = false;
                    }
                    cbCountry.Text = "Việt Nam";
                    txtCMND.Text = updateRecord.CMND;
                    cbJob.SelectedValue = updateRecord.JobId;
                    txtPhoneHome.Text = updateRecord.PhoneHome.ToString();
                    txtPhoneNumber.Text = updateRecord.PhoneNumber.ToString();
                    txtAddress.Text = updateRecord.Address.ToString();
                    cbTrainer.SelectedValue = updateRecord.PhuongTienId;
                    cbPurpose.SelectedValue = updateRecord.MucDichId;
                }
            }
        }
        //Combobox phương tiện, nghề nghiệp, quốc gia, mục đích, ngày tháng năm
        private void Combobox()
        {
            var date = custor.getDate();
            var month = custor.getMonth();
            var year = custor.getYear();
            var age = custor.getAge();
            var country = new List<Country>()
            {
                new Country(){ Id = "VNM", Name = "Việt Nam"}
            };
            var job = custor.getJob();
            var trainer = custor.getTrainer();
            var purpose = custor.getPurpose();
            //Date(Ngày)
            cbDate.DisplayMember = "Date1";
            cbDate.ValueMember = "Id";
            cbDate.DataSource = date;
            //Month(tháng)
            cbMonth.DisplayMember = "Month1";
            cbMonth.ValueMember = "Id";
            cbMonth.DataSource = month;
            //Year(Năm)
            cbYear.DisplayMember = "Year1";
            cbYear.ValueMember = "Id";
            cbYear.DataSource = year;
            //Age(Tuổi)
            cbAge.DisplayMember = "Age1";
            cbAge.ValueMember = "Id";
            cbAge.DataSource = age;
            //Country(Quốc gia)
            cbCountry.DisplayMember = "Name";
            cbCountry.ValueMember = "Id";
            cbCountry.DataSource = country;
            //Job(Nghề nghiệp)
            cbJob.DisplayMember = "Job1";
            cbJob.ValueMember = "Id";
            cbJob.DataSource = job;
            //Trainer(Phương tiện)
            cbTrainer.DisplayMember = "PhuongTien1";
            cbTrainer.ValueMember = "Id";
            cbTrainer.DataSource = trainer;
            //Purpose(Mục đích)
            cbPurpose.DisplayMember = "MucDich1";
            cbPurpose.ValueMember = "Id";
            cbPurpose.DataSource = purpose;
            //Topik
            var topik = new List<Topik>()
            {
                new Topik(){ Id = 1, Name = 1 },
                new Topik(){ Id = 2, Name = 2}
            };
            cbTopik.DisplayMember = "Name";
            cbTopik.ValueMember = "Id";
            cbTopik.DataSource = topik;

            rdWomen.Checked = true;
        }

        private void rdMem_CheckedChanged(object sender, EventArgs e)
        {
            rdWomen.Checked = !rdMem.Checked;
        }

        private void rdWomen_CheckedChanged(object sender, EventArgs e)
        {
            rdMem.Checked = !rdWomen.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(id == null)
            {
                InsertData();
            }
            else
            {
                UpdateData();
            }
        }

        private void InsertData()
        {
            try
            {
                try
                {
                    string fileName = Path.GetFileName(pathImage.Text);
                    string wanted_path = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), @"Images\" + fileName);
                    File.Copy(pathImage.Text, wanted_path, true);
                }
                catch(Exception ex)
                {
                   MessageBox.Show(ex.Message);
                   return;
                }

                bool isEmptry = txtEmail.Text.Equals("") || txtPassword.Text.Equals("") || txtNameEng.Text.Equals("") || txtNameKorean.Text.Equals("") ||
                    cbDate.Text.Equals("") || cbMonth.Text.Equals("") || cbYear.Text.Equals("") || cbAge.Text.Equals("") ||
                    cbCountry.Text.Equals("") || txtCMND.Text.Equals("") || cbJob.Text.Equals("") || txtPhoneHome.Text.Equals("") ||
                    txtPhoneNumber.Text.Equals("") || txtAddress.Text.Equals("") || cbTrainer.Text.Equals("") || cbPurpose.Text.Equals("");
                if (isEmptry)
                {
                    MessageBox.Show("Bạn cần nhập đầy đủ thông tin!");
                    return;
                }
                var checkSex = false;
                if (rdWomen.Checked) checkSex = true;
                string date = cbDate.SelectedValue.ToString();
                int dateId = Convert.ToInt32(date);
                int monthId = Convert.ToInt32(cbMonth.SelectedValue.ToString());
                int yearId = Convert.ToInt32(cbYear.SelectedValue.ToString());
                int ageId = Convert.ToInt32(cbAge.SelectedValue.ToString());
                int jobId = Convert.ToInt32(cbJob.SelectedValue.ToString());
                int phuongtienId = Convert.ToInt32(cbTrainer.SelectedValue.ToString());
                int mucdichId = Convert.ToInt32(cbPurpose.SelectedValue.ToString());
                int topik = Convert.ToInt32(cbTopik.SelectedValue.ToString());
                var member = new Person()
                {
                    Topik = topik,
                    Email = txtEmail.Text.ToString(),
                    Password = txtPassword.Text.ToString(),
                    NameEng = txtNameEng.Text.ToString(),
                    NameKor = txtNameKorean.Text.ToString(),
                    DateId = dateId,
                    MonthId = monthId,
                    YearId = yearId,
                    AgeId = ageId,
                    Sex = checkSex,
                    Country = cbCountry.SelectedValue.ToString(),
                    CMND = txtCMND.Text.ToString(),
                    JobId = jobId,
                    PhoneHome = txtPhoneHome.Text.ToString(),
                    PhoneNumber = txtPhoneNumber.Text.ToString(),
                    Address = txtAddress.Text.ToString(),
                    PhuongTienId = phuongtienId,
                    MucDichId = mucdichId
                };
                id = null;
                var error = custor.InsertData(member);
                if (error)
                {
                    ResetInput();
                    MessageBox.Show("Thêm mới thành công!");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateData()
        {
            try
            {
                var model = new Person();
                var checkSex = false;
                if (rdWomen.Checked) checkSex = true;
                model.Topik = Convert.ToInt32(cbTopik.SelectedValue.ToString());
                model.Email = txtEmail.Text.ToString();
                model.Password = txtPassword.Text.ToString();
                model.NameEng = txtNameEng.Text.ToString();
                model.NameKor = txtNameKorean.Text.ToString();
                model.DateId = Convert.ToInt32(cbDate.SelectedValue.ToString());
                model.MonthId = Convert.ToInt32(cbMonth.SelectedValue.ToString());
                model.YearId = Convert.ToInt32(cbYear.SelectedValue.ToString());
                model.AgeId = Convert.ToInt32(cbAge.SelectedValue.ToString());
                model.Sex = checkSex;
                model.Country = cbCountry.SelectedValue.ToString();
                model.CMND = txtCMND.Text.ToString();
                model.JobId = Convert.ToInt32(cbJob.SelectedValue.ToString());
                model.PhoneHome = txtPhoneHome.Text.ToString();
                model.PhoneNumber = txtPhoneNumber.Text.ToString();
                model.Address = txtAddress.Text.ToString();
                model.PhuongTienId = Convert.ToInt32(cbTrainer.SelectedValue.ToString());
                model.MucDichId = Convert.ToInt32(cbPurpose.SelectedValue.ToString());
                var checkError = custor.UpdateData(model);
                id = null;
                if (checkError)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy bản ghi!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void ResetInput()
        {
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtNameEng.Text = string.Empty;
            txtNameKorean.Text = string.Empty;
            cbDate.SelectedValue = 1;
            cbMonth.SelectedValue = 1;
            cbYear.SelectedValue = 1999;
            cbAge.SelectedValue = 7;
            cbJob.SelectedValue = 1;
            cbTrainer.SelectedValue = 1;
            cbPurpose.SelectedValue = 1;
            txtCMND.Text = string.Empty;
            txtPhoneHome.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtAddress.Text = string.Empty;

        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
                openFileDialog.FilterIndex = 2;
                openFileDialog.Multiselect = true;
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.ShowReadOnly = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureImage.Image = Image.FromFile(openFileDialog.FileName.ToString());
                    pictureImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    pathImage.Text = openFileDialog.FileName.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + " Try later.");
            }

        }
        
    }
}
