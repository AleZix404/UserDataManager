namespace UserDataManager.EntityFramework.DTO
{
    public class AdressDataInsertDTO
    {
        public int? IdAdress { get; set; }
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
    }
    /*
    {
        "Street": "Independencia Nacional",
        "Suite": "",
        "City": "Asuncion",
        "Zipcode": "1234"
    }
     */
}
