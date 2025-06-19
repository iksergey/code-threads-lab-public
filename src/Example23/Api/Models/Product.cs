public record Product(
    int Id,
    string Title,
    decimal Price,
    string Description,
    Category Category,
    DateTime CreationAt,
    DateTime UpdatedAt
);
