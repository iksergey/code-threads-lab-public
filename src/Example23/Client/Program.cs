using System.Text.Json;

HttpClient httpClient = new HttpClient();
string url = "http://localhost:5053/products/";
Task<string> res = httpClient.GetStringAsync(url).ContinueWith(task =>
{
    string productList = task.Result;
    JsonDocument json = JsonDocument.Parse(productList);

    string productUrl = json.RootElement[0].GetString()!;
    return httpClient.GetStringAsync(productUrl);
}).Unwrap().ContinueWith(productDataTask =>
{
    string productData = productDataTask.Result;
    JsonDocument productJson = JsonDocument.Parse(productData);

    string categoryUrl = productJson
        .RootElement
        .GetProperty("category")
        .GetProperty("descriptionUrl")
        .GetString()!;

    return httpClient.GetStringAsync(categoryUrl);
}).Unwrap().ContinueWith(productDescTask =>
{
    string desc = productDescTask.Result;
    return desc;
});

Console.WriteLine(res.Result);
