using CommunicationApi.Adapters;
using CommunicationApi.Models.Dtos;
using CommunicationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationApi.Controllers
{
    [Route("api/[controller]")]
    public class BundleController : Controller
    {
        private readonly IBundleFactoryService bundleFactoryService;

        public BundleController(IBundleFactoryService bundleFactoryService)
        {
            this.bundleFactoryService = bundleFactoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAllInstances()
        {
            return Ok(bundleFactoryService.GetAllRunningBundlesAsString());
        }

        [HttpPost]
        public ActionResult<Guid> InstanceBundle([FromBody] InstanceBundleDto dto)
        {
            try
            {
                switch (dto.BundleType)
                {
                    case TestPackages.Utils.Enums.BundleTypes.MetaTrader:
                        {
                            return Ok(bundleFactoryService.InstanceMetaTraderBundle(dto.HeadId));
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
    }
}
