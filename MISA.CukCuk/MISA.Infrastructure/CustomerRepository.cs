using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure {
    public class CustomerRepository :BaseRepository<Customer>,ICustomerRepository {

        public CustomerRepository(IConfiguration configuration):base(configuration) {
        }

        public Customer GetCustomerByCode(string customerCode) {
            var customer = _dbConnection.Query<Customer>($"Select * from {_tableName} where CustomerCode='{customerCode}'").FirstOrDefault();
            return customer;
        }
    }
}
