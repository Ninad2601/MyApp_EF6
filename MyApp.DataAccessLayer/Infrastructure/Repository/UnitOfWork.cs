using MyApp.DataAccessLayer.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }
        public ICartRepository Cart { get; private set; }
        public IApplicationRepository ApplicationRepository { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailsRepository OrderDetail { get; private set; }
        

        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            Cart= new CartRepository(context);
            ApplicationRepository = new ApplicationUserRepository(context);
            OrderHeader = new OrderHeaderRepository(context);
            OrderDetail = new OrderDetailRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
