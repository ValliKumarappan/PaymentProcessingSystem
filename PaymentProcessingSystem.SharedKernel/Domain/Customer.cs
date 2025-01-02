using System.ComponentModel.DataAnnotations;

namespace PaymentProcessingSystem.SharedKernel.Domain;
public static class RegexPatterns
{
    public const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
}
public class Customer : Entity
{
    public int Id { get; set; }
    public string Name { get; set; }
    [Required]
    [RegularExpression(RegexPatterns.regex, ErrorMessage = "Invalid Email Id.")]
    public string Email { get; set; }

    public Customer()
    {

    }

    public Customer(string name, string email)
    {
 
        Name = name;
        Email = email;
        CreatedOn = DateTime.Now;
    }
    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
        UpdatedOn = DateTime.Now;
    }
}
