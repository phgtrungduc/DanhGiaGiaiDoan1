using Microsoft.AspNetCore.Mvc;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;
using MISA.ApplicationCore;
using MISA.ApplicationCore.interfaces;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers {
    public class CustomersController : BaseEntityController<Customer> {
        ICustomerService _customerService;
        public CustomersController(ICustomerService customerService):base(customerService) {
            _customerService = customerService;
        }
    }
}
