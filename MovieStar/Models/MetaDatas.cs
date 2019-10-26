using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieStar.Models
{
    [MetadataType(typeof(FilmMetaData))]
    public partial class Film
    {
        private class FilmMetaData
        {
            [Required(ErrorMessage = "Name must fill.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Year must select.")]
            public Nullable<System.DateTime> Year { get; set; }

            [Required(ErrorMessage = "IMDB must fill.")]
            public Nullable<decimal> IMDB { get; set; }

            [Required(ErrorMessage = "Iframe must fill.")]
            public string Iframe { get; set; }

            [Required(ErrorMessage = "Trailer must fill.")]
            public string Trailer { get; set; }

            [Required(ErrorMessage = "About must fill.")]
            public string Aboout { get; set; }

            [Required(ErrorMessage = "Length must fill.")]
            public string Length { get; set; }
        }
    }

    [MetadataType(typeof(ActorMetaData))]
    public partial class Actor
    {
        private class ActorMetaData
        {
            [Required(ErrorMessage = "Name must fill.")]
            public string Name { get; set; }
        }
    }

    [MetadataType(typeof(DirectorMetaData))]
    public partial class Director
    {
        private class DirectorMetaData
        {
            [Required(ErrorMessage = "Name must fill.")]
            public string Name { get; set; }
        }
    }

    [MetadataType(typeof(NewsMetaData))]
    public partial class News
    {
        private class NewsMetaData
        {
            [Required(ErrorMessage = "Title must fill.")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Content must fill.")]
            public string Content { get; set; }

            [Required(ErrorMessage = "Author name must fill.")]
            public string Author { get; set; }

            [Required(ErrorMessage = "PostDate must fill.")]
            public Nullable<System.DateTime> PostDate { get; set; }
        }
    }

    [MetadataType(typeof(DublajMetaData))]
    public partial class Dublaj
    {
        private class DublajMetaData
        {
            [Required(ErrorMessage = "Name must fill.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Color must fill.")]
            public string Color { get; set; }
        }
    }

    [MetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {
        private class CategoryMetaData
        {
            [Required(ErrorMessage = "Name must fill.")]
            public string Name { get; set; }
        }
    }

    [MetadataType(typeof(GenreMetaData))]
    public partial class Genre
    {
        private class GenreMetaData
        {
            [Required(ErrorMessage = "Name must fill.")]
            public string Name { get; set; }
        }
    }

    [MetadataType(typeof(CountryMetaData))]
    public partial class Country
    {
        private class CountryMetaData
        {
            [Required(ErrorMessage = "Name must fill.")]
            public string Name { get; set; }
        }
    }
}