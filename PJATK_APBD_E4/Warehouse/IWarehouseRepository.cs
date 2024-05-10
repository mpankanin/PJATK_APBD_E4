namespace PJATK_APBD_E4.Warehouse;

public interface IWarehouseRepository
{
    void AddProductWarehouse(ProductWarehouse productWarehouse);
    bool WarehouseExists(int id);
    bool OrderExists(int orderId);
}