using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserDataManager.EntityFramework.Models
{
    public class UserData
    {
        public class Address
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int IdAdress { get; set; }
            public string Street { get; set; }
            public string Suite { get; set; }
            public string City { get; set; }
            public string Zipcode { get; set; }
        }

        public class UserDataResponse
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            
            public int? IdAdress { get; set; }
            [ForeignKey("IdAdress")]
            public Address? Address { get; set; }

            public string Phone { get; set; }
            public string Website { get; set; }
        }

    }
}
