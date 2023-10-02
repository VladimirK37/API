using SPEEDBOXAPI.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/calculatortariff", (int weight, int height, int width, int length, int fromcode, int tocode) =>
{
    var Api = new API();
    var token = Api.GetToken();
    HttpClient client = new HttpClient();
    //добавляем заголовок Content-Type
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.access_token}");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    height /= 10;
    width /= 10;
    length /= 10;
    //Example
    //Location fromlocation = new Location { code = 270 };
    //Location tolocation = new Location { code = 44 };
    //Package package = new Package { height = 10, length = 10, weight = 4000, width = 10 };
    Location fromLocation = new Location { code = fromcode };
    Location toLocation = new Location { code = tocode };
    Package Package = new Package { height = height, length = length, weight = weight, width = width };
    Item requestData = new Item { from_location = fromLocation, to_location = toLocation, packages = Package };
    var x = System.Text.Json.JsonSerializer.Serialize(requestData);

    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.edu.cdek.ru/v2/calculator/tarifflist");
    httpWebRequest.Headers.Add("Authorization", $"Bearer {token.access_token}");
    httpWebRequest.ContentType = "application/json";
    httpWebRequest.Method = "POST";

    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
    {
        streamWriter.Write(x);
    }

    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    var rootobject = new ShippingCost();
    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
    {
        var result = streamReader.ReadToEnd();
        rootobject = JsonConvert.DeserializeObject<ShippingCost>(result);
    }
    return rootobject;
})
.WithName("PostCalculatorTariff");


app.Run();

public class API
{
    public TokenResponse GetToken()
    {
        // Указываем базовый адрес сервера авторизации
        string baseAddress = "https://api.edu.cdek.ru/v2/oauth/token?parameters";

        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseAddress);


            // Формирование тела запроса
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("grant_type", "client_credentials");
            postData.Add("client_id", "EMscd6r9JnFiQ3bLoyjJY6eM78JrJceI");
            postData.Add("client_secret", "PjLZkKBHEiLK3YsjtNrt3TGNG0ahs3kG");


            // Отправка POST-запроса на сервер авторизации
            HttpResponseMessage response = client.PostAsync(client.BaseAddress, new FormUrlEncodedContent(postData)).Result;
            if (response.IsSuccessStatusCode)
            {
                string responsestring = response.Content.ReadAsStringAsync().Result;
                TokenResponse tokenresponse = JsonConvert.DeserializeObject<TokenResponse>(responsestring);
                return tokenresponse;

                //Console.WriteLine("access token retrieved: " + tokenresponse.access_token);
            }
            else
            {
                return null;
                //Console.WriteLine("request failed with status code: " + response.StatusCode);
            }

        }
    }

}