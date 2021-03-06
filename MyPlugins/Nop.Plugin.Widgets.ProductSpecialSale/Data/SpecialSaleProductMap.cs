﻿using Nop.Plugin.Widgets.ProductSpecialSale.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductSpecialSale.Data
{
    public class SpecialSaleProductMap : EntityTypeConfiguration<SpecialSaleProduct>
    {
        public SpecialSaleProductMap()
        {
            this.ToTable("SpecialSaleProduct");
            this.HasKey(c => c.Id);
            this.Property(p => p.Price).HasPrecision(18, 4);
            this.Property(p => p.OriginalPrice).HasPrecision(18, 4);
            this.HasRequired(s => s.SpecialSaler).WithMany(s => s.SpecialSaleProducts).HasForeignKey(s => s.SpecialSalerId);
        }
    }
}
