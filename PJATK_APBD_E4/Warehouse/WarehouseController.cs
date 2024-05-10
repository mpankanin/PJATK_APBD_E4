using Microsoft.AspNetCore.Mvc;

namespace PJATK_APBD_E4.Warehouse;

[Route("api/warehouses")]
[ApiController]
public class WarehouseController : ControllerBase
{
    
    private IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    public IActionResult AddWarehouse([FromBody] ProductWarehouse productWarehouse)
    {
        try
        {
            _warehouseService.AddProductWarehouse(productWarehouse);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
    
}