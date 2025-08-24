using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sistema_de_rh_API.Contexts;
using sistema_de_rh_API.DTOs;
using sistema_de_rh_API.Models;

namespace sistema_de_rh_API.Repositories
{
    public class FuncionarioRepository
    {
        private readonly RhContext _context;

        public FuncionarioRepository(RhContext context)
        {
            _context = context;
        }

        public Funcionario CriarFuncionario(FuncionarioDTO funcionarioDTO)
        {
            var novoFuncionario = new Funcionario
            {
                Nome = funcionarioDTO.Nome,
                Endereco = funcionarioDTO.Endereco,
                Ramal = funcionarioDTO.Ramal,
                EmailProfissional = funcionarioDTO.EmailProfissional,
                Departamento = funcionarioDTO.Departamento,
                Salario = funcionarioDTO.Salario,
                DataAdmissao = funcionarioDTO.DataAdmissao
            };

            _context.Funcionarios.Add(novoFuncionario);
            var funcionarioFoiSalvo = _context.SaveChanges() > 0;

            if (!funcionarioFoiSalvo)
            {
                throw new Exception("Failed to save Funcionario.");
            }

            return novoFuncionario;
        }

        public List<Funcionario> ObterFuncionarios()
        {
            var funcionarios = _context.Funcionarios.AsNoTracking().ToList();
            return funcionarios;
        }

        public Funcionario ObterFuncionarioPorId(int id)
        {
            var funcionario = _context.Funcionarios.AsNoTracking().FirstOrDefault(f => f.Id == id);
            return funcionario;
        }

        public Funcionario UpdateFuncionario(FuncionarioDTO funcionarioDTO, int id)
        {
            var updateFuncionario = _context.Funcionarios.FirstOrDefault(f => f.Id == id);
            updateFuncionario.Nome = funcionarioDTO.Nome;
            updateFuncionario.Endereco = funcionarioDTO.Endereco;
            updateFuncionario.Ramal = funcionarioDTO.Ramal;
            updateFuncionario.EmailProfissional = funcionarioDTO.EmailProfissional;
            updateFuncionario.Departamento = funcionarioDTO.Departamento;
            updateFuncionario.Salario = funcionarioDTO.Salario;
            updateFuncionario.DataAdmissao = funcionarioDTO.DataAdmissao;

            _context.Funcionarios.Update(updateFuncionario);
            var funcionarioFoiSalvo = _context.SaveChanges() > 0;

            if (!funcionarioFoiSalvo)
            {
                throw new Exception("Failed to save Funcionario.");
            }

            return updateFuncionario;
        }
        public void DeleteFuncionario(int id)
        {
            var funcionarioDeleted = _context.Funcionarios.Find(id);

            _context.Funcionarios.Remove(funcionarioDeleted);
            _context.SaveChanges();

            var funcionarioFoiSalvo = _context.SaveChanges() > 0;

            if (!funcionarioFoiSalvo)
            {
                throw new Exception("Failed to save Funcionario.");
            }
        }

    }
}