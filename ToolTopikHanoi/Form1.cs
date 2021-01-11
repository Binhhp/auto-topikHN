using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolTopikHanoi.EF;
using ToolTopikHanoi.IIS.Domain;
using ToolTopikHanoi.Libraries;
using Zu.WebBrowser.AsyncInteractions;

namespace ToolTopikHanoi
{
    public partial class Form1 : Form
    {
        private readonly ICustomer db;
        public static int countIndex { get; set; }
        private static string id { get; set; }
        public Form1()
        {
            db = new Customer();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTable();
            LoadComboboxThread();
        }
        private void LoadTable()
        {
            var list = db.getPerson();
            dataGridView.DataSource = list;
            cbAdd.DisplayMember = "Email";
            cbAdd.ValueMember = "Id";
            cbAdd.DataSource = list;
        }
        private void LoadComboboxThread()
        {
            cbThread.Items.Clear();
            var list = new List<Date>()
            {
                new Date(){ Id = 1, Date1 = 1},
                new Date(){ Id = 2, Date1 = 2}
            };
            cbThread.DisplayMember = "Id";
            cbThread.ValueMember = "Date1";
            cbThread.DataSource = list;
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            DangKy formDangky = new DangKy();
            formDangky.id = null;
            formDangky.ShowDialog();
        }
        /// <summary>
        /// Run auto task 1
        /// </summary>
        /// <param name="selectIDs"></param>
        /// <param name="countTask"></param>
        /// <param name="sumTask"></param>
        /// <returns></returns>
        public async Task<int> OpenSeleniumTaskOne(string[] selectIDs, int countTask, int sumTask)
        {
            var driver = ChromeDriverOne._driver;
            await driver.Options().Timeouts.SetImplicitWait(TimeSpan.FromSeconds(2));
            for (var i = countTask; i < sumTask; i++)
            {
                int id = Convert.ToInt32(selectIDs[i]);
                var customer = db.GetInfoId(id);
                await driver.GoToUrl("http://topikhanoi.com/bbs/apply_reg.php?t_idx=16");
                Thread.Sleep(1500);
                string title = await driver.Title();
                while (true)
                {
                    if (title.Contains("응시원서 | :::하노이한국국제학교:::"))
                    {
                        try
                        {
                            if (customer.Topik == 2)
                            {
                                var checkTopik2 = await driver.FindElementById("a_level1");
                                if ((await checkTopik2.Selected()) == false)
                                {
                                    await ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked=true", CancellationToken.None, checkTopik2);
                                }
                            }

                            AutoItX3Lib.AutoItX3 autoItX3 = new AutoItX3Lib.AutoItX3();
                            autoItX3.WinActivate("Open");
                            string wanted_path = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), @"Images\1.jpg");
                            autoItX3.Send(wanted_path);
                            Thread.Sleep(1500);
                            autoItX3.Send("{ENTER}");

                            await (await driver.FindElementByXPath("//*[@id='u_email0']")).SendKeys(customer.Email);
                            await (await driver.FindElementByXPath("//*[@id='u_email1']")).SendKeys("gmail.com");
                            await (await driver.FindElementByXPath("//*[@id='u_pwd']")).SendKeys(customer.Password);
                            await (await driver.FindElementByXPath("//*[@id='u_pwd_confirm']")).SendKeys(customer.Password);
                            await (await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[7]/td[2]/input")).SendKeys(customer.NameEng);
                            await (await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[7]/td[4]/input")).SendKeys(customer.NameKor);
                            var comboboxYear = await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[8]/td[2]/select[1]");
                            await comboboxYear.SendKeys(customer.YearId.ToString());
                            var comboboxMonth = await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[8]/td[2]/select[2]");
                            await comboboxMonth.SendKeys(customer.MonthId.ToString());
                            var comboboxDate = await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[8]/td[2]/select[3]");
                            await comboboxDate.SendKeys(customer.DateId.ToString());
                            var comboboxAge = await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[8]/td[2]/select[4]");
                            await comboboxAge.SendKeys(customer.AgeId.ToString());
                            if (customer.Sex == true)
                            {
                                var checkboxSex1 = await driver.FindElementById("u_sex1");
                                await checkboxSex1.Click();
                            }
                            var comboboxCountry = await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[9]/td[2]/select");
                            await comboboxCountry.SendKeys(customer.Country);
                            await (await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[9]/td[4]/input")).SendKeys(customer.CMND);
                            var comboboxJob = await driver.FindElementByXPath("//*[@id='u_job']");
                            var jobName = db.getInfoJob(customer.JobId).Job1;
                            await comboboxJob.SendKeys(jobName);
                            await (await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[11]/td[2]/input")).SendKeys(customer.PhoneHome);
                            await (await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[11]/td[4]/input")).SendKeys(customer.PhoneNumber);
                            await (await driver.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[12]/td[2]/input")).SendKeys(customer.Address);
                            var comboboxTraniner = await driver.FindElementByXPath("//*[@id='u_motive']");
                            var traninerName = db.getInfoTrainer(customer.PhuongTienId).PhuongTien1;
                            await comboboxTraniner.SendKeys(traninerName);
                            var comboboxTraning = await driver.FindElementByXPath("//*[@id='u_purpose']");
                            var pupose = db.getInfoPurpose(customer.MucDichId).MucDich1;
                            await comboboxTraning.SendKeys(pupose);
                            if (sumTask - 1 > 0)
                            {
                                ChromeDriverOne.sumTask1 = sumTask;
                                if (sumTask - 1 > i)
                                {
                                    ChromeDriverOne.countTask1 = i + 1;
                                }
                                else
                                {
                                    ChromeDriverOne.countTask1 = i;
                                }
                            }
                            return 0;
                        }
                        catch
                        {
                            await driver.Close();
                            return 1;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// Run auto task 2
        /// </summary>
        /// <param name="selectIDs"></param>
        /// <param name="countTask"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<int> OpenSeleniumTaskHight(string[] selectIDs, int countTask, int count)
        {

            var driver2 = ChromeDriverHight._driver;
            await Task.Delay(1000);
            await driver2.Options().Timeouts.SetImplicitWait(TimeSpan.FromSeconds(2));
            for (var j = countTask; j < count; j++)
            {
                var id = Convert.ToInt32(selectIDs[j]);
                var item = db.GetInfoId(id);
                await driver2.GoToUrl("http://topikhanoi.com/bbs/apply_reg.php?t_idx=16");

                Thread.Sleep(1500);
                var title = await driver2.Title();
                while (true)
                {
                    if (title.Contains("응시원서 | :::하노이한국국제학교:::"))
                    {
                        try
                        {
                            if (item.Topik == 2)
                            {
                                var checkTopik1 = await driver2.FindElementById("a_level1");
                                if ((await checkTopik1.Selected()) == false)
                                {
                                    await ((IJavaScriptExecutor)driver2).ExecuteScript("arguments[0].checked=true", CancellationToken.None, checkTopik1);
                                }
                            }

                            AutoItX3Lib.AutoItX3 autoItX3 = new AutoItX3Lib.AutoItX3();
                            autoItX3.WinActivate("Open");
                            string wanted_path = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), @"Images\1.jpg");
                            autoItX3.Send(wanted_path);
                            Thread.Sleep(1500);
                            autoItX3.Send("{ENTER}");

                            await (await driver2.FindElementByXPath("//*[@id='u_email0']")).SendKeys(item.Email);
                            await (await driver2.FindElementByXPath("//*[@id='u_email1']")).SendKeys("gmail.com");
                            await (await driver2.FindElementByXPath("//*[@id='u_pwd']")).SendKeys(item.Password);
                            await (await driver2.FindElementByXPath("//*[@id='u_pwd_confirm']")).SendKeys(item.Password);
                            await (await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[7]/td[2]/input")).SendKeys(item.NameEng);
                            await (await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[7]/td[4]/input")).SendKeys(item.NameKor);
                            var comboboxYear = await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[8]/td[2]/select[1]");
                            await comboboxYear.SendKeys(item.YearId.ToString());
                            var comboboxMonth = await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[8]/td[2]/select[2]");
                            await comboboxMonth.SendKeys(item.MonthId.ToString());
                            var comboboxDate = await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[8]/td[2]/select[3]");
                            await comboboxDate.SendKeys(item.DateId.ToString());
                            var comboboxAge = await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[8]/td[2]/select[4]");
                            await comboboxAge.SendKeys(item.AgeId.ToString());
                            if (item.Sex == true)
                            {
                                var checkboxSex1 = await driver2.FindElementById("u_sex1");
                                await checkboxSex1.Click();
                            }
                            var comboboxCountry = await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[9]/td[2]/select");
                            await comboboxCountry.SendKeys(item.Country);
                            await (await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[9]/td[4]/input")).SendKeys(item.CMND);
                            var comboboxJob = await driver2.FindElementByXPath("//*[@id='u_job']");
                            var jobName = db.getInfoJob(item.JobId).Job1;
                            await comboboxJob.SendKeys(jobName);
                            await (await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[11]/td[2]/input")).SendKeys(item.PhoneHome);
                            await (await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[11]/td[4]/input")).SendKeys(item.PhoneNumber);
                            await (await driver2.FindElementByXPath("//*[@id='steptable_wrap2']/table/tbody/tr[12]/td[2]/input")).SendKeys(item.Address);
                            var comboboxTraniner = await driver2.FindElementByXPath("//*[@id='u_motive']");
                            var traninerName = db.getInfoTrainer(item.PhuongTienId).PhuongTien1;
                            await comboboxTraniner.SendKeys(traninerName);
                            var comboboxTraning = await driver2.FindElementByXPath("//*[@id='u_purpose']");
                            var pupose = db.getInfoPurpose(item.MucDichId).MucDich1;
                            await comboboxTraning.SendKeys(pupose);
                            if (count - 1 > 0)
                            {
                                ChromeDriverHight.sumTask2 = count;
                                if (count - 1 > j)
                                {
                                    ChromeDriverHight.countTask2 = j + 1;
                                }
                                else
                                {
                                    ChromeDriverHight.countTask2 = j;
                                }
                            }
                            return 0;
                        }
                        catch
                        {
                            await driver2.Close();
                            return 1;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return 0;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Text = "Loading...";
            btnStart.ForeColor = Color.Red;
            await StartAuto();
            btnStart.Text = "Start";
            btnStart.ForeColor = Color.Black;
        }
        public async Task StartAuto()
        {
            var thread = cbThread.SelectedValue.ToString();
            string[] selectIds = new string[countIndex];
            for (int i = 0; i < countIndex; i++)
            {
                selectIds[i] = listView.Items[i].SubItems[0].Text;
            }
            if (selectIds == null)
            {
                MessageBox.Show("Cần thêm thông tin để chạy!");
            }
            int count = selectIds.Count();
            ChromeDriverHight.selectIDs = selectIds;
            if (count > 1 && Convert.ToInt32(thread) > 1)
            {
                int countTask = (int)Math.Round((double)count / 2);
                int[] check = await Task.WhenAll(OpenSeleniumTaskOne(selectIds, 0, count), OpenSeleniumTaskHight(selectIds, countTask, count));
                string message = "";
                for (int i = 0; i < check.Count(); i++)
                {
                    if (check[i] == 1)
                    {
                        message += "Có lỗi xảy ra khi chạy auto " + (i + 1);
                    }
                }
                if (message != "")
                {
                    MessageBox.Show(message);
                }
                else
                {
                    MessageBox.Show("Nhập dữ liệu hoàn tất!");
                }
            }
            else
            {
                int[] checkError = await Task.WhenAll(OpenSeleniumTaskOne(selectIds, 0, count));
                foreach(var item in checkError)
                {
                    if(item == 1)
                    {
                        MessageBox.Show("Có lỗi xảy ra trong quá trình thực hiện!");
                    }
                }

            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var itemAdd = cbAdd.SelectedValue.ToString();
            listView.Items.Add(itemAdd);
            countIndex += 1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listView.Clear();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                ChromeDriverOne.check = "End";
                ChromeDriverOne.countTask1 = 0;
                ChromeDriverOne.sumTask1 = 0;

                ChromeDriverHight.check = "End";
                ChromeDriverHight.countTask2 = 0;
                ChromeDriverHight.sumTask2 = 0;

                var driver1 = ChromeDriverOne._driver;
                var driver2 = ChromeDriverHight._driver;
                if (driver1 == null && driver2 == null)
                {
                    MessageBox.Show("Kết thúc!");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnCountinue_Click(object sender, EventArgs e)
        {
            await CountinueAuto();
        }

        public async Task CountinueAuto()
        {
            try
            {
                ChromeDriverOne.check = "countinue";
                ChromeDriverHight.check = "countinue";
                var driverTask1 = ChromeDriverOne._driver;
                var countTask1 = ChromeDriverOne.countTask1;
                var sumTask1 = ChromeDriverOne.sumTask1;
                var driverTask2 = ChromeDriverHight._driver;
                var countTask2 = ChromeDriverHight.countTask2;
                var sumTask2 = ChromeDriverHight.sumTask2;

                if (driverTask1 != null && driverTask2 != null)
                {
                    await (await driverTask1.FindElementByXPath("//*[@id='btn_submit_register']")).Submit();
                    await Task.Delay(1000);
                    await (await driverTask2.FindElementByXPath("//*[@id='btn_submit_register']")).Submit();
                    await Task.Delay(8000);
                    while ((await driverTask1.Title()) != "응시원서 | :::하노이한국국제학교:::")
                    {
                        await driverTask1.Navigate().Refresh();
                    }
                    while ((await driverTask2.Title()) != "응시원서 | :::하노이한국국제학교:::")
                    {
                        await driverTask2.Navigate().Refresh();
                    }
                    await Task.Delay(1000);
                    ChromeDriverOne.check = "End";
                    ChromeDriverHight.check = "End";
                    await driverTask1.Close();
                    await driverTask1.Quit();
                    await driverTask2.Close();
                    await driverTask2.Quit();
                    if (countTask1 > 0 && countTask2 > 0 && sumTask2 > 0 && sumTask1 > 0)
                    {
                        var selectIDs = ChromeDriverHight.selectIDs;
                        if (selectIDs.Count() > 1)
                        {
                            var check = await Task.WhenAll(OpenSeleniumTaskOne(selectIDs, countTask1, sumTask1), OpenSeleniumTaskHight(selectIDs, countTask2, sumTask2));
                            string message = "";
                            for (int i = 0; i < check.Count(); i++)
                            {
                                if (message[i] == 1)
                                {
                                    message += "Có lỗi xảy ra khi auto task " + (i + 1);
                                }
                            }
                            if (message != "")
                            {
                                MessageBox.Show(message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Kết thúc!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kết thúc!");
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình thực hiện!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DangKy formDangKy = new DangKy();
            formDangKy.id = id;
            formDangKy.ShowDialog();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xóa không?", "Confirmation", MessageBoxButtons.OKCancel);
                if(result == DialogResult.OK)
                {
                    try
                    {
                        DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                        var idDelete = Convert.ToInt32(row.Cells[0].Value.ToString());
                        var resultDelete = db.DeleteData(idDelete);
                        if (resultDelete)
                        {
                            LoadTable();
                            MessageBox.Show("Xóa thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Lỗi xảy ra!");
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
