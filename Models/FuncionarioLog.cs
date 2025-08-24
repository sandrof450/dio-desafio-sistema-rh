
using Azure.Data.Tables;
using Azure;
using sistema_de_rh_API.Enums;
using sistema_de_rh_API.DTOs;
using System.Text.Json;

namespace sistema_de_rh_API.Models
{
    public class FuncionarioLog : Funcionario, ITableEntity
    {
        public FuncionarioLog() { }

        public FuncionarioLog(Funcionario funcionario, TipoAcaoEnum tipoAcao)
        {
            base.Id = funcionario.Id;
            base.DataAdmissao = DateTime.SpecifyKind(funcionario.DataAdmissao, DateTimeKind.Utc);
            base.Departamento = funcionario.Departamento;
            base.EmailProfissional = funcionario.EmailProfissional;
            base.Endereco = funcionario.Endereco;
            base.Nome = funcionario.Nome;
            base.Ramal = funcionario.Ramal;
            base.Salario = funcionario.Salario;

            TipoAcao = tipoAcao;
            JsonNew = JsonSerializer.Serialize(funcionario);
            PartitionKey = funcionario.Departamento;
            RowKey = Guid.NewGuid().ToString();
        }


        public TipoAcaoEnum TipoAcao { get; set; }
        public string? JsonOld { get; set; }
        public string? JsonNew { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}