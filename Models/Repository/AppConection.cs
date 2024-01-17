namespace CadastroClientes.Models.Repository
{
    public class AppConection
    {

        public string ConnectionString { get; set; }

        // CONTRUTOR PARA CONEXÃO 
        public AppConection (IConfiguration configuration)
        {
            // OBS : O NOME DA VARIÁVEL ENTRE ASPAS DEVE SER O MESMO DO APP SETTINGS 
            ConnectionString = configuration.GetConnectionString("ConnString");
        }
    }
}
