using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Models;

namespace Product.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext (DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Proizvod> Proizvod { get; set; }
        public virtual DbSet<Dobavljac> Dobavljac { get; set; }
        public virtual DbSet<TipProizvoda> TipProizvoda { get; set; }
        public virtual DbSet<JedinicaMere> JedinicaMere { get; set; }


     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proizvod>(entity =>
            {
                entity.HasOne(p => p.TipProizvoda).WithMany();
                entity.HasOne(p => p.JedinicaMere).WithMany();
            });

            modelBuilder.Entity<ProizvodDobavljac>(p => p.HasKey(x => new { x.DobavljacId, x.ProizvodId }));

            modelBuilder.Entity<Dobavljac>().HasData(new Dobavljac[]
            {
                new Dobavljac
                {
                    Id = 1,
                    Naziv = "Dobavljac 1",
                    PIB = "1221223344",
                    Napomena = "Napomena...."

                },
                new Dobavljac
                {
                    Id = 2,
                    Naziv = "Dobavljac 2",
                    PIB = "9874512"

                },
                new Dobavljac
                {
                    Id = 3,
                    Naziv = "Dobavljac 3",
                    PIB = "23974524123"

                }

            });

            modelBuilder.Entity<TipProizvoda>().HasData(new TipProizvoda[]
            {
                new TipProizvoda
                {
                    Id = 1,
                    Naziv = "Tip 1"

                },
                new TipProizvoda
                {
                    Id = 2,
                    Naziv = "Tip 2"

                },
                new TipProizvoda
                {
                    Id = 3,
                    Naziv = "Tip 3"

                },
                 new TipProizvoda
                {
                    Id = 4,
                    Naziv = "Tip 4"

                },

            });

            modelBuilder.Entity<JedinicaMere>().HasData(new JedinicaMere[]
            {
                new JedinicaMere
                {
                    Id = 1,
                    Naziv = "Kilogram"

                },
                new JedinicaMere
                {
                    Id = 2,
                    Naziv = "Kilogram"
                },
                new JedinicaMere
                {
                    Id = 3,
                    Naziv = "Gram"

                },
                 new JedinicaMere
                {
                     Id = 4,
                    Naziv = "Miligram"

                }

            });

        }
    }
}
