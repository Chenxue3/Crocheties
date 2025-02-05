/*
this file is responsible for creating the database context class.
Context class is a class that is derived from DbContext class, used to perform database operations.
*/
using System;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class AuctionDbContext : DbContext
// DbContext is a class that is provided by Entity Framework Core to work with the database. 
//It is a bridge between your domain or entity classes and the database. 
//DbContext is the primary class that is responsible for interacting with the database.
{
    public AuctionDbContext(DbContextOptions options) : base(options)
    {
    }

    // DbSet represents an entity set that can be used for the database operations.
    public DbSet<Auction> Auctions { get; set; } 
}
