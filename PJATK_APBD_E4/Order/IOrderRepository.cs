namespace PJATK_APBD_E4.Order;

public interface IOrderRepository
{
    bool OrderExists(int productId, int amount, DateTime requestDate);
    int GetOrderId(int productId, int amount, DateTime requestDate);
    
    void UpdateOrderFullfilled(int orderId);
    
}