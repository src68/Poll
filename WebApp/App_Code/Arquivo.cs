using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.App_Code
{
    public class Arquivo
    {
        string filePath = HttpContext.Current.Server.MapPath("~/App_Data");

        int GetLastPollId()
        {
            try
            {
                string path = filePath;

                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] files = di.GetFiles();

                int id = 0;

                foreach (var file in files)
                {
                    //pegar o id no nome do arquivo
                    if (file.Name.StartsWith("poll"))
                        id = Convert.ToInt32(file.Name.Split('-')[1].Split('.')[0]);
                }

                return id;
            }
            catch
            {
                throw;
            }
        }

        int GetVotes(int idPoll, int idOption)
        {
            try
            {
                string path = filePath;

                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] files = di.GetFiles();

                int qtde = 0;
                int idPollFile = 0;
                int idOptionFile = 0;
                string[] nomeSplit;

                foreach (var file in files)
                {
                    if (file.Name.StartsWith("vote"))
                    {
                        nomeSplit = file.Name.Split('.')[0].Split('-');

                        idPollFile = Convert.ToInt32(nomeSplit[1]);
                        idOptionFile = Convert.ToInt32(nomeSplit[2]);

                        if (idPollFile == idPoll && idOptionFile == idOption)
                        {
                            qtde++;
                        }
                    }
                }

                return qtde;
            }
            catch
            {
                throw;
            }
        }

        int GetViews(int idPoll)
        {
            try
            {
                string path = filePath;

                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] files = di.GetFiles();

                int qtde = 0;
                int idPollFile = 0;
                string[] nomeSplit;

                foreach (var file in files)
                {
                    if (file.Name.StartsWith("view"))
                    {
                        nomeSplit = file.Name.Split('.')[0].Split('-');

                        idPollFile = Convert.ToInt32(nomeSplit[1]);

                        if (idPollFile == idPoll)
                        {
                            qtde++;
                        }
                    }
                }

                return qtde;
            }
            catch
            {
                throw;
            }
        }

        public Models.Poll GetPoll(int id)
        {
            try
            {
                string path = filePath;

                FileInfo fi = new FileInfo(path);

                Models.Poll retorno = null;


                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] files = di.GetFiles();

                foreach (var file in files)
                {
                    //pegar o id no nome do arquivo
                    int idTemp = Convert.ToInt32(file.Name.Split('-')[1].Split('.')[0]);

                    if (idTemp == id)
                    {
                        StreamReader sr = new StreamReader(file.FullName);

                        retorno = new Models.Poll();

                        string linha = "";
                        int idLinha = 0;
                        string[] linhaSplit;
                        bool primeiraLinha = true;

                        while ((linha = sr.ReadLine()) != null)
                        {
                            if (primeiraLinha) //primeira linha do arquivo corresponde ao próprio poll
                            {
                                retorno.poll_id = id;
                                retorno.poll_description = linha.ToString();
                                primeiraLinha = false;
                            }
                            else //da segunda em diante são os itens do poll
                            {
                                linhaSplit = linha.Split(';');
                                idLinha = Convert.ToInt32(linhaSplit[0]);

                                retorno.options.Add(new Models.PollOption()
                                {
                                    option_id = Convert.ToInt32(linhaSplit[0].ToString()),
                                    poll_id = id,
                                    option_description = linhaSplit[1].ToString()
                                });
                            }
                        }

                        break;
                    }
                }
                
                return retorno;
            }
            catch
            {
                throw;
            }
        }

        public int AddPoll(Models.Poll poll)
        {
            try
            {
                int nextId = GetLastPollId() + 1;
                poll.poll_id = nextId;

                string path = filePath;

                string novoArquivo = path + @"\" + String.Format("poll-{0}.txt", poll.poll_id);


                StreamWriter sw = new StreamWriter(novoArquivo, false, System.Text.Encoding.Default);
                sw.WriteLine(poll.poll_description);

                int index = 1;

                foreach (var item in poll.options)
                {
                    item.option_id = index;

                    sw.WriteLine(String.Format("{0};{1}", item.option_id, item.option_description));

                    index++;
                }

                sw.Flush();
                sw.Close();

                return poll.poll_id;
            }
            catch
            {
                return 0;
            }
        }

        public bool Vote(int idPoll, int idOption)
        {
            try
            {
                Models.Poll poll = GetPoll(idPoll);

                if (poll == null)
                    throw new Exception();

                var item = poll.options.Where(w => w.option_id == idOption).FirstOrDefault();

                if (item == null)
                    throw new Exception();

                string path = filePath;

                string novoArquivo = path + @"\" + String.Format("vote-{0}-{1}-{2:ddMMyyyy HHmmss}.txt", idPoll, idOption, DateTime.Now);


                StreamWriter sw = new StreamWriter(novoArquivo, false, System.Text.Encoding.Default);
                sw.WriteLine(String.Format("{0};{1}", idPoll, idOption));
                sw.Flush();
                sw.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Models.ModelView GetView(int idPoll)
        {
            try
            {
                Models.Poll poll = GetPoll(idPoll);

                if (poll == null)
                    return null;

                string path = filePath;
                string novoArquivo = path + @"\" + String.Format("view-{0}-{1:ddMMyyyy HHmmss}.txt", idPoll, DateTime.Now);


                StreamWriter sw = new StreamWriter(novoArquivo, false, System.Text.Encoding.Default);
                sw.WriteLine(String.Format("{0}", idPoll));
                sw.Flush();
                sw.Close();


                Models.ModelView view = new Models.ModelView();

                foreach (var item in poll.options)
                {
                    view.views = GetViews(idPoll);
                    view.votes.Add(new Models.ModelViewItem()
                    {
                        option_id = item.option_id,
                        qty = GetVotes(idPoll, item.option_id)
                    });
                }


                return view;

            }
            catch
            {
                throw;
            }
        }
    }
}