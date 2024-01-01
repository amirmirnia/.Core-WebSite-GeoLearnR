using Data.TMU.Model;
using Data.TMU.Model.Course;
using Data.TMU.Model.Gallery;
using Data.TMU.Model.Image;
using Data.TMU.Model.Menu;
using Data.TMU.Model.Nav;
using Data.TMU.Model.News;
using Data.TMU.Model.Page;
using Data.TMU.Model.Slider;
using Data.TMU.Model.Wallet;
using Data.TMU.Permissions;
using Data.TMU.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.TMU.Context
{
    public class ContextTMU : DbContext
    {

        public ContextTMU(DbContextOptions<ContextTMU> options) : base(options)
        {

        }
        public DbSet<News> News { get; set; }
        public DbSet<FileNews> FileNews { get; set; }
        public DbSet<permission> Permissions { get; set; }
        public DbSet<Model.User> Users { set; get; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Navbar> Navbars { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<FileGallery> FileGalleries { get; set; }
        public DbSet<FilePage> FilePages { get; set; }
        public DbSet<ImagePage> imagePages { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<FileCourse> FileCourse { get; set; }

        public DbSet<CourseComment> courseComments { get; set; }
        public DbSet<Wallet> Wallet { get; set; }

        public DbSet<walletType> walletType { get; set; }
        public DbSet<UserCourse> UserCourse { get; set; }


        #region modelBuilder

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }


        #endregion



    }
}
