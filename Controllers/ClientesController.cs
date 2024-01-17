using CadastroClientes.Models;
using CadastroClientes.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CadastroClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        //  INSTÂNCIA DE ICONFIGURATION CRIADA EM MEMÓRIA PARA CARREGAR O APPSETTINGS.JSON 
        IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();


        [HttpPost("Salvar")]
        public object Salvar([FromBody] Clientes cliente)
        {
            try
            {
                var appConfig = new AppConection(configuration);

                ClientesRespository clientes = new ClientesRespository(appConfig);

                var retornoCliente = clientes.GetClientes(cliente.IdCliente);

                if (retornoCliente != null)
                {
                    clientes.Atualizar(cliente);
                }
                else
                {
                    clientes.Salvar(cliente);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }

            return null;
        }

        [HttpPost("Alterar")]

        public object Alterar([FromBody] Clientes cliente)
        {

            try
            {
                var appConfig = new AppConection(configuration);
                ClientesRespository clientes = new ClientesRespository(appConfig);
                clientes.Atualizar(cliente);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        [HttpGet("Listar")]
        public object Listar()
        {
            List<Clientes> clientes = null;
            try
            {
                var appConfig = new AppConection(configuration);
                ClientesRespository clientesRespository = new ClientesRespository(appConfig);

                clientes = clientesRespository.Listar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }

            return clientes;
        }

        [HttpDelete("Deletar")]
        public object Deletar(int IdCliente)
        {
            try
            {
                var appConfig = new AppConection(configuration);
                ClientesRespository clientes = new ClientesRespository(appConfig);

                bool retornoDelete = clientes.Deletar(IdCliente);

                return retornoDelete;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        [HttpGet("GetCliente")]

        public object ListarCliente(int IdCliente)
        {
            try
            {
                var appConfig = new AppConection(configuration);
                ClientesRespository clientes = new ClientesRespository(appConfig);

                var retornoCliente = clientes.GetClientes(IdCliente);

                return retornoCliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

    }
}
