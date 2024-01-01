using Core.TMU.Service.ITMUService;
using Core.TMU.Service.TMUService;
using Data.TMU.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Context
{
    public class UnitOfWork : IDisposable
    {

        private ContextTMU db;
        private IUser _User;

        public IUser User
        {
            get
            {
                if (_User == null)
                {
                    _User = User;
                }
                return _User;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
