using PJATK_APBD_E4.Order;
using PJATK_APBD_E4.Product;

namespace PJATK_APBD_E4.Warehouse;

public class WarehouseService : IWarehouseService
{
    
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    
    public WarehouseService(IWarehouseRepository warehouseRepository, IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _warehouseRepository = warehouseRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }
    
    public int AddProductWarehouse(ProductWarehouse productWarehouse)
    {
        //checking if product exists
        if(_productRepository.ProductExists(productWarehouse.IdProduct) == false)
        {
            throw new Exception("Product does not exist");
        }
        
        //checking if warehouse exists
        if(_warehouseRepository.WarehouseExists(productWarehouse.IdWarehouse) == false)
        {
            throw new Exception("Warehouse does not exist");
        }
        
        //checking if amount is greater than 0
        if(productWarehouse.Amount <= 0)
        {
            throw new Exception("Amount must be greater than 0");
        }
        
        //checking if order exists
        if(_orderRepository.OrderExists(productWarehouse.IdProduct, productWarehouse.Amount, productWarehouse.CreatedAt) == false)
        {
            throw new Exception("Order does not exist");
        }
        
        //checking if order was realised
        var orderId = _orderRepository.GetOrderId(productWarehouse.IdProduct, productWarehouse.Amount, productWarehouse.CreatedAt);
        if(_warehouseRepository.OrderExists(orderId))
        {
            throw new Exception("Order was realised");
        }
        
        //count order price
        var price = _productRepository.GetProductPrice(productWarehouse.IdProduct);
        var totalPrice = price * productWarehouse.Amount;
        
        //adding product to warehouse
        var insertedId = _warehouseRepository.AddProductWarehouse(productWarehouse.IdWarehouse, 
            productWarehouse.IdProduct, 
            orderId, 
            productWarehouse.Amount, 
            totalPrice, 
            DateTime.Now);

        return insertedId;
    }
    
public int AddProductWarehouseSP(ProductWarehouse productWarehouse)
{
    return _warehouseRepository.AddProductWarehouseSP(productWarehouse.IdProduct, productWarehouse.IdWarehouse,
        productWarehouse.Amount, productWarehouse.CreatedAt);
}
    
}