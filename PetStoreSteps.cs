using System.Collections.Generic;
using TechTalk.SpecFlow;
using NUnit.Framework;
using RestAPIAutomation.DTOs;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace RestAPIAutomation
{
    [Binding]
    public class PetStoreSteps
    {
        private PetTestAPI petTestAPI;
        private Pet demoPet;

        private Pet petResult;
        private List<Pet> petResults;
        private IRestResponse response;

        [Given(@"I am using the same Pet instance for testing")]
        public void GivenIAmUsingADemoPetForTesting() {
            demoPet = new Pet() {
                id = 55756,
                name = "RUFUS",
                status = "pending",
                category = new PetCategory() { id = 2, name = "cat" },
                tags = new List<PetTag>() {
                    new PetTag() { id = 1, name ="good" },
                    new PetTag() { id = 2, name = "fluffy"}
                },
                photoUrls = new List<string>() { "test.com/img/test.jpg" }
            };
        }

        [Given(@"I am using the petstore API")]
        public void GivenIAmUsingThePetstoreAPI()
        {
            petTestAPI = new PetTestAPI("https://petstore.swagger.io/v2");
        }
        
        [When(@"I call for all the available pets")]
        public void WhenICallForAllTheAvailablePets()
        {
            response = petTestAPI.GetPetsByState(
                Constants.PetStates.available.ToString());
        }
        
        [When(@"I post a new pet")]
        public void WhenIPostANewPet()
        {
            response = petTestAPI.PostNewPet(demoPet);
        }
        
        [When(@"I update a pet to have ""(.*)"" as its status")]
        public void WhenIUpdateAPetToHaveAsItsStatus(string newStatus)
        {
            demoPet.status = newStatus;
            response = petTestAPI.UpdatePet(demoPet);
        }
        
        [When(@"I delete a pet")]
        public void WhenIDeleteAPet()
        {
            response = petTestAPI.DeletePet(demoPet.id);
        }
        
        [Then(@"the API should return an OK status code")]
        public void ThenTheAPIShouldReturnAnOKStatusCode()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Then(@"the API should return a list of pets")]
        public void ThenTheAPIShouldReturnAListOfPets()
        {
            petResults = GetPetsFromResponse();
        }
        
        [Then(@"all of them should have its status as available")]
        public void ThenAllOfThemShouldHaveItsStatusAsAvailable()
        {
            int petsAvailable = petResults.FindAll(
                p => p.status == Constants.PetStates.available.ToString()
                ).Count;
            Assert.AreEqual(petsAvailable, petResults.Count);
        }
        
        [Then(@"the new pet should be accesible")]
        public void ThenTheNewPetShouldBeAccesible()
        {
            response = petTestAPI.GetPet(demoPet.id);
        }
        
        [Then(@"the new pet should have the same properties we requested")]
        public void ThenTheNewPetShouldHaveTheSamePropertiesWeRequested()
        {
            petResult = GetPetFromResponse();
            Assert.AreEqual(demoPet.status, petResult.status);
            Assert.AreEqual(demoPet.name, petResult.name);
            Assert.AreEqual(demoPet.id, petResult.id);
            Assert.AreEqual(demoPet.category.id, petResult.category.id);
            Assert.AreEqual(demoPet.category.name, petResult.category.name);
            Assert.AreEqual(demoPet.tags[0].id, petResult.tags[0].id);
            Assert.AreEqual(demoPet.tags[0].name, petResult.tags[0].name);
            CollectionAssert.AreEqual(demoPet.photoUrls, petResult.photoUrls);
        }
        
        [Then(@"the pet should now have status set to ""(.*)""")]
        public void ThenTheNewRequestedPetPropertyShouldNowBeAccesible(string newStatus)
        {
            response = petTestAPI.GetPet(demoPet.id);
            petResult = GetPetFromResponse();
            Assert.AreEqual(newStatus, petResult.status);
        }
        
        [Then(@"the pet should no longer be accesible")]
        public void ThenThePetShouldNoLongerBeAccesible()
        {
            response = petTestAPI.DeletePet(demoPet.id);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        public Pet GetPetFromResponse() {
            return JsonConvert.DeserializeObject<Pet>(response.Content);
        }

        public List<Pet> GetPetsFromResponse() {
            return JsonConvert.DeserializeObject<List<Pet>>(response.Content);
        }
    }
}
