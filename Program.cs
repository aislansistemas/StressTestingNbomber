
using NBomber.Contracts.Stats;
using NBomber.CSharp;

using var httpClient = new HttpClient();

var scenario = Scenario.Create("Primeiro teste de carga", async context => {
    var response = await httpClient.GetAsync("https://google.com.br");

    return response.IsSuccessStatusCode
        ? Response.Ok()
        : Response.Fail();
})
.WithoutWarmUp() //removendo aquecimento
.WithLoadSimulations(
    Simulation.Inject(rate: 10, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30))
); // define carga de teste

NBomberRunner.RegisterScenarios(scenario).Run();