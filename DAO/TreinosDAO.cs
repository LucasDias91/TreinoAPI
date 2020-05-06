using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TreinoAPI.Db_Context;
using TreinoAPI.DTO.Treinos;

namespace TreinoAPI.DAO
{
    public class TreinosDAO
    {
        TreinoConnectionString DbTreino = new TreinoConnectionString();

        public void PopulaTreinoDias(int IDUsuario)
        {
            DbTreino.Database.ExecuteSqlCommand("Insert Into TreinoDias (IDUsuario,IDCiclo, IDDivisao, Executado, Ativo)" +
                                                " Select Distinct @IDUsuario, Ciclo.IDCiclo, Divisao.IDDivisao, null, 0 from Treino$ as Treino" +
                                                " inner Join Ciclos as Ciclo on Ciclo.IDCiclo = Treino.Ciclo" +
                                                " inner join Divisoes as Divisao on Treino.Divisao = Divisao.Divisao",
                                                 new SqlParameter("@IDUsuario", IDUsuario));
        }

        public CiclosDTO InsertCiclo(CiclosDTO CicloAdd)
        {
            DbTreino.Add(CicloAdd);
            DbTreino.SaveChanges();
            return CicloAdd;
        }

        public List<CiclosDTO> SelectCiclos()
        {
            return DbTreino.Ciclos.Where((ciclo) => ciclo.Ativo == true)
                                  .ToList();
        }

        public CiclosDTO UpdateCiclo(CiclosDTO CicloEdit)
        {
            DbTreino.Update(CicloEdit);
            DbTreino.SaveChanges();
            return CicloEdit;
        }
    }
}
