Feature: PetStore
	API for dealing with pets

	Background: 
		Given I am using the same Pet instance for testing

	@smoke
	Scenario: Be able to get all available pets
		Given I am using the petstore API
		When I call for all the available pets
		Then the API should return an OK status code
		And the API should return a list of pets
		And all of them should have its status as available

	Scenario: Be able to post a new pet
		Given I am using the petstore API
		When I post a new pet
		Then the API should return an OK status code
		And the new pet should be accesible 
		And the new pet should have the same properties we requested

	Scenario: Be able to update a pet to have a "sold" status
		Given I am using the petstore API
		When I update a pet to have "sold" as its status
		Then the API should return an OK status code
		And the pet should now have status set to "sold"

	Scenario: Be able to delete a pet
		Given I am using the petstore API
		When I delete a pet
		Then the API should return an OK status code
		And the pet should no longer be accesible