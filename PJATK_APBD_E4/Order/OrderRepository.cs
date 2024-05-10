using System.Data.SqlClient;

namespace PJATK_APBD_E4.Order;

public class OrderRepository : IOrderRepository
{
    private IConfiguration _configuration;

    public OrderRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool OrderExists(int productId, int amount, DateTime requestDate)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        using var command = new SqlCommand("SELECT COUNT(*) FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @RequestDate", connection);
        command.Parameters.AddWithValue("@IdProduct", productId);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@RequestDate", requestDate);
        return (int)command.ExecuteScalar() > 0;
    }
    
    public int GetOrderId(int productId, int amount, DateTime requestDate)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        using var command = new SqlCommand("SELECT IdOrder FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @RequestDate", connection);
        command.Parameters.AddWithValue("@IdProduct", productId);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@RequestDate", requestDate);
        return (int)command.ExecuteScalar();
    }
    
    public void UpdateOrderFullfilled(int orderId)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        using var command = new SqlCommand("UPDATE [Order] SET Fullfilled = GETDATE() WHERE IdOrder = @IdOrder", connection);
        command.Parameters.AddWithValue("@IdOrder", orderId);
        command.ExecuteNonQuery();
    }
    
}