using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carter.DAL
{
    public class PoupancaDAL
    {
        public int CadastrarEObterIdPoupanca(decimal valorObjetivo, DateTime dataLimite, int idUsuario)
        {
            string strsql = @"INSERT INTO poupanca (
	                                    valor_objetivo
	                                    ,data_cadastro
	                                    ,data_limite_objetivo
	                                    ,id_usuario
	                                    )
                                    VALUES (
	                                    @valorObjetivo
	                                    ,GETDATE()
	                                    ,@dataLimite
	                                    ,@idUsuario
	                                    )
                                
                                SELECT IDENT_CURRENT('poupanca') AS id_poupanca";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@valorObjetivo", valorObjetivo);
                busca.Parameters.AddWithValue("@dataLimite", dataLimite);
                busca.Parameters.AddWithValue("@idUsuario", idUsuario);

                using (var reader = busca.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["id_poupanca"]);
                    }
                }
            }

            return 0;

        }
    }
}
