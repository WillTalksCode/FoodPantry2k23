namespace FoodPantry2k23.Models
{
    public class Referral
    {
        public int ReferralID {  get; set; }
        public int HouseHoldID {  get; set; }
        public Service Service { get; set; }
        public DateTime ReferralDate { get; set; }
    }

    public enum Service
    {
        FoodAssitance,
        UtilityAssistance,
        RentalAssistance,
        HealthAssistance,
        ChildCare,
        Legal
    }
}
