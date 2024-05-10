using System.Data.SqlClient;

namespace PJATK_APBD_E4.Product;

public class ProductRepository : IProductRepository
{
    private IConfiguration _configuration;

    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool ProductExists(int id)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        using var command = new SqlCommand("SELECT COUNT(*) FROM Product WHERE IdProduct = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        return (int)command.ExecuteScalar() > 0;
    }
    
    public double GetProductPrice(int id)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        connection.Open();
        using var command = new SqlCommand("SELECT Price FROM Product WHERE IdProduct = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        return (double)command.ExecuteScalar();
    }
} 