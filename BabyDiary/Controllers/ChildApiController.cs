using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BabyDiary.Business.Interfaces;
using BabyDiary.Models.DTOs;

namespace BabyDiary.Controllers
{
    [Authorize]
    public class ChildApiController : ApiController
    {
        private readonly IChildProvider _childProvider;

        public ChildApiController(IChildProvider childProvider)
        {
            _childProvider = childProvider;
        }

        // GET: Childs
        [HttpGet]
        [Route("childs")]
        public async Task<List<ChildDto>> ChildsList()
        {
            return await _childProvider.GetChilds();
        }
    }
}