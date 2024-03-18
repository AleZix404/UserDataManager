using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserDataManager.EntityFramework.Models
{
    public class UserDataDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        /*
     Update
    {
        "Id":"40",
        "Name":"Ariel",
        "Email":"Ariel625@gmail.com",
        "WebSite":"www.Ariel.com"
    }
     */
    }
}
