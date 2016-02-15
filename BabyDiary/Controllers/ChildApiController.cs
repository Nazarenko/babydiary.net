using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BabyDiary.Business.Interfaces;
using BabyDiary.Helpers;
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
            return await _childProvider.GetChildsAsync();
        }

        // POST: child
        [HttpPost]
        [Route("child")]
        [ValidateModel]
        public async Task<HttpResponseMessage> SaveChild(ChildDto childDto)
        {
            await _childProvider.SaveChildAsync(childDto);
            return Request.CreateResponse(HttpStatusCode.OK, childDto);
        }

        // DELETE: child
        [HttpDelete]
        [Route("child/{childId:long}")]
        public async Task<HttpResponseMessage> DeleteChild(long childId)
        {
            await _childProvider.DeleteChildAsync(childId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}