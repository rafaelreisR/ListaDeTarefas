using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoMvc.Models;

namespace TodoMvc.Infrastructure
{
    public class ToDoRestClient
    {
        private string URL_PET_REST = "https://functionazureamigo.azurewebsites.net/api";

        public IList<ToDoItemModel> GetAll()
        {
            var client = new RestClient(URL_PET_REST);

            var request = new RestRequest("GetTodos", DataFormat.Json);

            var response = client.Get<IList<ToDoItemModel>>(request);

            return response.Data;

        }

        public ToDoItemModel GetById(Guid id)
        {
            var client = new RestClient(URL_PET_REST);

            var request = new RestRequest($"GetByIdFunction?id={id}", DataFormat.Json);

            //Outra forma de passar o parametro id
            //request.AddQueryParameter("id", id.ToString());            

            var response = client.Get<ToDoItemModel>(request);

            return response.Data;

        }

        public void Save(ToDoItemModel model)
        {
            var client = new RestClient(URL_PET_REST);
            var request = new RestRequest($"SaveTodos", DataFormat.Json);
            request.AddJsonBody(model);

            var response = client.Post<ToDoItemModel>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
                throw new Exception("Não consegui listar a tarefa");


        }

        public void Delete(Guid id)
        {
            var client = new RestClient(URL_PET_REST);

            var request = new RestRequest($"DeleteFunction?id={id}", DataFormat.Json);

            //Outra forma de passar o parametro id
            //request.AddQueryParameter("id", id.ToString());            

            var response = client.Delete(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Não consegui deletar a tarefa");

        }

        public void Update(ToDoItemModel model)
        {
            var client = new RestClient(URL_PET_REST);

            var request = new RestRequest($"PutFunction", DataFormat.Json);
            request.AddJsonBody(model);

            var response = client.Put<ToDoItemModel>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Não consegui atualizar a tarefa");

        }
    }
}