﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    internal interface IApplicationDbContext
    {
         DbSet<Student> Students { get; set; }
    }
}
