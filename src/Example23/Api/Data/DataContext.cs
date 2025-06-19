public class DataContext
{
    public List<Category> Categories
    {
        get
        {
            return new List<Category>
            {
                new Category(
                    1,
                    "Одежда",
                    DateTime.Parse("2025-05-27T19:58:17.000Z"),
                    DateTime.Parse("2025-05-27T19:58:17.000Z"),
                    "/categories/1/description"
                ),
                new Category(
                    2,
                    "Электроника",
                    DateTime.Parse("2025-05-27T19:58:17.000Z"),
                    DateTime.Parse("2025-05-27T19:58:17.000Z"),
                    "/categories/2/description"
                ),
                new Category(
                    3,
                    "Домашний декор",
                    DateTime.Parse("2025-05-27T19:58:17.000Z"),
                    DateTime.Parse("2025-05-27T19:58:17.000Z"),
                    "/categories/3/description"
                )
            };
        }
    }

    public List<Product> Products
    {
        get
        {
            return new List<Product>
        {
            new Product(
                1,
                "Тёплый вязаный свитер",
                2499.99m,
                "Мягкий свитер из 100% шерсти с высоким воротом. Идеален для зимних прогулок.",
                Categories[0],
                DateTime.Parse("2025-05-27T19:58:17.000Z"),
                DateTime.Parse("2025-05-27T19:58:17.000Z")
            ),
            new Product(
                2,
                "Футболка с принтом",
                799.50m,
                "Хлопковая футболка свободного кроя с ярким принтом. Лёгкая и удобная.",
                Categories[0],
                DateTime.Parse("2025-05-27T19:58:17.000Z"),
                DateTime.Parse("2025-05-27T19:58:17.000Z")
            ),
            new Product(
                3,
                "Беспроводные наушники",
                4599.00m,
                "Шумоподавляющие наушники с длительным временем работы до 24 часов без подзарядки.",
                Categories[1],
                DateTime.Parse("2025-05-27T19:58:17.000Z"),
                DateTime.Parse("2025-05-27T19:58:17.000Z")
            ),
            new Product(
                4,
                "Настольная лампа LED",
                1299.00m,
                "Регулируемая лампа с тремя режимами яркости и гибким штативом из алюминия.",
                Categories[2],
                DateTime.Parse("2025-05-27T19:58:17.000Z"),
                DateTime.Parse("2025-05-27T19:58:17.000Z")
            ),
            new Product(
                5,
                "Диванная подушка",
                599.00m,
                "Уютная декоративная подушка размером 45×45 см с мягким наполнителем из холлофайбера.",
                Categories[2],
                DateTime.Parse("2025-05-27T19:58:17.000Z"),
                DateTime.Parse("2025-05-27T19:58:17.000Z")
            )
        };
        }
    }

}