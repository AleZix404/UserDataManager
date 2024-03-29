﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static UserDataManager.EntityFramework.Models.UserData;

namespace UserDataManager.EntityFramework.DTO
{
    public class UserDataInsertDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int? IdAdress { get; set; }
        public Address? Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}
