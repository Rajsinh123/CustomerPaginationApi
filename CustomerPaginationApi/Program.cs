
using CustomerPaginationApi;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Step 1: Create sample customer data
var customers = new List<Customer>
{
    new() { Id = 1, Name = "Rahul", Email = "rahul@example.com" },
    new() { Id = 2, Name = "Priya", Email = "priya@example.com" },
    new() { Id = 3, Name = "Amit", Email = "amit@example.com" },
    new() { Id = 4, Name = "Sneha", Email = "sneha@example.com" },
    new() { Id = 5, Name = "Vijay", Email = "vijay@example.com" },
    new() { Id = 6, Name = "Neha", Email = "neha@example.com" },
    new() { Id = 7, Name = "Rohan", Email = "rohan@example.com" },
    new() { Id = 8, Name = "Pooja", Email = "pooja@example.com" },
    new() { Id = 9, Name = "Akash", Email = "akash@example.com" },
    new() { Id = 10, Name = "Kiran", Email = "kiran@example.com" },
    new() { Id = 11, Name = "Anjali", Email = "anjali@example.com" },
    new() { Id = 12, Name = "Sagar", Email = "sagar@example.com" }
};

// Step 2: Create the pagination endpoint
app.MapGet("/api/customers", (
    int pageNumber = 1,
    int pageSize = 5) =>
{
    // Step 3: Validate inputs
    if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
    {
        return Results.BadRequest(
            "Use pageNumber >= 1 and pageSize between 1 and 100.");
    }

    // Step 4: Calculate how many records to skip
    int skip = (pageNumber - 1) * pageSize;

    // Step 5: Get the requested page
    var data = customers
        .OrderBy(c => c.Id)
        .Skip(skip)
        .Take(pageSize)
        .ToList();

    // Step 6: Calculate pagination information
    int totalRecords = customers.Count;

    int totalPages = (int)Math.Ceiling(
        (double)totalRecords / pageSize);

    // Step 7: Return the response
    return Results.Ok(new
    {
        PageNumber = pageNumber,
        PageSize = pageSize,
        TotalRecords = totalRecords,
        TotalPages = totalPages,
        Data = data
    });
});

app.Run();