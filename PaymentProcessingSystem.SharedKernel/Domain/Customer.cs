using System.ComponentModel.DataAnnotations;

namespace PaymentProcessingSystem.SharedKernel.Domain;

public class Customer : Entity
{
    public int Id { get; set; }
    public string Name { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
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
