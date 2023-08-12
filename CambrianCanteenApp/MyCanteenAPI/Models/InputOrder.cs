namespace MyCanteenAPI.Models
{
    public class InputOrder
    {
        public List<int> foodItems { get; set; }
        public int studentId { get; set; }
        public double total { get; set; }
        public double tax { get; set; }

        public InputOrder()
        {
            foodItems = new List<int>();
        }

    }
}
