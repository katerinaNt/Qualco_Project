using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;

namespace DbContextN
{
    public class DataBaseFactory:IDesignTimeDbContextFactory<DataBase>
    {
        public DataBase CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataBase>();
            optionsBuilder.UseSqlServer("Data Source=Lancelot;Initial catalog=QualcoOne;Integrated security=True;");
            return new DataBase(optionsBuilder.Options);
        }
        
    }
}
