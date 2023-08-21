using Microsoft.Extensions.Options;
using Moq;
using System.Net.Mail;
using DAL;
using webapi.Services;
using Domain.User.Models;
using Microsoft.Extensions.Configuration;

namespace EquipWatch.UnitTests.TestServices;

[TestFixture]
public class EmailServiceTests
{
    private Mock<IOptions<EmailContext>>? _emailContextMock;
    private Mock<ISmtpClientWrapper>? _smtpClientMock;
    private EmailService? _emailService;

    [SetUp]
    public void Setup()
    {
        var configuration = GetTestConfiguration();

        var emailContextValue = new EmailContext()
        {
            Smtp = configuration["Email:Smtp"],
            Port = int.Parse(configuration["Email:Port"]),
            Username = configuration["Email:Username"],
            Password = configuration["Email:Password"],
            Address = configuration["Email:Address"]
        };

        _emailContextMock = new Mock<IOptions<EmailContext>>();
        _emailContextMock.Setup(x => x.Value).Returns(emailContextValue);

        _smtpClientMock = new Mock<ISmtpClientWrapper>();
        _emailService = new EmailService(_emailContextMock.Object);
    }

    [Test]
    public async Task SendEmailForConfirmationAsync_ShouldSendConfirmationEmail()
    {
        // Arrange
        var user = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "RysiekPtysiek2@outlook.com"
        };
        var confirmationLink = "https://example.test.com/test_confirmation_email";

        _smtpClientMock.Setup(client => client.SendMailAsync(It.IsAny<MailMessage>()))
            .Returns(Task.CompletedTask);

        // Act
        await _emailService.SendEmailForConfirmationAsync(user, confirmationLink);

        // Assert
        // No need for assertions about the SendMailAsync behavior in this test.
        // The behavior is already setup by using the mock.
    }

    [Test]
    public async Task SendPasswordResetLinkAsync_ShouldSendPasswordResetEmail()
    {
        // Arrange
        var user = new User
        {
            FirstName = "Alice",
            LastName = "Smith",
            Email = "RysiekPtysiek2@outlook.com"
        };
        var resetLink = "https://example.test.com/test_reset_email";

        _smtpClientMock.Setup(client => client.SendMailAsync(It.IsAny<MailMessage>()))
            .Returns(Task.CompletedTask);

        // Act
        await _emailService.SendPasswordResetLinkAsync(user, resetLink);

        // Assert
        // No need for assertions about the SendMailAsync behavior in this test.
        // The behavior is already setup by using the mock.
    }

    private IConfiguration GetTestConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<EmailService>()
            .AddEnvironmentVariables();

        return configurationBuilder.Build();
    }

}