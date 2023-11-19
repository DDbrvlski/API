using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;
using BookStoreAPI.Services;
using System.Net.Http.Json;
using BookStoreAPI.Services.Email;

namespace BookStoreAPI.Test
{
    [TestClass]
    public class ApiTests
    {
        private readonly WebApplicationFactory<Program> _webAppFactory;
        private readonly HttpClient _httpClient;
        private int _id;
        private readonly EmailService _emailService;

        public ApiTests()
        {
            _webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = _webAppFactory.CreateDefaultClient();
            //_id = 20;
        }

        //[TestMethod]
        //public void SendEmail_ShouldSendEmailSuccessfully()
        //{
        //    // Arrange
        //    var emailConfig = new EmailConfiguration();
        //    emailConfig.Email = "spellariumemailsender@gmail.com";
        //    emailConfig.Password = "oanm jxta qitf luus";
        //    var emailService = new EmailService(emailConfig);

        //    //emailService.SetSmtpClient(smtpClient);

        //    string to = "nologicc00@gmail.com"; // Tw�j drugi adres e-mail
        //    string subject = "Test Email Subject";
        //    string body = "Test Email Body";

        //    // Act
        //    emailService.SendEmail(to, subject, body);

        //    // Assert
        //    // Mo�esz doda� asercje tutaj, aby sprawdzi�, czy e-mail zosta� pomy�lnie wys�any
        //}

        //[TestMethod]
        //[TestPriority(2)]
        //public async Task bGetPaymentMethods_ReturnsSuccessStatusCode()
        //{

        //    // Act
        //    var response = await _httpClient.GetAsync("/api/PaymentMethod");

        //    // Assert
        //    response.EnsureSuccessStatusCode();

        //    // Pobierz tre�� odpowiedzi
        //    var content = await response.Content.ReadAsStringAsync();

        //    // Wy�wietl tre�� odpowiedzi w konsoli
        //    Console.WriteLine($"Tre�� odpowiedzi: {content}");
        //}

        //[TestMethod]
        //[TestPriority(1)]
        //public async Task aPostPaymentMethod_ReturnsSuccessStatusCode()
        //{
        //    // Arrange: Przygotuj dane do wys�ania
        //    var paymentMethodData = new
        //    {
        //        id = 0, // ID PaymentMethod (mo�esz zostawi� 0, je�li chcesz, �eby baza danych nadawa�a ID)
        //        isActive = true, // Status aktywno�ci
        //        name = "Blik" // Nazwa metody p�atno�ci
        //    };

        //    // Act: Wy�lij ��danie POST
        //    var response = await _httpClient.PostAsJsonAsync("/api/PaymentMethod", paymentMethodData);

        //    // Assert: Sprawd�, czy odpowied� jest sukcesem
        //    response.EnsureSuccessStatusCode();
        //}

        //[TestMethod]
        //[TestPriority(3)]
        //public async Task cPutPaymentMethod_ReturnsSuccessStatusCode()
        //{
        //    var paymentMethodId = _id; // Tutaj podaj istniej�ce ID

        //    // Arrange: Przygotuj dane do aktualizacji
        //    var updatedPaymentMethodData = new
        //    {
        //        Name = "PayPal"
        //    };

        //    // Act: Wy�lij ��danie PUT z odpowiednim ID
        //    var response = await _httpClient.PutAsJsonAsync($"/api/PaymentMethod/{paymentMethodId}", updatedPaymentMethodData);

        //    // Assert: Sprawd�, czy odpowied� jest sukcesem
        //    response.EnsureSuccessStatusCode();
        //}

        //[TestMethod]
        //[TestPriority(4)]
        //public async Task dGetPaymentMethodById_ReturnsSuccessStatusCode()
        //{
        //    // Przygotuj ID istniej�cego PaymentMethod
        //    var paymentMethodId = _id; // Tutaj podaj istniej�ce ID

        //    // Act
        //    var response = await _httpClient.GetAsync($"/api/PaymentMethod/{paymentMethodId}");

        //    // Assert
        //    response.EnsureSuccessStatusCode();

        //    // Pobierz tre�� odpowiedzi
        //    var content = await response.Content.ReadAsStringAsync();

        //    // Wy�wietl tre�� odpowiedzi w konsoli
        //    Console.WriteLine($"Tre�� odpowiedzi: {content}");
        //}

        //[TestMethod]
        //public async Task eDeletePaymentMethod_ReturnsSuccessStatusCode()
        //{
        //    var paymentMethodIdToDelete = _id;

        //    var response = await _httpClient.DeleteAsync($"/api/PaymentMethod/{paymentMethodIdToDelete}");

        //    response.EnsureSuccessStatusCode();
        //}
    }
}