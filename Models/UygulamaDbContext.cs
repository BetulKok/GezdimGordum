using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GezdimGördüm.Models
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options ) :base(options)
        {

        }

        public DbSet<Paylasim> Paylasimlar { get; set; }
    }
}
