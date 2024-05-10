using System.Data;
using System.Data.SqlClient;

namespace PJATK_APBD_E4.Warehouse;


public class WarehouseRepository : IWarehouseRepository
{
    
    private IConfiguration _configuration;
    
    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public int AddProductWarehouse(int idWarehouse, int idProduct, int idOrder, int amount, double price, DateTime createdAt)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        using var command = new SqlCommand("INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt); SELECT SCOPE_IDENTITY();", connection);
        command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
        command.Parameters.AddWithValue("@IdProduct", idProduct);
        command.Parameters.AddWithValue("@IdOrder", idOrder);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@Price", price);
        command.Parameters.AddWithValue("@CreatedAt", createdAt);
        return Convert.ToInt32(command.ExecuteScalar());
    }
    
    public bool WarehouseExists(int id)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        using var command = new SqlCommand("SELECT COUNT(*) FROM Warehouse WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        return (int)command.ExecuteScalar() > 0;
    }
    
    public bool OrderExists(int orderId)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        using var command = new SqlCommand("SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @IdOrder", connection);
        command.Parameters.AddWithValue("@IdOrder", orderId);
        return (int)command.ExecuteScalar() > 0;
    }
    
    public int AddProductWarehouseSP(int idProduct, int idWarehouse, int amount, DateTime createdAt)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        using var command = new SqlCommand("AddProductToWarehouse", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@IdProduct", idProduct);
        command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@CreatedAt", createdAt);
        return Convert.ToInt32(command.ExecuteScalar());
    }
    
}