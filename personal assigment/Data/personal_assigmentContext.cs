using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using personal_assigment.Models;
using MySql.Data.MySqlClient;

namespace personal_assigment.Data
{
    public class personal_assigmentContext : DbContext
    {
        public personal_assigmentContext (DbContextOptions<personal_assigmentContext> options)
            : base(options)
        {
        }

        

        public DbSet<personal_assigment.Models.Student> Student { get; set; } = default!;
    }
}
