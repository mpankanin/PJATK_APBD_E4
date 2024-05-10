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
            var id = _warehouseService.AddProductWarehouse(productWarehouse);
            return StatusCode(StatusCodes.Status201Created, new {id = id});
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
    
    [HttpPost("sp")]
    public IActionResult AddWarehouseSP([FromBody] ProductWarehouse productWarehouse)
    {
        try
        {
            var id = _warehouseService.AddProductWarehouseSP(productWarehouse);
            return StatusCode(StatusCodes.Status201Created, new {id = id});
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
    
}