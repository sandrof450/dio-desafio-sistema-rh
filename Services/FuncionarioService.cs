using System.Text.Json;
using sistema_de_rh_API.Contexts;
using sistema_de_rh_API.DTOs;
using sistema_de_rh_API.Models;
using sistema_de_rh_API.Repositories;

namespace sistema_de_rh_API.Services
{
    public class FuncionarioService
    {
        private readonly RhContext _context;
        private readonly FuncionarioRepository _funcionarioRepository;
        private readonly FuncionarioLogService _funcionarioLogService;

        public FuncionarioService(RhContext context, FuncionarioRepository funcionarioRepository, FuncionarioLogService funcionarioLogService)
        {
            _context = context;
            _funcionarioRepository = funcionarioRepository;
            _funcionarioLogService = funcionarioLogService;
        }

        public async Task<FuncionarioDTO> CriarFuncionario(FuncionarioDTO funcionarioDTO)
        {
            #region Validate funcionario is not null
            if (funcionarioDTO == null)
            {
                throw new ArgumentNullException(nameof(funcionarioDTO), "Funcionario cannot be null");
            }
            #endregion

            #region Check if funcionario already exists
            var funcionarios = _funcionarioRepository.ObterFuncionarios();
            var existeFuncionarioComMesmoEmail = funcionarios.Find(f => f.EmailProfissional == funcionarioDTO.EmailProfissional) != null;
            if (existeFuncionarioComMesmoEmail)
            {
                throw new InvalidOperationException("Funcionario with this email already exists.");
            }
            #endregion

            // Add the new funcionario to the context
            #region Add the new funcionario to the context
            var novoFuncionario = _funcionarioRepository.CriarFuncionario(funcionarioDTO);
            #endregion

            // Lógica para adicionar o registro na tabela de logs usando Azure Table Storage
            // Supondo que você tenha uma classe LogService e um modelo LogEntry
            #region Add a log to new funcionario
            var log = new FuncionarioLog(novoFuncionario, Enums.TipoAcaoEnum.Inclusao);
            await _funcionarioLogService.RegistrarLogAsync(log);
            #endregion

            return funcionarioDTO;
        }

        public List<Funcionario> ObterFuncionarios()
        {
            #region Get the funcionarios to the context
            var funcionarios = _funcionarioRepository.ObterFuncionarios();
            #endregion

            #region Validate funcionario is not null
            if (funcionarios == null)
            {
                throw new ArgumentNullException(nameof(funcionarios), "Funcionario cannot be null");
            }
            #endregion

            return funcionarios;
        }

        public Funcionario ObterFuncionarioPorId(int id)
        {
            #region Get the funcionario as ID to the context
            var funcionario = _funcionarioRepository.ObterFuncionarioPorId(id);
            #endregion

            #region Validate funcionario is not null
            if (funcionario == null)
            {
                throw new Exception($"Funcionario with id {id} not found.");
            }
            #endregion


            return funcionario;

        }

        public async Task<FuncionarioDTO> UpdateFuncionario(FuncionarioDTO funcionarioDTO, int id)
        {
            #region Instaces and variables Initials
            var funcionarioOld = _funcionarioRepository.ObterFuncionarioPorId(id);
            #endregion

            #region Validate funcionario is not null or not exists
            var funcionarios = _funcionarioRepository.ObterFuncionarios();
            var existsFuncionario = funcionarios.Find(f => f.Id == id) != null;
            
            if (!existsFuncionario)
            {
                throw new Exception("Funcionario not found");
            }

            if (funcionarioDTO == null)
            {
                throw new ArgumentNullException(nameof(funcionarioDTO), "Funcionario cannot be null");
            }
            #endregion

            #region Update the funcionario to the context
            var updateFuncionario = _funcionarioRepository.UpdateFuncionario(funcionarioDTO, id);
            #endregion

            #region update funcionarioDTO
            funcionarioDTO.Nome = updateFuncionario.Nome;
            funcionarioDTO.Salario = updateFuncionario.Salario;
            funcionarioDTO.Ramal = updateFuncionario.Ramal;
            funcionarioDTO.Departamento = updateFuncionario.Departamento;
            funcionarioDTO.Endereco = updateFuncionario.Endereco;
            funcionarioDTO.DataAdmissao = updateFuncionario.DataAdmissao;
            funcionarioDTO.EmailProfissional = updateFuncionario.EmailProfissional;
            #endregion


            #region Add a log to new funcionario
            var funcionarioLog = new FuncionarioLog(updateFuncionario, Enums.TipoAcaoEnum.Atualizacao);
            funcionarioLog.JsonOld = JsonSerializer.Serialize(funcionarioOld);
            await _funcionarioLogService.RegistrarLogAsync(funcionarioLog);
            #endregion

            return funcionarioDTO;
        }

        public async Task DeleteFuncionario(int id)
        {
            #region Validate funcionario is not exist
            var funcionarios = _funcionarioRepository.ObterFuncionarios();
            var existsFuncionario = funcionarios.Find(f => f.Id == id) != null;
            
            if (!existsFuncionario)
            {
                throw new Exception("Funcionario not found");
            }
            #endregion

            #region Get old funcionario 
            var funcionarioOld = _funcionarioRepository.ObterFuncionarioPorId(id);
            #endregion

            #region Remove the funcionario with the matching id provided in the parameter
            _funcionarioRepository.DeleteFuncionario(id);
            #endregion            

            #region Add a log to new funcionario
            var log = new FuncionarioLog(funcionarioOld, Enums.TipoAcaoEnum.Remocao);
            log.JsonNew = null;
            log.JsonOld = JsonSerializer.Serialize(funcionarioOld);
            await _funcionarioLogService.RegistrarLogAsync(log);
            #endregion
        }

    }
}