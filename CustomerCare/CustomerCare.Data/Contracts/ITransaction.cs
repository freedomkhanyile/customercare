using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCare.Data.Contracts
{
    public interface ITransaction: IDisposable
    {
        void Commit();
        void Rollback();
    }
}
