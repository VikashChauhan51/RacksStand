﻿using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IInventoryRepository : IRepository<Inventory, string>
    {

    }
}