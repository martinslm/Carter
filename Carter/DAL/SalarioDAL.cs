using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carter.DAL
{
    public class SalarioDAL
    {
        public int InserirSalarioEObterId(decimal ValorSalario)
        {
            string strsql = @" INSERT INTO historico_salarios 
                              (salario, data_cadastro)
                              VALUES(@ValorSalario, GETDATE());

                            SELECT IDENT_CURRENT('historico_salarios') AS id_salario";

            using (var busca = new SqlCommand(strsql, Conexao.Conectar()))
            {
                busca.Parameters.AddWithValue("@ValorSalario", ValorSalario);

                using (var reader = busca.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["id_salario"]);
                    }
                }
            }

            return 0;
        }
    }
}
