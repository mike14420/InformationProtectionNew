using System;

namespace WMC.Core.DataProvider2008
{
    /// <summary>
    /// Summary description for IDataProvider.
    /// </summary>
    public interface IDataProvider
    {

        void StartTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}