using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Models
{
    public class RecipeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name"), BsonRequired]
        public string Name { get; set; }

        [BsonElement("ImagePath"), BsonRequired]
        public string ImagePath { get; set; }

        [BsonElement("Slug"), BsonRequired]
        public string Slug { get; set; }

        [BsonElement("View"), BsonRequired]
        public int View { get; set; }

        [BsonElement("Servings"), BsonRequired]
        public int Servings { get; set; } = -1;

        [BsonElement("Directions"), BsonRequired]
        public string Directions { get; set; }

        [BsonElement("Ingredients"), BsonRequired]
        public string[] Ingredients { get; set; }

        [BsonElement("CategoryId"), BsonRequired]
        public string CategoryId { get; set; }
    }
}
