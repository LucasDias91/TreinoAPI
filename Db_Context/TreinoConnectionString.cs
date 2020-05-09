using ApresentacoesAPI.DTO.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreinoAPI.DTO.Treinos;
using TreinoAPI.DTO.Usuarios;

namespace TreinoAPI.Db_Context
{
    public class TreinoConnectionString: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=Lucas-PC;Initial Catalog=Treino;User ID=USR_Treino;Password=081991@RAtm");
        }

        public virtual DbSet<UsuariosDTO> Usuarios { get; set; }

        public virtual DbSet<SessoesDTO> Sessoes { get; set; }

        public virtual DbSet<SemanasDTO> Semanas { get; set; }

        public virtual DbSet<DivisoesDTO> Divisoes { get; set; }

        public virtual DbSet<ExerciciosDTO> Exercicios { get; set; }

        public virtual DbSet<SemanaUsuariosDTO> SemanaUsuarios { get; set; }

        public virtual DbSet<TreinosDTO> Treinos { get; set; }

        public virtual DbSet<GruposDTO> Grupos { get; set; }

        public virtual DbSet<IntervalosDTO> Intervalos { get; set; }

        public virtual DbSet<SxRsDTO> SxRs { get; set; }

        public virtual DbSet<TiposDTO> Tipos { get; set; }

        public virtual DbSet<TreinoUsuariosDTO> TreinoUsuarios { get; set; }
    }
}
