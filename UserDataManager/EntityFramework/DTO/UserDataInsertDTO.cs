using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserDataManager.EntityFramework.DTO
{
    public class UserDataInsertDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int? IdAdress { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        /*
    {
        "Name":"Ariel",
        "Username":"Ariel",
        "IdAdress":"2",
        "Phone":"0985789456",
        "Email":"Ariel625@gmail.com",
        "WebSite":"www.Ariel.com"
    }
     */
    }
}
