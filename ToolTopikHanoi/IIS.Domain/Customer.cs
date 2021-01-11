using IIS.Migrations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolTopikHanoi.EF;

namespace ToolTopikHanoi.IIS.Domain
{
    public class Customer : ICustomer
    {
        private readonly Model _context;
        public Customer()
        {
            _context = new Model();
        }
        public List<Date> getDate()
        {
            return _context.Dates.ToList();
        }
        public List<Month> getMonth()
        {
            return _context.Months.ToList();
        }
        public List<Year> getYear()
        {
            return _context.Years.ToList();
        }
        public List<Age> getAge()
        {
            return _context.Ages.ToList();
        }
        public List<Job> getJob()
        {
            return _context.Jobs.ToList();
        }
        public List<PhuongTien> getTrainer()
        {
            return _context.PhuongTiens.ToList();
        }
        public List<MucDich> getPurpose()
        {
            return _context.MucDiches.ToList();
        }
        public DataTable getPerson()
        {
            return DataProvider.ExcuteGetData("SELECT * FROM Person", false);
        }
        public Job getInfoJob(int? id)
        {
            return _context.Jobs.FirstOrDefault(x => x.Id == id);
        }
        public PhuongTien getInfoTrainer(int? id)
        {
            return _context.PhuongTiens.FirstOrDefault(x => x.Id == id);
        }
        public MucDich getInfoPurpose(int? id)
        {
            return _context.MucDiches.FirstOrDefault(x => x.Id == id);
        }
        public Person GetInfoId(int id)
        {
            return _context.People.FirstOrDefault(x => x.Id == id);
        }
        public bool InsertData(Person model)
        {
            try
            {
                _context.People.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateData(Person model)
        {
            var record = _context.People.FirstOrDefault(x => x.Email.Equals(model.Email));
            if (record != null)
            {
                record.Topik = model.Topik;
                record.Password = model.Password;
                record.NameEng = model.NameEng;
                record.NameKor = model.NameKor;
                record.DateId = model.DateId;
                record.MonthId = model.MonthId;
                record.YearId = model.YearId;
                record.AgeId = model.AgeId;
                record.Sex = model.Sex;
                record.Country = model.Country;
                record.CMND = model.CMND;
                record.JobId = model.JobId;
                record.PhoneHome = model.PhoneHome;
                record.PhoneNumber = model.PhoneNumber;
                record.Address = model.Address;
                record.PhuongTienId = model.PhuongTienId;
                record.MucDichId = model.MucDichId;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteData(int id)
        {
            try
            {
                var result = _context.People.FirstOrDefault(x => x.Id == id);
                _context.People.Remove(result);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
