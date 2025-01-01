namespace PaymentProcessingSystem.SharedKernel.FilterModels;

public class CustomerFilters
{
    public CustomerFilters()
    {
        if (pageSize <= 0) pageSize = 5;
        if (page <= 0) page = 1;
    }
    public int page { get; set; }
    public int pageSize { get; set; }
}
