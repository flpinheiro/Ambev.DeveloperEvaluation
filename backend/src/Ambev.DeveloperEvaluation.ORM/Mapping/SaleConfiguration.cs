﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.Number).UseSerialColumn().IsRequired();
        builder.Property(s => s.TotalValue).IsRequired();
        builder.Property(s => s.Date).IsRequired();
        builder.Property(p => p.Status).IsRequired().HasDefaultValue(SaleStatus.Active);

        builder.HasIndex(s => s.Number).IsUnique();
    }
}
