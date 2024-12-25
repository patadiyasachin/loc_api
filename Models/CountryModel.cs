namespace loc_api_crud.Models
{
    public class CountryModel
    {
        public int? CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }

    public class CountryDropDown
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }

    public class StateDropDown
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
}
