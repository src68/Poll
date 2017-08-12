using ConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static string uriApi = "http://localhost:8080/";
        //static string uriApi = "http://localhost:3166/";

        static void Main(string[] args)
        {
            var result = "";

            while (result != "5")
            {
                CriarMenuPrincipal();

                result = Console.ReadLine();
                string retorno = "";
                string idPoll = "";
                string idPollItem = "";

                switch (result)
                {
                    case "1":
                        retorno = AddPoll().Result;
                        break;
                    case "2":

                        Console.Write("Informe o id da enquete: ");
                        idPoll = Console.ReadLine();

                        retorno = GetPoll(Convert.ToInt32(idPoll)).Result;
                        break;
                    case "3":
                        Console.Write("Informe o id da enquete: ");
                        idPoll = Console.ReadLine();
                        Console.Write("Informe o id da opção: ");
                        idPollItem = Console.ReadLine();

                        retorno = VotePollItem(Convert.ToInt32(idPoll), Convert.ToInt32(idPollItem)).Result;
                        break;
                    case "4":
                        Console.Write("Informe o id da enquete: ");
                        idPoll = Console.ReadLine();

                        retorno = GetView(Convert.ToInt32(idPoll)).Result;
                        break;
                }

                Console.Clear();
                Console.WriteLine(retorno);


                if (result != "5")
                {
                    Console.ReadLine();
                    Console.Clear();
                }
            }

        }

        static void CriarMenuPrincipal()
        {
            Console.WriteLine("1. Adicionar enquete");
            Console.WriteLine("2. Consultar enquete");
            Console.WriteLine("3. Registrar voto");
            Console.WriteLine("4. Consultar estatística");
            Console.WriteLine("5. FECHAR");
        }

        static async Task<String> GetPoll(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uriApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string response = await client.GetStringAsync(String.Format("api/enquete/{0}", id));

                //Poll poll = JsonConvert.DeserializeObject<Poll>(response);

                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        static async Task<String> AddPoll()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uriApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                Poll poll = new Poll();
                poll.poll_description = "new poll - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                poll.options.Add(new PollOption() { option_description = "new poll option 1" });
                poll.options.Add(new PollOption() { option_description = "new poll option 2" });
                poll.options.Add(new PollOption() { option_description = "new poll option 3" });

                StringContent content = new StringContent(JsonConvert.SerializeObject(poll), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/enquete", content);//.Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return response.Content.ReadAsStringAsync().Result;
                else
                    throw new Exception(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        static async Task<String> VotePollItem(int idPoll, int idPollItem)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uriApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var param = new ModelVotacao()
                {
                    idPoll = idPoll,
                    idPollItem = idPollItem
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/enquetevotacao", content);//.Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return response.Content.ReadAsStringAsync().Result;
                else
                    throw new Exception(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        static async Task<String> GetView(int idPoll)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uriApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string response = await client.GetStringAsync(String.Format("api/enquetevotacao/{0}", idPoll));

                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
