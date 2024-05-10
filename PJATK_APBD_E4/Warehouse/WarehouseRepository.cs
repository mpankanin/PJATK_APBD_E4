using System.Data.SqlClient;

namespace PJATK_APBD_E4.Warehouse;


public class WarehouseRepository : IWarehouseRepository
{
    
    private IConfiguration _configuration;
    
    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void AddProductWarehouse(ProductWarehouse productWarehouse)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        using var command = new SqlCommand("INSERT INTO Warehouse (IdProduct, IdWarehouse, Amount, CreatedAt) VALUES (@IdProduct, @IdWarehouse, @Amount, @CreatedAt)", connection);
        command.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
        command.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
        command.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
        command.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);
        command.ExecuteNonQuery();
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
    
}