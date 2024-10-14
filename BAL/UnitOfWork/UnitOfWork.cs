using BAL.Database.Company;
using BAL.Database.DatabaseContext;
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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Database_Context _context;

        public IGenericRepository<User> users { get; private set; }
        public IGenericRepository<UserInfo> userInfo { get; private set; }

        public IGenericRepository<Expense> expenses { get; private set; }

        public IGenericRepository<Leave> leaves { get; private set; }

        public IGenericRepository<CompanyData> companyData { get; private set; }

        public IGenericRepository<Education> education { get; private set; }

        public IGenericRepository<Experience> experience { get; private set; }

        public IGenericRepository<NewCompanyRequest> reqComp { get; private set; }

        public UnitOfWork(Database_Context context)
        {
            _context = context;

            users = new GenericRepository<User>(context);

            userInfo = new GenericRepository<UserInfo>(context);

            expenses = new GenericRepository<Expense>(context);

            leaves = new GenericRepository<Leave>(context);

            companyData = new GenericRepository<CompanyData>(context);

            education = new GenericRepository<Education>(context);

            experience = new GenericRepository<Experience>(context);

            reqComp = new GenericRepository<NewCompanyRequest>(context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
