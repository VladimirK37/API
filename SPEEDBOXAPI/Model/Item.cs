namespace SPEEDBOXAPI.Model
{
    public class Item
    {
        //public DateTime date { get; set; }  = DateTime.Now;

        public int type { get; set; } = 1;

        public int currency { get; set; } = 1;

        //public string tariff_code { get; set; }

        public Location from_location { get; set; }

        public Location to_location { get; set; }

        //public Services services { get; set; }

        public Package packages { get; set; }
    }
}
