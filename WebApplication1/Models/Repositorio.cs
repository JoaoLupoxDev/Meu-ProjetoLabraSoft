using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1;
using WebApplication1.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebApplication1
{
    public class Repositorio
    {

        //funções de query (SQL)
        private static string connectionString = ConfigurationManager.ConnectionStrings["BancoLabrasoft"].ConnectionString; //string de conexao entre o aspx e o BD
        public static void SalvarBolsista(Bolsista bolsista)
        {
            //string para inserção dos dados do bolsista
            string sql = "INSERT INTO Bolsista (Nome, Matricula, CPF, Sexo, DataNascimento) Values (@Nome, @Matricula, @CPF, @Sexo, @DataNascimento)";
            using (SqlConnection conn = new SqlConnection(Repositorio.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", bolsista.Nome);
                    cmd.Parameters.AddWithValue("@Matricula", bolsista.Matricula);
                    cmd.Parameters.AddWithValue("@CPF", bolsista.CPF);
                    cmd.Parameters.AddWithValue("@Sexo", bolsista.Sexo);
                    cmd.Parameters.AddWithValue("@DataNascimento", bolsista.DataNascimento);

                    conn.Open(); //abre a porta da comunicacao com o BD
                    cmd.ExecuteNonQuery(); //executa o comando no banco de dados
                }
            }
        }

        public static List<Bolsista> ObterDadosBolsista()
        {
            var Lista = new List<Bolsista>();
            string sql = "SELECT Id, Nome, CPF, Matricula, DataNascimento, Sexo FROM Bolsista";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    //ExecuteReader() para consulta que retorna dados
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lógica para ler linha por linha da resposta do banco
                        while (reader.Read())
                        {
                            Lista.Add(MapearBolsista(reader));
                        }
                    }
                }
            }
            return Lista;
        }
        public static int ContarBolsistas()
        {
            string sql = "SELECT COUNT(*) FROM Bolsista";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    // ExecuteScalar retorna um único valor do banco
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public static List<Bolsista> ObterPorSexo(string sexo)
        {
            var lista = new List<Bolsista>();
            string sql = "SELECT ID, Matricula, Nome, CPF, Sexo, DataNascimento FROM Bolsista WHERE Sexo = @Sexo";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Sexo", sexo);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MapearBolsista(reader));
                        }
                    }
                }
            }

            return lista;
        }
        public static List<Bolsista> ObterOrdenadoPorNome()
        {
            var lista = new List<Bolsista>();
            string sql = "SELECT ID, Matricula, Nome, CPF, Sexo, DataNascimento FROM Bolsista ORDER BY Nome ASC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MapearBolsista(reader));
                        }
                    }
                }
            }

            return lista;
        }

        private static Bolsista MapearBolsista(SqlDataReader reader)
        {
            return new Bolsista
            {
                ID = Convert.ToInt32(reader["ID"]),
                Matricula = Convert.ToInt64(reader["Matricula"]),
                Nome = reader["Nome"].ToString(),
                CPF = reader["CPF"].ToString(),
                Sexo = reader["Sexo"].ToString(),
                DataNascimento = Convert.ToDateTime(reader["DataNascimento"])
            };
        }

        public static int SalvarProjeto(Projeto projeto)
        {
            // Retorna o ID gerado automaticamente (IDENTITY) na criação do Projeto
            string sql = @"INSERT INTO Projeto (Titulo, AreaConhecimento, VerbaAprovada, ValorBolsaIndividual, CoordenadorID) 
                   OUTPUT INSERTED.ID
                   VALUES (@Titulo, @AreaConhecimento, @VerbaAprovada, @ValorBolsaIndividual, @CoordenadorID)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Titulo", projeto.Titulo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AreaConhecimento", projeto.AreaConhecimento ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@VerbaAprovada", projeto.Verba);
                    cmd.Parameters.AddWithValue("@ValorBolsaIndividual", projeto.ValorBolsa);
                    cmd.Parameters.AddWithValue("@CoordenadorID", projeto.Coordenador.ID);

                    conn.Open();
                    // ExecuteScalar executa o INSERT e retorna o valor de OUTPUT INSERTED.ID
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static Projeto MapearProjeto(SqlDataReader reader)
        {
            return new Projeto
            {
                ID = Convert.ToInt32(reader["ID"]),
                Titulo = reader["Titulo"].ToString(),
                AreaConhecimento = reader["AreaConhecimento"].ToString(),

                // Trata valores decimais/doubles que podem vir nulos do banco
                Verba = reader["VerbaAprovada"] != DBNull.Value
                    ? Convert.ToDouble(reader["VerbaAprovada"])
                    : 0.0,

                ValorBolsa = reader["ValorBolsaIndividual"] != DBNull.Value
                    ? Convert.ToDouble(reader["ValorBolsaIndividual"])
                    : 0.0,

                // Cria o objeto Coordenador apenas com o ID retornado
                Coordenador = new Coordenador
                {
                    ID = Convert.ToInt32(reader["CoordenadorID"])
                }
            };
        }

        public static List<Projeto> ObterDadosProjeto()
        {
            var Lista = new List<Projeto>();
            string sql = "SELECT Id, Titulo, VerbaAprovada, ValorBolsaIndividual, AreaConhecimento, CoordenadorID FROM Projeto";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    //ExecuteReader() para consulta que retorna dados
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Lógica para ler linha por linha da resposta do banco
                        while (reader.Read())
                        {
                            Lista.Add(MapearProjeto(reader));
                        }
                    }
                }
            }
            return Lista;
        }

        public static void SalvarProjetoBolsista(int projetoId, int bolsistaId)
        {
            if (projetoId <= 0 || bolsistaId <= 0)
            {
                throw new ArgumentException("O ID do Projeto e do Bolsista devem ser válidos.");
            }

            // Insere apenas se o Projeto e o Bolsista existirem no banco
            string sql = @"
        IF EXISTS (SELECT 1 FROM Projeto WHERE ID = @ProjetoID) 
           AND EXISTS (SELECT 1 FROM Bolsista WHERE ID = @BolsistaID)
        BEGIN
            INSERT INTO ProjetoBolsista (ProjetoID, BolsistaID, DataVinculo) 
            VALUES (@ProjetoID, @BolsistaID, GETDATE())
        END
        ELSE
        BEGIN
            RAISERROR('Projeto ou Bolsista não encontrado no banco de dados.', 16, 1)
        END";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProjetoID", projetoId);
                    cmd.Parameters.AddWithValue("@BolsistaID", bolsistaId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Bolsista> ObterBolsistasPorProjeto(int projetoId)
        {
            List<Bolsista> lista = new List<Bolsista>();
            string sql = @"SELECT b.ID, b.Nome, b.CPF 
                   FROM Bolsista b
                   INNER JOIN ProjetoBolsista pb ON b.ID = pb.BolsistaID
                   WHERE pb.ProjetoID = @ProjetoID";

            using (SqlConnection conn = new SqlConnection(Repositorio.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProjetoID", projetoId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Bolsista
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nome = reader["Nome"].ToString(),
                                // Adicione as outras propriedades da sua classe Bolsista aqui
                            });
                        }
                    }
                }
            }
            return lista;
        }
        // Método para SALVAR um Coordenador no Banco de Dados
        public static void SalvarCoordenador(Coordenador coordenador)
        {
            // A coluna ID ficou fora por ser IDENTITY/autoincremento no banco
            string sql = @"INSERT INTO Coordenador (Nome, CPF, Titulacao, AreaAtuacao, Email) 
                       VALUES (@Nome, @CPF, @Titulacao, @AreaAtuacao, @Email)";

            using (SqlConnection conn = new SqlConnection(Repositorio.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", coordenador.Nome);
                    cmd.Parameters.AddWithValue("@CPF", coordenador.CPF);
                    cmd.Parameters.AddWithValue("@Titulacao", coordenador.Titulacao ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AreaAtuacao", coordenador.AreaAtuacao ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", coordenador.Email ?? (object)DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<Coordenador> ObterDadosCoordenador()
        {
            List<Coordenador> lista = new List<Coordenador>();
            string sql = "SELECT ID, Nome, CPF, Titulacao, AreaAtuacao, Email FROM Coordenador";

            using (SqlConnection conn = new SqlConnection(Repositorio.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Coordenador
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nome = reader["Nome"].ToString(),
                                CPF = reader["CPF"].ToString(),
                                Titulacao = reader["Titulacao"] != DBNull.Value ? reader["Titulacao"].ToString() : string.Empty,
                                AreaAtuacao = reader["AreaAtuacao"] != DBNull.Value ? reader["AreaAtuacao"].ToString() : string.Empty,
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty
                            });
                        }
                    }
                }
            }
            return lista;
        }
        public static void SalvarDespesa(Despesa despesa)
        {
            string sql = @"INSERT INTO Despesa (Descricao, Categoria, Valor, DataDespesa, ProjetoID) 
                   VALUES (@Descricao, @Categoria, @Valor, @DataDespesa, @ProjetoID)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Descricao", despesa.Descricao ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Categoria", despesa.Categoria ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Valor", despesa.Valor);
                    cmd.Parameters.AddWithValue("@DataDespesa", despesa.DataDespesa);
                    cmd.Parameters.AddWithValue("@ProjetoID", despesa.ProjetoID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeletarCoordenador(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Verifica se o coordenador possui projetos vinculados
                string sqlVerifica = "SELECT COUNT(*) FROM dbo.Projeto WHERE CoordenadorID = @ID";
                using (SqlCommand cmdVerifica = new SqlCommand(sqlVerifica, conn))
                {
                    cmdVerifica.Parameters.AddWithValue("@ID", id);
                    int quantidadeProjetos = Convert.ToInt32(cmdVerifica.ExecuteScalar());

                    if (quantidadeProjetos > 0)
                    {
                        throw new InvalidOperationException("Não é possível excluir este coordenador pois ele está vinculado a um ou mais projetos.");
                    }
                }

                // 2. Se não houver projetos, realiza a exclusão
                string sqlCoordenador = "DELETE FROM dbo.Coordenador WHERE ID = @ID";
                using (SqlCommand cmdCoordenador = new SqlCommand(sqlCoordenador, conn))
                {
                    cmdCoordenador.Parameters.AddWithValue("@ID", id);
                    cmdCoordenador.ExecuteNonQuery();
                }
            }
        }
        public static void DeletarProjeto(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Verifica se o projeto possui despesas vinculadas
                string sqlVerificaDespesas = "SELECT COUNT(*) FROM dbo.Despesa WHERE ProjetoID = @ID";
                using (SqlCommand cmd = new SqlCommand(sqlVerificaDespesas, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                    {
                        throw new InvalidOperationException("Não é possível excluir este projeto pois existem despesas vinculadas a ele.");
                    }
                }

                // 2. Verifica se o projeto possui bolsistas vinculados (Tabela ProjetoBolsista)
                string sqlVerificaBolsistas = "SELECT COUNT(*) FROM dbo.ProjetoBolsista WHERE ProjetoID = @ID";
                using (SqlCommand cmd = new SqlCommand(sqlVerificaBolsistas, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                    {
                        throw new InvalidOperationException("Não é possível excluir este projeto pois existem bolsistas vinculados a ele.");
                    }
                }

                // 3. Se estiver tudo livre, realiza o DELETE do Projeto
                string sqlDelete = "DELETE FROM dbo.Projeto WHERE ID = @ID";
                using (SqlCommand cmdDelete = new SqlCommand(sqlDelete, conn))
                {
                    cmdDelete.Parameters.AddWithValue("@ID", id);
                    cmdDelete.ExecuteNonQuery();
                }
            }
        }
        public static void AtualizarCoordenador(Coordenador coordenador)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"UPDATE dbo.Coordenador 
                       SET Nome = @Nome, 
                           Email = @Email, 
                           Titulacao = @Titulacao, 
                           AreaAtuacao = @AreaAtuacao 
                       WHERE ID = @ID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", coordenador.ID);
                    cmd.Parameters.AddWithValue("@Nome", (object)coordenador.Nome ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)coordenador.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Titulacao", (object)coordenador.Titulacao ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AreaAtuacao", (object)coordenador.AreaAtuacao ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void AtualizarBolsistasDoProjeto(int projetoId, List<int> idsBolsistas)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Remove todos os vínculos atuais do projeto na tabela dbo.ProjetoBolsista
                string sqlDeletar = "DELETE FROM dbo.ProjetoBolsista WHERE ProjetoID = @ProjetoID";
                using (SqlCommand cmdDeletar = new SqlCommand(sqlDeletar, conn))
                {
                    cmdDeletar.Parameters.AddWithValue("@ProjetoID", projetoId);
                    cmdDeletar.ExecuteNonQuery();
                }

                // 2. Insere os bolsistas atualmente selecionados
                if (idsBolsistas != null && idsBolsistas.Count > 0)
                {
                    string sqlInserir = "INSERT INTO dbo.ProjetoBolsista (ProjetoID, BolsistaID, DataVinculo) VALUES (@ProjetoID, @BolsistaID, GETDATE())";

                    foreach (int bolsistaId in idsBolsistas)
                    {
                        using (SqlCommand cmdInserir = new SqlCommand(sqlInserir, conn))
                        {
                            cmdInserir.Parameters.AddWithValue("@ProjetoID", projetoId);
                            cmdInserir.Parameters.AddWithValue("@BolsistaID", bolsistaId);
                            cmdInserir.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        // Retorna bolsistas sem projeto vinculado (ou pertencentes ao projetoAtualId no caso de edição)
        public static List<Bolsista> ObterBolsistasDisponiveis(int? projetoAtualId = null)
        {
            List<Bolsista> lista = new List<Bolsista>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
            SELECT b.ID, b.Nome, b.Matricula, b.CPF, b.Sexo, b.DataNascimento 
            FROM dbo.Bolsista b
            WHERE b.ID NOT IN (
                SELECT pb.BolsistaID 
                FROM dbo.ProjetoBolsista pb 
                WHERE @ProjetoAtualID IS NULL OR pb.ProjetoID <> @ProjetoAtualID
            )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProjetoAtualID", (object)projetoAtualId ?? DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Bolsista
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                Nome = dr["Nome"].ToString(),
                                Matricula = Convert.ToInt64(dr["Matricula"]),
                                CPF = dr["CPF"].ToString(),
                                Sexo = dr["Sexo"] != DBNull.Value ? dr["Sexo"].ToString() : string.Empty
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
