namespace PJATK_APBD_E4.Warehouse;

public interface IWarehouseRepository
{
    int AddProductWarehouse(int idWarehouse, int idProduct, int idOrder, int amount, double price, DateTime createdAt);
    bool WarehouseExists(int id);
    bool OrderExists(int orderId);
    int AddProductWarehouseSP(int idProduct, int idWarehouse, int amount, DateTime createdAt);
}
