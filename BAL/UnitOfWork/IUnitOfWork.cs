using BAL.Database.Company;
using BAL.Database.DatabaseIdentity;
using BAL.Database.DatabaseModels;
using BAL.Database.RequestCompany;
using BAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> users { get; }
        IGenericRepository<UserInfo> userInfo { get; }
        IGenericRepository<Expense> expenses { get; }
        IGenericRepository<Leave> leaves { get; }
        IGenericRepository<CompanyData> companyData { get; }
        IGenericRepository<Education> education { get; }
        IGenericRepository<Experience> experience { get; }
        IGenericRepository<NewCompanyRequest> reqComp { get; }
        Task SaveAsync();
    }
}
