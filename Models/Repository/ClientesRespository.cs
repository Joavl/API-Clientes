using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Net.Security;

namespace CadastroClientes.Models.Repository

{
    public class ClientesRespository
    {
        private AppConection _appConfig;

        public ClientesRespository(AppConection appConfig)
        {
            _appConfig = appConfig;
        }

        public void Salvar(Clientes clientes)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, SslPolicyErrors) => false;

                // ESSE BLOCO FAZ A CONEXÃO ENTRE O BACKEND E O BANCO DE DADOS , USANDO OS ATRIBUTOS DE CONEXÃO DO APPSETTINGS 
                using (SqlConnection connection = new SqlConnection(_appConfig.ConnectionString))
                {
                    // ABRIMOS A CONEXÃO E CRIAMOS O COMANDO SQL , PASSANDO COMO PARÂMETRO A PROCEDURE E APÓS A CONNECTION
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("PROC_INSERIR_CLIENTES", connection))
                    {
                        // ESTABELECEMOS O TIPO DE COMANDO COMO STORED PROCEDURE E APÓS ISSO PASSAMOS OS VALORES RELATIVOS A PROCEDURE 
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", clientes.IdCliente);
                        cmd.Parameters.AddWithValue("@Documento", clientes.Documento);
                        cmd.Parameters.AddWithValue("@Nome", clientes.Nome);
                        cmd.Parameters.AddWithValue("@Sexo", clientes.Sexo);
                        cmd.Parameters.AddWithValue("@Email", clientes.Email);
                        cmd.Parameters.AddWithValue("@Telefone", clientes.Telefone);
                        cmd.Parameters.AddWithValue("@Fax", clientes.Fax);
                        cmd.Parameters.AddWithValue("@UF", clientes.UF);


                        // MÉTODO PARA EXECUTAR O COMANDO SQL 
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
        }

        public void Atualizar(Clientes clientes)
        {
            try
            {
                // PRIMEIRO PASSAMOS A CONEXÃO COM O APP CONFIG E APÓS ISSO ABRIMOS A CONEXÃO
                using (SqlConnection connection = new SqlConnection(_appConfig.ConnectionString))
                {
                    connection.Open();
                    // INSTANCIAMOS UM OBJETO DO TIPO SQL COMMAND E PASSAMOS COMO PARÂMETRO O NOME DA PROCEDURE E A VARIAVEL DA SQL CONNECTION 
                    using (SqlCommand cmd = new SqlCommand("PROCEDURE_UPDATE_CLIENTES", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", clientes.IdCliente);
                        cmd.Parameters.AddWithValue("@Documento", clientes.Documento);
                        cmd.Parameters.AddWithValue("@Nome", clientes.Nome);
                        cmd.Parameters.AddWithValue("@Sexo", clientes.Sexo);
                        cmd.Parameters.AddWithValue("@Email", clientes.Email);
                        cmd.Parameters.AddWithValue("@Telefone", clientes.Telefone);
                        cmd.Parameters.AddWithValue("@Fax", clientes.Fax);
                        cmd.Parameters.AddWithValue("@UF", clientes.UF);

                        // MÉTODO PARA EXECUTAR O COMANDO SQL 
                        cmd.ExecuteNonQuery();
                    }


                }
            }

            catch (Exception ex)
            {

            }
        }

        public List<Clientes> Listar()
        {
            List<Clientes> retorno = new List<Clientes>();
            try
            {
                // PRIMEIRO PASSAMOS A CONEXÃO COM O APP CONFIG E APÓS ISSO ABRIMOS A CONEXÃO
                using (SqlConnection connection = new SqlConnection(_appConfig.ConnectionString))
                {
                    connection.Open();
                    // INSTANCIAMOS UM OBJETO DO TIPO SQL COMMAND E PASSAMOS COMO PARÂMETRO O NOME DA PROCEDURE E A VARIAVEL DA SQL CONNECTION 
                    using (SqlCommand cmd = new SqlCommand("PROC_LISTAR_CLIENTES", connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            // ENQUANTO O BLOCO DE EXECUÇÃO ESTIVER SENDO POSSÍVEL LER , UM NOVO OBJETO DA CLASSE CLIENTE É CRIADO E APÓS ISSO ADICIONADO A LISTA QUE POR FIM SERÁ RETORNADA AO FINAL DO MÉTODO 
                            while (reader.Read())
                            {
                                Clientes clientes = new Clientes();
                                clientes.IdCliente = Convert.ToInt32(reader["IdCliente"].ToString());
                                clientes.UF = reader["UF"].ToString();
                                clientes.Email = reader["Email"].ToString();
                                clientes.Telefone = reader["Telefone"].ToString();
                                clientes.Documento = reader["Documento"].ToString();
                                clientes.Nome = reader["Nome"].ToString();
                                clientes.Fax = reader["Fax"].ToString();
                                clientes.Sexo = reader["Sexo"].ToString();

                                retorno.Add(clientes);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return retorno;
        }

        public bool Deletar(int idCliente)
        {
            bool retorno = false;

            try
            {
                // PRIMEIRO PASSAMOS A CONEXÃO COM O APP CONFIG E APÓS ISSO ABRIMOS A CONEXÃO
                using (SqlConnection connection = new SqlConnection(_appConfig.ConnectionString))
                {
                    connection.Open();
                    // INSTANCIAMOS UM OBJETO DO TIPO SQL COMMAND E PASSAMOS COMO PARÂMETRO O NOME DA PROCEDURE E A VARIAVEL DA SQL CONNECTION 
                    using (SqlCommand cmd = new SqlCommand("PROC_DELETAR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                        // MÉTODO PARA EXECUTAR O COMANDO SQL 
                        int linhas = cmd.ExecuteNonQuery();

                        if (linhas > 0)
                        {
                            retorno = true;
                        }


                    }
                }
            }

            catch (Exception ex)
            {

            }
            return retorno;
        }

        public Clientes GetClientes(int idCliente)
        {
            Clientes clientes = null;

            try
            {
                // PRIMEIRO PASSAMOS A CONEXÃO COM O APP CONFIG E APÓS ISSO ABRIMOS A CONEXÃO
                using (SqlConnection connection = new SqlConnection(_appConfig.ConnectionString))
                {
                    connection.Open();
                    // INSTANCIAMOS UM OBJETO DO TIPO SQL COMMAND E PASSAMOS COMO PARÂMETRO O NOME DA PROCEDURE E A VARIAVEL DA SQL CONNECTION 
                    using (SqlCommand cmd = new SqlCommand("PROC_BUSCAR_CLIENTE", connection))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            // SE O ITEM ESTIVER NO DATA READER , ESTE É RETORNADO PELO MÉTODO 
                            if (reader.Read())
                            {
                                clientes = new Clientes();
                                clientes.IdCliente = Convert.ToInt32(reader["IdCliente"].ToString());
                                clientes.UF = reader["UF"].ToString();
                                clientes.Email = reader["Email"].ToString();
                                clientes.Telefone = reader["Telefone"].ToString();
                                clientes.Documento = reader["Documento"].ToString();
                                clientes.Nome = reader["Nome"].ToString();
                                clientes.Fax = reader["Fax"].ToString();
                                clientes.Sexo = reader["Sexo"].ToString();

                            }
                        }
                    }
                }
            }

            catch { Exception ex; }
            {

            }

            return clientes;
        }
    }
}

