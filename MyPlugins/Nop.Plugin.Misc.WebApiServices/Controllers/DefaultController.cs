using Nop.Core.Domain.Common;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nop.Plugin.Misc.WebApiServices.Controllers
{
    public class DefaultController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly AdminAreaSettings _adminAreaSettings;
        public DefaultController(IOrderService orderService, AdminAreaSettings adminAreaSettings)
        {
            _orderService = orderService;
            _adminAreaSettings = adminAreaSettings;

        }
        // GET: api/Default
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", _adminAreaSettings.GridPageSizes };
        }

        // GET: api/Default/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
