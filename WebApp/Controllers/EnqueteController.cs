using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.App_Code;

namespace WebApp.Controllers
{
    public class EnqueteController : ApiController
    {
        // GET api/<controller>
        public IList<Models.Poll> Get()
        {
            Arquivo arquivo = new Arquivo();
            //var retornoX = arquivo.GetLastPollId();

            var retorno = new List<Models.Poll>();

            return retorno;
        }

        // GET api/<controller>/5
        public Models.Poll Get(int id)
        {
            var retorno = (new Arquivo()).GetPoll(id);

            if (retorno == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            

            return retorno;
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage PostPoll([FromBody]Models.Poll poll)
        {
            Arquivo arquivo = new Arquivo();
            var retorno = arquivo.AddPoll(poll);

            object obj = new
            {
                poll_id = retorno
            };

            return new HttpResponseMessage() { Content = new StringContent(JsonConvert.SerializeObject(obj)) };
        }
    }
}