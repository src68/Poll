using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.App_Code;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EnqueteVotacaoController : ApiController
    {
        public ModelView Get(int id)
        {
            Arquivo arquivo = new Arquivo();
            var retorno = arquivo.GetView(id);

            if (retorno != null)
                return retorno; 
            else
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        [HttpPost]
        public HttpResponseMessage PostVote([FromBody]ModelVotacao votacao)
        {
            Arquivo arquivo = new Arquivo();
            var retorno = arquivo.Vote(votacao.idPoll, votacao.idPollItem);

            if (retorno)
            {
                object obj = new
                {
                    option_id = votacao.idPollItem
                };

                return new HttpResponseMessage() { Content = new StringContent(JsonConvert.SerializeObject(obj)) };
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
        }
    }
}
