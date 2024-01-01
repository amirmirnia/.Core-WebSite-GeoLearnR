using Data.TMU.Model.Course;
using Data.TMU.Model.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.ITMUService
{
    public interface ICourse
    {
        ListCourseViewModel GetAllCourse(int pageid=1,string filtertitel=null,string tag=null, int take=0,string codeAuthor=null,string mony=null);

        ListCourseVideoViewModel GetAllVideoCourse(int pageid = 1, string filtertitel = null, int take = 0,int idcourse=0);
        int GetCoutVideoCourse(int idcourse);
        Tuple<List<CourseComment>, int> GetAllCourseComment(int courseid, int pageid = 1);
        void AddCoursUser(string idcode,int Idcourse);
        bool IsBuyCurseuser(string idcode, int Idcourse);


        ListCourseUserViewModel GetAllCourseUser(int pageid = 1, int take = 0, string CodeId = null);
    }
}
