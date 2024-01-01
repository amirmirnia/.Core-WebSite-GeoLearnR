using Core.TMU.Img;
using Core.TMU.Service.ITMUService;
using Data.TMU.Context;
using Data.TMU.Model.Course;
using Data.TMU.Model.News;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.TMUService
{
    public class CourseRepository : ICourse
    {
        private ContextTMU _db;
        private IUser _user;
        public CourseRepository(ContextTMU db, IUser user)
        {
            _db = db;
            _user=user;
        }

        public void AddCoursUser(string idcode, int Idcourse)
        {
            UserCourse userCourse = new UserCourse()
            {
                CourseId = Idcourse,
                userid = _user.GetiduserWhitidcode(idcode),
                CodeId = idcode
        };
            _db.Add(userCourse);
            _db.SaveChanges();

        }

        public ListCourseViewModel GetAllCourse(int pageid = 1, string filtertitel = null, string tag = null, int take = 0, string codeAuthor = null, string mony = null)
        {
            IQueryable<Course> result = _db.Courses;
            if (!string.IsNullOrEmpty(tag) || !string.IsNullOrEmpty(filtertitel))
            {
                result = result.Where(p => p.name.Contains(filtertitel) || p.Tag.Contains(tag));
            }
            if (!string.IsNullOrEmpty(codeAuthor))
            {
                result = result.Where(p => p.author == codeAuthor);
            }
            if (mony!=null)
            {
                result = result.Where(p => p.Lock == bool.Parse(mony));
            }
            



            int skip = (pageid - 1) * take;

            var count = result.Select(n => new Categorycorse
            {
                author=n.author,
                Status=n.Status,
                Date=n.Date,
                Id=n.Id,
                img=n.img,
                name=n.name,
                price=n.price,
                Titel=n.Titel,
                minet=n.minet,
                secend=n.secend,
                Time=n.Time,
                Lock=n.Lock

            }).Count();
            var Listcourse = result.Select(n => new Categorycorse
            {
                author = n.author,
                Status = n.Status,
                Date = n.Date,
                Id = n.Id,
                img = n.img,
                name = n.name,
                price = n.price,
                Titel = n.Titel,
                minet = n.minet,
                secend = n.secend,
                Time = n.Time,
                Lock = n.Lock

            }).OrderByDescending(p => p.Id).Skip(skip).Take(take).ToList();
            return new ListCourseViewModel()
            {
                Listcourse = Listcourse,
                CountPage = (count / take) + 1,
                IdPage = pageid
            };
        }

        public ListCourseUserViewModel GetAllCourseUser(int pageid = 1, int take = 0, string CodeId = null)
        {
            IQueryable<UserCourse> result = _db.UserCourse;
         
            if (!string.IsNullOrEmpty(CodeId))
            {
                result = result.Where(p => p.CodeId == CodeId);
            }




            int skip = (pageid - 1) * take;

            var count = result.Include(n=>n.Course).Select(n => new CourseUserViewModel
            {
                author=n.CodeId,
                name=n.Course.name,
                img=n.Course.img,
                IdCourse=n.CourseId

            }).Count();
            var ListcourseUser = result.Select(n => new CourseUserViewModel
            {
                author = n.CodeId,
                name = n.Course.name,
                img = n.Course.img,
                IdCourse = n.CourseId

            }).OrderByDescending(p => p.IdCourse).Skip(skip).Take(take).ToList();
            return new ListCourseUserViewModel()
            {
                CourseUserViewModel = ListcourseUser,
                CountPage = (count / take) + 1,
                IdPage = pageid
            };
        }

        public Tuple<List<CourseComment>, int> GetAllCourseComment(int courseid, int pageid = 1)
        {
            int take = 9;
            int skip = (pageid - 1) * take;
            int pagecount= _db.courseComments.Where(p => p.IsAllow == false && p.idC == courseid).Count() / take;
            return Tuple.Create(_db.courseComments.Where(p => p.IsAllow == false && p.idC == courseid).Skip(skip).Take(take).ToList(), pagecount);


        }

        public ListCourseVideoViewModel GetAllVideoCourse(int pageid = 1, string filtertitel = null, int take = 0, int idcourse = 0)
        {
            IQueryable<FileCourse> result = _db.FileCourse;
            if ( !string.IsNullOrEmpty(filtertitel))
            {
                result = result.Where(p => p.name.Contains(filtertitel));
            }
            result = result.Where(p => p.IdC==idcourse);

            int skip = (pageid - 1) * take;

            var count = result.Select(n => new FileCourse
            {
               author=n.author,
               Date=n.Date,
               file=n.file,
               hour=n.hour,
               IdC=n.IdC,
               Lock=n.Lock,
               minet=n.minet,
               name=n.name,
               secend=n.secend,
               IdFC=n.IdFC
               

            }).Count();
            var ListcourseVideo = result.Select(n => new FileCourse
            {
                author = n.author,
                Date = n.Date,
                file = n.file,
                hour = n.hour,
                IdC = n.IdC,
                Lock = n.Lock,
                minet = n.minet,
                name = n.name,
                secend = n.secend,
                IdFC = n.IdFC

            }).OrderByDescending(p => p.IdFC).Skip(skip).Take(take).ToList();
            return new ListCourseVideoViewModel()
            {
                ListcourseVideo = ListcourseVideo,
                CountPage = (count / take) + 1,
                IdPage = pageid
            };
        }

        public int GetCoutVideoCourse(int idcourse)
        {
            return _db.FileCourse.Where(p=>p.IdC==idcourse).Count();
        }

        public bool IsBuyCurseuser(string idcode, int Idcourse)
        {
            return _db.UserCourse.Any(p => p.CourseId == Idcourse && p.userid == _user.GetiduserWhitidcode(idcode));
        }
    }
}
