using BookStoreAPI.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace BookStoreAPI.Test
{
    [TestClass]
    public class ApiTests
    {
        private readonly WebApplicationFactory<Program> _webAppFactory;
        private readonly HttpClient _httpClient;
        private int _id;

        public ApiTests()
        {
            _webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = _webAppFactory.CreateDefaultClient();
            _id = 20;
        }

        [TestMethod]
        [TestPriority(2)]
        public async Task bGetPaymentMethods_ReturnsSuccessStatusCode()
        {

            // Act
            var response = await _httpClient.GetAsync("/api/PaymentMethod");

            // Assert
            response.EnsureSuccessStatusCode();

            // Pobierz treœæ odpowiedzi
            var content = await response.Content.ReadAsStringAsync();

            // Wyœwietl treœæ odpowiedzi w konsoli
            Console.WriteLine($"Treœæ odpowiedzi: {content}");
        }

        [TestMethod]
        [TestPriority(1)]
        public async Task aPostPaymentMethod_ReturnsSuccessStatusCode()
        {
            // Arrange: Przygotuj dane do wys³ania
            var paymentMethodData = new
            {
                id = 0, // ID PaymentMethod (mo¿esz zostawiæ 0, jeœli chcesz, ¿eby baza danych nadawa³a ID)
                isActive = true, // Status aktywnoœci
                name = "Blik" // Nazwa metody p³atnoœci
            };

            // Act: Wyœlij ¿¹danie POST
            var response = await _httpClient.PostAsJsonAsync("/api/PaymentMethod", paymentMethodData);

            // Assert: SprawdŸ, czy odpowiedŸ jest sukcesem
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        [TestPriority(3)]
        public async Task cPutPaymentMethod_ReturnsSuccessStatusCode()
        {
            var paymentMethodId = _id; // Tutaj podaj istniej¹ce ID

            // Arrange: Przygotuj dane do aktualizacji
            var updatedPaymentMethodData = new
            {
                Name = "PayPal"
            };

            // Act: Wyœlij ¿¹danie PUT z odpowiednim ID
            var response = await _httpClient.PutAsJsonAsync($"/api/PaymentMethod/{paymentMethodId}", updatedPaymentMethodData);

            // Assert: SprawdŸ, czy odpowiedŸ jest sukcesem
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        [TestPriority(4)]
        public async Task dGetPaymentMethodById_ReturnsSuccessStatusCode()
        {
            // Przygotuj ID istniej¹cego PaymentMethod
            var paymentMethodId = _id; // Tutaj podaj istniej¹ce ID

            // Act
            var response = await _httpClient.GetAsync($"/api/PaymentMethod/{paymentMethodId}");

            // Assert
            response.EnsureSuccessStatusCode();

            // Pobierz treœæ odpowiedzi
            var content = await response.Content.ReadAsStringAsync();

            // Wyœwietl treœæ odpowiedzi w konsoli
            Console.WriteLine($"Treœæ odpowiedzi: {content}");
        }

        [TestMethod]
        public async Task eDeletePaymentMethod_ReturnsSuccessStatusCode()
        {
            var paymentMethodIdToDelete = _id;

            var response = await _httpClient.DeleteAsync($"/api/PaymentMethod/{paymentMethodIdToDelete}");

            response.EnsureSuccessStatusCode();
        }
    }
}