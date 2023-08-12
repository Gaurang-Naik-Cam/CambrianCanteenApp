namespace MyCanteenAPI.Models
{
    public class InputCart
    {
        public int studentId { get; set;}
        public int foodItemId { get; set; }
        public bool fullEmptyCart { get; set; }

        public InputCart()
        {
            this.fullEmptyCart = false;
        }
    }
}
