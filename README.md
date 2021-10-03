# RestAPIAutomation
Tests for Exercise 1. Implemented with C# in .Net Core 3.1 using NUnit as a test framework, RestSharp for the API calls and SpecFlow for the gherkins logic.

## Execution instructions

Tests for the API are defined in the `PetStore.feature` gherkin file and implemented in `PetStoreSteps.cs` . Visual Studio's test explorer should detect it as the tests to be executed. NUnit's logs will register each step or issue found.

## Short description

Here we test 4 calls to the pet API. All API call logic is stored in a class separated from the test: `PetTestAPI.cs`. It leaves everything more tidy.

## Beware!

The API is rather unreliable. Wait some time after a test, have a coffee, then run the next test.
