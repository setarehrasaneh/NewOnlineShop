﻿using MyOnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineShop.Domain.Repositories
{
    public interface IDiscountRepository
    {
        Discount FindDiscount(string code);
    }
}