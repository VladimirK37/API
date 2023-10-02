namespace SPEEDBOXAPI.Model
{
    public class ShippingCost
    {
        public List<Tariff_Codes> Tariff_Codes { get; set; }
    }
    public class Tariff_Codes
    {
        public int Tariff_Code { get; set; }
        public string Tariff_Name { get; set; }

        //public string Tariff_Description { get; set; }

        //public int Delivery_Mode { get; set; }

        public float Delivery_Sum { get; set; }

        //public int period_min { get; set; }

        //public int period_max { get; set; }

        //public int calendar_min { get; set; }

        //public int calendar_max { get; set; }

    }

}
