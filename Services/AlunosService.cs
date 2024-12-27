using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlunosApi.Context;
using AlunosApi.Models;
using InterfaceAluno.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Linq;
namespace AlunosApi.Services
{
    public class AlunosService : IAlunoService
    {

        private readonly AppDbContext _context;

        public AlunosService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
               return await _context.Alunos.ToListAsync();
            }
            catch (Exception e)
            {
                Console.Write($"Error:{e.Message}");
                return Enumerable.Empty<Aluno>();
            }
            
        }




        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            IEnumerable<Aluno> alunos;

            if (!string.IsNullOrWhiteSpace(nome))
            {
                alunos = await _context.Alunos.Where( n => n.Nome.Contains(nome)).ToListAsync(); 
            }        
            else
            {
                alunos = await GetAlunos();
            }  
            return alunos; 
        }

        public async Task<Aluno> GetAluno(int id)
        {

            var aluno = await _context.Alunos.FindAsync(id);
            return aluno;  //Sem tratamente de erros, o que pode ser um problema ao testar o código. 
           

        }

        public async Task CreateAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

        }


         public async Task UpdateAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

        }
       
    }
}
