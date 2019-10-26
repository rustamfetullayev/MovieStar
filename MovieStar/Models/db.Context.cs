﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieStar.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MSEntities : DbContext
    {
        public MSEntities()
            : base("name=MSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<DislikedFilm> DislikedFilms { get; set; }
        public virtual DbSet<Dublaj> Dublajs { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Film_to_Actor> Film_to_Actor { get; set; }
        public virtual DbSet<Film_to_Country> Film_to_Country { get; set; }
        public virtual DbSet<Film_to_Director> Film_to_Director { get; set; }
        public virtual DbSet<Film_to_Genre> Film_to_Genre { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<LikedFilm> LikedFilms { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsComment> NewsComments { get; set; }
        public virtual DbSet<SavedFilm> SavedFilms { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}