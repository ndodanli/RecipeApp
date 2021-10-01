using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RecipeApp.Models
{
    public class AccountModel : IAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Username"), BsonRequired]
        public string Username { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("RoleId"), BsonRequired]
        public string RoleId { get; set; }
    }
}
