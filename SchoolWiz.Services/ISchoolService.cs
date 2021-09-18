using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface ISchoolService
    {
        //Only one school record will ever exist
        School GetSchool();
        Task CreateSchoolAsync(School school);
        Task EditSchool(School school);
        Task Delete();
    }
}
