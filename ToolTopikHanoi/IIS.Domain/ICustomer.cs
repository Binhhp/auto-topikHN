using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolTopikHanoi.EF;

namespace ToolTopikHanoi.IIS.Domain
{
    public interface ICustomer
    {
        Person GetInfoId(int id);
        bool InsertData(Person model);
        bool UpdateData(Person model);
        bool DeleteData(int id);
        List<Date> getDate();
        List<Month> getMonth();
        List<Year> getYear();
        List<Age> getAge();
        List<Job> getJob();
        List<PhuongTien> getTrainer();
        List<MucDich> getPurpose();
        DataTable getPerson();
        Job getInfoJob(int? id);
        PhuongTien getInfoTrainer(int? id);
        MucDich getInfoPurpose(int? id);
    }
}
