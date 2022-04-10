using CommunicationApi.Adapters;
using CommunicationApi.Models.Dtos;
using CommunicationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationApi.Controllers
{    
    [ApiController]
    public class BundleController : Controller
    {
        private readonly IBundleFactoryService bundleFactoryService;

        public BundleController(IBundleFactoryService bundleFactoryService)
        {
            this.bundleFactoryService = bundleFactoryService;
        }

        [HttpGet("com/[controller]/[action]")]
        public ActionResult<IEnumerable<string>> GetAllInstances()
        {
            try
            {
                return Ok(bundleFactoryService.GetAllInstancedBundles().Select(x => x.ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("com/[controller]/[action]")]
        public ActionResult<Guid> InstanceBundle([FromBody] InstanceBundleDto dto)
        {
            try
            {
                switch (dto.BundleType)
                {
                    case TestPackages.Utils.Enums.BundleTypes.MetaTrader:
                        {
                            return Ok(bundleFactoryService.InstanceMetaTraderBundle(dto.HeadId, dto.InitialState));
                        }
                    default:
                        {
                            throw new NotImplementedException("Unknown bundle type!");
                        }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("com/[controller]/[action]")]
        public ActionResult ChangeBundleState([FromBody] ChangeBundleStateDto dto)
        {
            try
            {
                bundleFactoryService.ChangeBundleState(dto.BundleId, dto.NewState);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
