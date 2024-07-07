namespace upskilling.Models
{
   public class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public ICollection<Product>? products { get; set; }
}


}
