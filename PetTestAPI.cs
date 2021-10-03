using Newtonsoft.Json;
using NUnit.Framework;
using RestAPIAutomation.DTOs;
using RestSharp;

namespace RestAPIAutomation
{
    public class PetTestAPI
    {
        private RestClient client;

        public PetTestAPI(string url) {
            this.client = new RestClient(url);
        }

        public IRestResponse GetPet(double id) {
            TestContext.Out.WriteLine("GET call for pet with ID: '{0}' sent", id);

            RestRequest request = new RestRequest($"pet/{id}", Method.GET);
            return client.Execute(request);
        }

        public IRestResponse GetPetsByState(string state) {
            TestContext.Out.WriteLine("GET call for all pets with state '{0}' sent", state);

            RestRequest request = new RestRequest($"pet/findByStatus/?status={state}", Method.GET);
            return client.Execute(request);
        }

        public IRestResponse PostNewPet(Pet newpet) {
            string json = JsonConvert.SerializeObject(newpet);
            TestContext.Out.WriteLine("POST call to create this pet:\n{0}", json);

            RestRequest request = new RestRequest("pet/", Method.POST);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            return client.Execute(request);
        }

        public IRestResponse UpdatePet(Pet pet) {
            TestContext.Out.WriteLine("POST call to update pet" +
                " with id: '{0}' to name '{1}' and status '{2}'", pet.id, pet.name, pet.status);

            RestRequest request = new RestRequest($"pet/{pet.id}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("name", pet.name);
            request.AddParameter("status", pet.status);

            return client.Execute(request);
        }

        public IRestResponse DeletePet(double id) {
            TestContext.Out.WriteLine("DELETE call for pet with id: '{0}'", id);

            RestRequest request = new RestRequest($"pet/{id}", Method.DELETE);
            return client.Execute(request);
        }
    }
}
