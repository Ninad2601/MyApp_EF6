﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.DataAccessLayer.Infrastructure.IRepository
{
    public interface IOrderHeaderRepository:IRepository<OrderHeader>
    {
        void Update (OrderHeader orderHeader);
        void UpdateStatus (int Id,string orderStaus,string? paymentStaus=null); //Where id= OrderHeaderId
    }
}