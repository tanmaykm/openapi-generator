/* 
 * OpenAPI Petstore
 *
 * This spec is mainly for testing Petstore server and contains fake endpoints, models. Please do not use this for any other purpose. Special characters: \" \\
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Org.OpenAPITools.Test.Api
{
	/// <summary>
	/// Class for testing PetApi
	/// </summary>
	/// <remarks>
	/// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
	/// Please update the test case below to test the API endpoint.
	/// </remarks>
	public class PetApiTestsV2 : IDisposable
	{
		// CONFIGURE TESTING PARAMETERS HERE
		// see the Integration Test Wiki for details: https://github.com/OpenAPITools/openapi-generator/wiki/Integration-Tests
		private const string BasePath = "http://petstore.swagger.io/v2";
		private const long PetId = 100000;
	    private const long NotExistentId = 100001;

	    private readonly HttpClient _httpClient = new HttpClient();
        private readonly PetApi _petApi;

        public PetApiTestsV2()
        {
			// prepare the client
	        _petApi = new PetApi(_httpClient, new Configuration
	        {
		        BasePath = BasePath,
		        Timeout = TimeSpan.FromSeconds(10),
		        UserAgent = "TEST_USER_AGENT"
	        });

            // add a sample pet for the expected PetId
            _petApi.AddPet(BuildSamplePet());

			// ensure there is not a pet for that ID
			try
			{
				_petApi.DeletePet(NotExistentId);
			}
			catch (ApiException ex) when (ex.ErrorCode == 404) { }
        }

		#region Get

		/// <summary>
		/// Test GetPetById with an existent Id
		/// </summary>
		[Fact]
        public void GetPetById_GivenExistentId_ReturnsPet()
        {
	        Pet expected = BuildSamplePet();

			Pet response = _petApi.GetPetById(PetId);

			Assert.IsType<Pet>(response);
			Assert.Equal(expected.Name, response.Name);
			Assert.Equal(expected.Status, response.Status);
			Assert.IsType<List<Tag>>(response.Tags);
			Assert.Equal(expected.Tags[0].Id, response.Tags[0].Id);
			Assert.Equal(expected.Tags[0].Name, response.Tags[0].Name);
			Assert.IsType<List<string>>(response.PhotoUrls);
			Assert.Equal(expected.PhotoUrls[0], response.PhotoUrls[0]);
			Assert.IsType<Category>(response.Category);
			Assert.Equal(expected.Category.Id, response.Category.Id);
			Assert.Equal(expected.Category.Name, response.Category.Name);
		}

		/// <summary>
		/// Test GetPetById with a not existent Id
		/// </summary>
		[Fact]
		public void GetPetById_GivenNotExistentId_ThrowsApiException()
		{
			var exception = Assert.Throws<ApiException>(() =>
			{
				_petApi.GetPetById(NotExistentId);
			});

			Assert.IsType<ApiException>(exception);
			Assert.Equal(404, exception.ErrorCode);
			Assert.Equal("{\"code\":1,\"type\":\"error\",\"message\":\"Pet not found\"}", exception.ErrorContent);
			Assert.Equal("Error calling GetPetById: {\"code\":1,\"type\":\"error\",\"message\":\"Pet not found\"}", exception.Message);
		}

		/// <summary>
		/// Test GetPetByIdWithHttpInfo with an existent Id
		/// </summary>
		[Fact]
		public void GetPetByIdWithHttpInfo_GivenExistentId_Returns200Response()
		{
			Pet expected = BuildSamplePet();

			ApiResponse<Pet> response = _petApi.GetPetByIdWithHttpInfo(PetId);
			Pet result = response.Data;

			Assert.IsType<ApiResponse<Pet>>(response);
			Assert.Equal(200, (int)response.StatusCode);
		    Assert.True(response.Headers.ContainsKey("Content-Type"));
		    Assert.Equal("application/json", response.Headers["Content-Type"][0]);

			Assert.Equal(expected.Name, result.Name);
			Assert.Equal(expected.Status, result.Status);
			Assert.IsType<List<Tag>>(result.Tags);
			Assert.Equal(expected.Tags[0].Id, result.Tags[0].Id);
			Assert.Equal(expected.Tags[0].Name, result.Tags[0].Name);
			Assert.IsType<List<string>>(result.PhotoUrls);
			Assert.Equal(expected.PhotoUrls[0], result.PhotoUrls[0]);
			Assert.IsType<Category>(result.Category);
			Assert.Equal(expected.Category.Id, result.Category.Id);
			Assert.Equal(expected.Category.Name, result.Category.Name);
		}

		/// <summary>
		/// Test GetPetByIdWithHttpInfo with a not existent Id and the ExceptionFactory disabled
		/// </summary>
		[Fact]
		public void GetPetByIdWithHttpInfo_GivenNotExistentId_ThrowsApiException()
		{
			var exception = Assert.Throws<ApiException>(() =>
			{
				_petApi.GetPetByIdWithHttpInfo(NotExistentId);
			});

			Assert.IsType<ApiException>(exception);
			Assert.Equal(404, exception.ErrorCode);
			Assert.Equal("{\"code\":1,\"type\":\"error\",\"message\":\"Pet not found\"}", exception.ErrorContent);
			Assert.Equal("Error calling GetPetById: {\"code\":1,\"type\":\"error\",\"message\":\"Pet not found\"}", exception.Message);
		}

		/// <summary>
		/// Test GetPetByIdWithHttpInfo with a not existent Id and the ExceptionFactory disabled
		/// </summary>
		[Fact]
		public void GetPetByIdWithHttpInfo_WithoutExceptionFactory_GivenNotExistentId_Returns404Response()
		{
			_petApi.ExceptionFactory = null;
			ApiResponse<Pet> response = _petApi.GetPetByIdWithHttpInfo(NotExistentId);
			Pet result = response.Data;

			Assert.IsType<ApiResponse<Pet>>(response);
			Assert.Equal(404, (int)response.StatusCode);
			Assert.True(response.Headers.ContainsKey("Content-Type"));
			Assert.Equal("application/json", response.Headers["Content-Type"][0]);

			Assert.Null(result);
			Assert.Equal("{\"code\":1,\"type\":\"error\",\"message\":\"Pet not found\"}", response.RawContent);
			Assert.Equal("Not Found", response.ErrorText);
		}

		#endregion

		#region Get Async

		/// <summary>
		/// Test GetPetByIdAsync with an existent Id.
		/// </summary>
		[Fact]
		public async Task GetPetByIdAsync_GivenExistentId_ReturnsPet()
		{
			Pet expected = BuildSamplePet();

			Pet response = await _petApi.GetPetByIdAsync(PetId);

			Assert.IsType<Pet>(response);
			Assert.Equal(expected.Name, response.Name);
			Assert.Equal(expected.Status, response.Status);
			Assert.IsType<List<Tag>>(response.Tags);
			Assert.Equal(expected.Tags[0].Id, response.Tags[0].Id);
			Assert.Equal(expected.Tags[0].Name, response.Tags[0].Name);
			Assert.IsType<List<string>>(response.PhotoUrls);
			Assert.Equal(expected.PhotoUrls[0], response.PhotoUrls[0]);
			Assert.IsType<Category>(response.Category);
			Assert.Equal(expected.Category.Id, response.Category.Id);
			Assert.Equal(expected.Category.Name, response.Category.Name);
		}

		/// <summary>
		/// Test GetPetByIdAsync with a not existent Id.
		/// </summary>
		[Fact]
		public async Task GetPetByIdAsync_GivenNotExistentId_ThrowsApiException()
		{
			var exception = await Assert.ThrowsAsync<ApiException>(() => _petApi.GetPetByIdAsync(NotExistentId));

			Assert.IsType<ApiException>(exception);
			Assert.Equal(404, exception.ErrorCode);
			Assert.Equal("{\"code\":1,\"type\":\"error\",\"message\":\"Pet not found\"}", exception.ErrorContent);
			Assert.Equal("Error calling GetPetById: {\"code\":1,\"type\":\"error\",\"message\":\"Pet not found\"}", exception.Message);
		}

		/// <summary>
		/// Test GetPetByIdWithHttpInfoAsync with an existent Id.
		/// </summary>
		[Fact]
		public async Task GetPetByIdWithHttpInfoAsync_GivenExistentId_Returns200Response()
		{
			Pet expected = BuildSamplePet();

			ApiResponse<Pet> response = await _petApi.GetPetByIdWithHttpInfoAsync(PetId);
			Pet result = response.Data;

			Assert.IsType<ApiResponse<Pet>>(response);
			Assert.Equal(200, (int)response.StatusCode);
			Assert.True(response.Headers.ContainsKey("Content-Type"));
			Assert.Equal("application/json", response.Headers["Content-Type"][0]);

			Assert.Equal(expected.Name, result.Name);
			Assert.Equal(expected.Status, result.Status);
			Assert.IsType<List<Tag>>(result.Tags);
			Assert.Equal(expected.Tags[0].Id, result.Tags[0].Id);
			Assert.Equal(expected.Tags[0].Name, result.Tags[0].Name);
			Assert.IsType<List<string>>(result.PhotoUrls);
			Assert.Equal(expected.PhotoUrls[0], result.PhotoUrls[0]);
			Assert.IsType<Category>(result.Category);
			Assert.Equal(expected.Category.Id, result.Category.Id);
			Assert.Equal(expected.Category.Name, result.Category.Name);
		}

		/// <summary>
		/// Test GetPetByIdWithHttpInfoAsync with a not existent Id and the ExceptionFactory disabled.
		/// </summary>
		[Fact]
		public async Task GetPetByIdWithHttpInfoAsync_GivenNotExistentId_ThrowsApiException()
		{
			var exception = await Assert.ThrowsAsync<ApiException>(() => _petApi.GetPetByIdWithHttpInfoAsync(NotExistentId));

			Assert.IsType<ApiException>(exception);
			Assert.Equal(404, exception.ErrorCode);
			Assert.Equal("{\"code\":1,\"type\":\"error\",\"message\":\"Pet not found\"}", exception.ErrorContent);
			Assert.Equal("Error calling GetPetById: {\"code\":1,\"type\":\"error\",\"message\":\"Pet not found\"}", exception.Message);
		}

		/// <summary>
		/// Test GetPetByIdWithHttpInfoAsync with a not existent Id and the ExceptionFactory disabled.
		/// </summary>
		[Fact]
		public async Task GetPetByIdWithHttpInfoAsync_WithoutExceptionFactory_GivenNotExistentId_Returns404Response()
		{
			_petApi.ExceptionFactory = null;
			ApiResponse<Pet> response = await _petApi.GetPetByIdWithHttpInfoAsync(NotExistentId);
			Pet result = response.Data;

			Assert.IsType<ApiResponse<Pet>>(response);
			Assert.Equal(404, (int)response.StatusCode);
			Assert.True(response.Headers.ContainsKey("Content-Type"));
			Assert.Equal("application/json", response.Headers["Content-Type"][0]);

			Assert.Null(result);
			Assert.Equal("{\"code\":1,\"type\":\"error\",\"message\":\"Pet not found\"}", response.RawContent);
			Assert.Equal("Not Found", response.ErrorText);
		}

		#endregion

		#region Find

		/// <summary>
		/// Test FindPetsByStatus filtering available pets.
		/// </summary>
		[Fact(Skip = "too much elements to fetch, the server cut the json content")]
		public void FindPetsByStatus_ReturnsListOfPetsFiltered()
		{
			List<Pet> pets = _petApi.FindPetsByStatus(new List<string>(new[] { "available" }));

			foreach (Pet pet in pets)
			{
				Assert.IsType<Pet>(pet);
				Assert.Equal(Pet.StatusEnum.Available, pet.Status);
			}
		}

		#endregion

		#region Add

		/// <summary>
		/// Test AddPet with an existent Id. The current server beavior is to update the Pet.
		/// </summary>
		[Fact]
		public void AddPet_GivenExistentId_UpdateThePet()
		{
			Pet pet = BuildSamplePet();

			_petApi.AddPet(pet);
		}

		#endregion

		#region AddAsync

		/// <summary>
		/// Test AddPetAsync with an existent Id. The current server beavior is to update the Pet.
		/// </summary>
		[Fact]
		public async Task AddPetAsync_GivenExistentId_UpdateThePet()
		{
			Pet pet = BuildSamplePet();

			await _petApi.AddPetAsync(pet);
		}

		#endregion

		#region Update

		/// <summary>
		/// Test UpdatePet with an existent Id.
		/// </summary>
		[Fact]
		public void UpdatePet_GivenExistentId_UpdateThePet()
		{
			Pet pet = BuildSamplePet();

			_petApi.UpdatePet(pet);
		}

		/// <summary>
		/// Test UpdatePet with a not existent Id. The current server beavior is to create the Pet.
		/// </summary>
		[Fact]
		public void UpdatePet_GivenNotExistentId_UpdateThePet()
		{
			Pet pet = BuildSamplePet(i => i.Id = NotExistentId);

			_petApi.UpdatePet(pet);
		}

		/// <summary>
		/// Test UpdatePetWithForm with an existent Id.
		/// </summary>
		[Fact]
		public void UpdatePetWithForm_GivenExistentId_UpdatesTheFields()
		{
			Pet expected = BuildSamplePet(pet =>
			{
				pet.Name = "name updated";
				pet.Status = Pet.StatusEnum.Pending;
			});

			_petApi.UpdatePetWithForm(PetId, "name updated", "pending");

			Pet response = _petApi.GetPetById(PetId);

			Assert.IsType<Pet>(response);
			Assert.Equal(expected.Name, response.Name);
			Assert.Equal(expected.Status, response.Status);
			Assert.IsType<List<Tag>>(response.Tags);
			Assert.Equal(expected.Tags[0].Id, response.Tags[0].Id);
			Assert.Equal(expected.Tags[0].Name, response.Tags[0].Name);
			Assert.IsType<List<string>>(response.PhotoUrls);
			Assert.Equal(expected.PhotoUrls[0], response.PhotoUrls[0]);
			Assert.IsType<Category>(response.Category);
			Assert.Equal(expected.Category.Id, response.Category.Id);
			Assert.Equal(expected.Category.Name, response.Category.Name);

			_petApi.UpdatePetWithForm(PetId, "name updated twice");

			response = _petApi.GetPetById(PetId);

			Assert.Equal("name updated twice", response.Name);
		}

		/// <summary>
		/// Test UploadFile with an existent Id.
		/// </summary>
		[Fact(Skip = "generates 500 code at the time of test")]
		public void UploadFile_UploadFileUsingFormParameters_UpdatesTheFields()
		{
			var assembly = Assembly.GetExecutingAssembly();
			using Stream imageStream = assembly.GetManifestResourceStream("Org.OpenAPITools.Test.linux-logo.png");
			_petApi.UploadFile(PetId, "metadata sample", imageStream);
		}

		/// <summary>
		/// Test UploadFile with an existent Id.
		/// </summary>
		[Fact(Skip = "generates 500 code at the time of test")]
		public void UploadFile_UploadFileAlone_UpdatesTheField()
		{
			var assembly = Assembly.GetExecutingAssembly();
			using Stream imageStream = assembly.GetManifestResourceStream("Org.OpenAPITools.Test.linux-logo.png");
			_petApi.UploadFile(petId: PetId, file: imageStream);
		}

		#endregion

		#region UpdateAsync

		/// <summary>
		/// Test UpdatePetAsync with an existent Id.
		/// </summary>
		[Fact]
		public async Task UpdatePetAsync_GivenExistentId_UpdateThePet()
		{
			Pet pet = BuildSamplePet();

			await _petApi.UpdatePetAsync(pet);
		}

		/// <summary>
		/// Test UpdatePetAsync with a not existent Id. The current server beavior is to create the Pet.
		/// </summary>
		[Fact]
		public async Task UpdatePetAsync_GivenNotExistentId_UpdateThePet()
		{
			Pet pet = BuildSamplePet(i => i.Id = NotExistentId);

			await _petApi.UpdatePetAsync(pet);
		}

		/// <summary>
		/// Test UpdatePetWithFormAsync with an existent Id.
		/// </summary>
		[Fact]
		public async Task UpdatePetWithFormAsync_GivenExistentId_UpdatesTheFields()
		{
			Pet expected = BuildSamplePet(pet =>
			{
				pet.Name = "name updated";
				pet.Status = Pet.StatusEnum.Pending;
			});

			await _petApi.UpdatePetWithFormAsync(PetId, "name updated", "pending");

			Pet response = await _petApi.GetPetByIdAsync(PetId);

			Assert.IsType<Pet>(response);
			Assert.Equal(expected.Name, response.Name);
			Assert.Equal(expected.Status, response.Status);
			Assert.IsType<List<Tag>>(response.Tags);
			Assert.Equal(expected.Tags[0].Id, response.Tags[0].Id);
			Assert.Equal(expected.Tags[0].Name, response.Tags[0].Name);
			Assert.IsType<List<string>>(response.PhotoUrls);
			Assert.Equal(expected.PhotoUrls[0], response.PhotoUrls[0]);
			Assert.IsType<Category>(response.Category);
			Assert.Equal(expected.Category.Id, response.Category.Id);
			Assert.Equal(expected.Category.Name, response.Category.Name);

			await _petApi.UpdatePetWithFormAsync(PetId, "name updated twice");

			response = await _petApi.GetPetByIdAsync(PetId);

			Assert.Equal("name updated twice", response.Name);
		}

		/// <summary>
		/// Test UploadFileAsync with an existent Id.
		/// </summary>
		[Fact(Skip = "generates 500 code at the time of test")]
		public async Task UploadFileAsync_UploadFileUsingFormParameters_UpdatesTheFields()
		{
			var assembly = Assembly.GetExecutingAssembly();
			await using Stream imageStream = assembly.GetManifestResourceStream("Org.OpenAPITools.Test.linux-logo.png");
			await _petApi.UploadFileAsync(PetId, "metadata sample", imageStream);
		}

		/// <summary>
		/// Test UploadFileAsync with an existent Id.
		/// </summary>
		[Fact(Skip = "generates 500 code at the time of test")]
		public async Task UploadFileAsync_UploadFileAlone_UpdatesTheField()
		{
			var assembly = Assembly.GetExecutingAssembly();
			await using Stream imageStream = assembly.GetManifestResourceStream("Org.OpenAPITools.Test.linux-logo.png");
			await _petApi.UploadFileAsync(petId: PetId, file: imageStream);
		}

		#endregion

		#region Delete

		/// <summary>
		/// Test DeletePet with an existent Id.
		/// </summary>
		[Fact]
		public void DeletePet_GivenExistentId_DeleteThePet()
		{
			_petApi.DeletePet(PetId);

			var exception = Assert.Throws<ApiException>(() => _petApi.GetPetById(PetId));

			Assert.IsType<ApiException>(exception);
			Assert.Equal(404, exception.ErrorCode);
		}

		/// <summary>
		/// Test DeletePet with a not existent Id. The current server beavior is to return 404.
		/// </summary>
		[Fact]
		public void DeletePet_GivenNotExistentId_ThrowsApiException()
		{
			var exception = Assert.Throws<ApiException>(() => _petApi.DeletePet(NotExistentId));

			Assert.IsType<ApiException>(exception);
			Assert.Equal(404, exception.ErrorCode);
		}

		#endregion

		#region DeleteAsync

		/// <summary>
		/// Test DeletePet with an existent Id.
		/// </summary>
		[Fact]
		public async Task DeletePetAsync_GivenExistentId_DeleteThePet()
		{
			await _petApi.DeletePetAsync(PetId);

			var exception = Assert.Throws<ApiException>(() => _petApi.GetPetById(PetId));

			Assert.IsType<ApiException>(exception);
			Assert.Equal(404, exception.ErrorCode);
		}

		/// <summary>
		/// Test DeletePet with a not existent Id. The current server beavior is to return 404.
		/// </summary>
		[Fact]
		public async Task DeletePetAsync_GivenNotExistentId_ThrowsApiException()
		{
			var exception = await Assert.ThrowsAsync<ApiException>(() => _petApi.DeletePetAsync(NotExistentId));

			Assert.IsType<ApiException>(exception);
			Assert.Equal(404, exception.ErrorCode);
		}

		#endregion

		private static Pet BuildSamplePet(Action<Pet> callback = null)
		{
			var pet = new Pet(
				name: "csharp test",
				photoUrls: new List<string> { "http://petstore.com/csharp_test" })
			{
				Id = PetId,
				Status = Pet.StatusEnum.Available,
				Category = new Category { Id = 10, Name = "sample category" },
				Tags = new List<Tag> { new Tag { Id = 100, Name = "sample tag" } }
			};

			callback?.Invoke(pet);

			return pet;
		}

		public void Dispose()
        {
	        // remove the pet after testing
	        try
            {
	            _petApi.DeletePet(PetId);
            }
            catch (ApiException ex) when (ex.ErrorCode == 404) { }

			_petApi.Dispose();
            _httpClient.Dispose();
        }
    }
}
