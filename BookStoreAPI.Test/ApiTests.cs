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

            // Pobierz tre�� odpowiedzi
            var content = await response.Content.ReadAsStringAsync();

            // Wy�wietl tre�� odpowiedzi w konsoli
            Console.WriteLine($"Tre�� odpowiedzi: {content}");
        }

        [TestMethod]
        [TestPriority(1)]
        public async Task aPostPaymentMethod_ReturnsSuccessStatusCode()
        {
            // Arrange: Przygotuj dane do wys�ania
            var paymentMethodData = new
            {
                id = 0, // ID PaymentMethod (mo�esz zostawi� 0, je�li chcesz, �eby baza danych nadawa�a ID)
                isActive = true, // Status aktywno�ci
                name = "Blik" // Nazwa metody p�atno�ci
            };

            // Act: Wy�lij ��danie POST
            var response = await _httpClient.PostAsJsonAsync("/api/PaymentMethod", paymentMethodData);

            // Assert: Sprawd�, czy odpowied� jest sukcesem
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        [TestPriority(3)]
        public async Task cPutPaymentMethod_ReturnsSuccessStatusCode()
        {
            var paymentMethodId = _id; // Tutaj podaj istniej�ce ID

            // Arrange: Przygotuj dane do aktualizacji
            var updatedPaymentMethodData = new
            {
                Name = "PayPal"
            };

            // Act: Wy�lij ��danie PUT z odpowiednim ID
            var response = await _httpClient.PutAsJsonAsync($"/api/PaymentMethod/{paymentMethodId}", updatedPaymentMethodData);

            // Assert: Sprawd�, czy odpowied� jest sukcesem
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        [TestPriority(4)]
        public async Task dGetPaymentMethodById_ReturnsSuccessStatusCode()
        {
            // Przygotuj ID istniej�cego PaymentMethod
            var paymentMethodId = _id; // Tutaj podaj istniej�ce ID

            // Act
            var response = await _httpClient.GetAsync($"/api/PaymentMethod/{paymentMethodId}");

            // Assert
            response.EnsureSuccessStatusCode();

            // Pobierz tre�� odpowiedzi
            var content = await response.Content.ReadAsStringAsync();

            // Wy�wietl tre�� odpowiedzi w konsoli
            Console.WriteLine($"Tre�� odpowiedzi: {content}");
        }

        [TestMethod]
        [TestPriority(5)]
        public async Task eDeletePaymentMethod_ReturnsSuccessStatusCode()
        {
            // Przygotuj ID PaymentMethod do usuni�cia
            var paymentMethodIdToDelete = _id; // Tutaj podaj ID do usuni�cia

            // Act: Wy�lij ��danie DELETE z odpowiednim ID
            var response = await _httpClient.DeleteAsync($"/api/PaymentMethod/{paymentMethodIdToDelete}");

            // Assert: Sprawd�, czy odpowied� jest sukcesem
            response.EnsureSuccessStatusCode();
        }
    }
}