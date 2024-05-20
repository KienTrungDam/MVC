﻿using MVC.DataAccess.Data;
using MVC.DataAccess.Repository.IRepository;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db) // lay tu ham tao trong repository
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
