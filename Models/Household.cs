namespace FoodPantry2k23.Models
{
    public class Household
    {
        public int HouseHoldID { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? StateProvince { get; set; }
        public string? ZipCode { get; set; }
        public bool ConsentFormOnFile {  get; set; }
        public bool VerbalConsentGiven {  get; set; }
        public DateTime? ConsentFormSigned { get; set; }
        public DateTime? VerbalConsentGivenOn { get; set; }
        public string? AdminNotes { get; set; }

    }
}
