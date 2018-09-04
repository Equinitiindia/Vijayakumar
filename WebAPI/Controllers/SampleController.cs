using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
//using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    public class SampleController : ApiController
    {


        private ISampleRepository service;
        public SampleController(ISampleRepository service)
        {
            this.service = service;
        }
        // GET: api/Sample
        public IEnumerable<SampleData> Get()
        {
            return this.service.GetAll();
        }

        // GET: api/Sample/3f2b12b8-2a06-45b4-b057-45949279b4e5
        public SampleData Get(Guid id)
        {
            SampleData item = this.service.GetById(id);
            if (item == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No Sample with ID = {0}", id)),
                    ReasonPhrase = "Sample ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
            return item;

        }

        // POST: api/Sample
        public IHttpActionResult Post(SampleData sample)
        {
            if (!ModelState.IsValid)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Invalid  Sample"
                };
                throw new HttpResponseException(resp);
            }
            this.service.Add(sample);
            return Ok();

        }

        // PUT: api/Sample/5
        public IHttpActionResult Put(Guid id, SampleData sample)
        {
            if (!ModelState.IsValid)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Invalid Sample, Please updated values "
                };
                throw new HttpResponseException(resp);
            }
            this.service.Update(id,sample);
            return Ok();
        }

        // DELETE: api/Sample/5
        public IHttpActionResult Delete(Guid id)
        {
            SampleData item = this.service.GetById(id);
            if (item == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No Sample with ID = {0}", id)),
                    ReasonPhrase = "Sample ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
            this.service.Delete(id);

            return Ok();

        }
    }
}
