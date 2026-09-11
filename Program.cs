using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsumerAdviceApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // URL da API fornecida no exercício
            string endpoint = "https://api.adviceslip.com/advice";

            // Instanciando o HttpClient para fazer a requisição
            using HttpClient client = new HttpClient();

            try
            {
                // Realiza a requisição GET
                HttpResponseMessage response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                // Lê o corpo da resposta como string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Desserializa o JSON para os nossos objetos C#
                AdviceResponse adviceData = JsonSerializer.Deserialize<AdviceResponse>(responseBody);

                // Imprime no formato solicitado
                if (adviceData != null && adviceData.Slip != null)
                {
                    Console.WriteLine("Conselho de Hoje:");
                    Console.WriteLine(adviceData.Slip.Advice);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao buscar o conselho: {ex.Message}");
            }
        }
    }

    // Classes para mapear o retorno JSON da API
    public class AdviceResponse
    {
        [JsonPropertyName("slip")]
        public Slip Slip { get; set; }
    }

    public class Slip
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("advice")]
        public string Advice { get; set; }
    }
}