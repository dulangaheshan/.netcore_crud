using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testing2.Model;

namespace Testing2.DataProvider
{
    interface IDataProvider
    {
      User GetUser(int CustomerId);
      Boolean AddUser(User user);
    }
}
