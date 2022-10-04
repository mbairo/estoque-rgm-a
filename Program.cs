//	dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 6.0 && dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0
//	dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0
//	dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0
	
//	dotnet ef migrations add InitialCreate
// Sempre que alterar algo, atualizar com o update
//	dotnet ef database update

using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EstoqueRgm
{
	class BaseProdutos : DbContext
	{
		public BaseProdutos(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Produto> Produtos { get; set; } = null!;
	}

	class Program
	{
		static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("Produtos") ?? "Data Source=Produtos.db";
			builder.Services.AddSqlite<BaseProdutos>(connectionString);

			var app = builder.Build();


			// --------------------------------- PRODUTO --------------------------------- //
			//--- Cadastrar produto --- 
			app.MapPost("/cadastrar", (BaseProdutos baseProdutos, Produto produto) =>
			{
				baseProdutos.Produtos.Add(produto);
				baseProdutos.SaveChanges();
				return "Produto adicionado";
			});

			// --- Atualizar Cadastro por ID ---
			app.MapPost("/atualizar/{id}", (BaseProdutos baseProdutos, Produto produtoAtualizado, int id) =>
			{
				var cadastro = baseProdutos.Produtos.Find(id);
				cadastro.categoria = produtoAtualizado.categoria;
				cadastro.modelo = produtoAtualizado.modelo;
				cadastro.quantidade = produtoAtualizado.quantidade;
				//TODO data do pedido - está dando erro ao utilizar a api
				//cadastro.data = produtoAtualizado.data;
				baseProdutos.SaveChanges();
				return "Cadastro atualizado";
			});


			// --- Mostrar todos os Produtos/Cadastros ---
			app.MapGet("/produtos", (BaseProdutos baseProdutos) => {
				return baseProdutos.Produtos.ToList();
			});


			// --- Mostar um Cadastro em especifico listando pelo ID
			app.MapGet("/produtos/{id}", (BaseProdutos baseProdutos, int id) => {
				return baseProdutos.Produtos.Find(id);
			});

			// TODO mostar todos por nome 
			// TODO mostar todos por categoria


			// --- Deletar cadastro por ID --- 
			app.MapPost("/deletar/{id}", (BaseProdutos baseProdutos, int id) =>
			{
				var produto = baseProdutos.Produtos.Find(id);
				baseProdutos.Remove(produto);
				baseProdutos.SaveChanges();
				return "produto atualizado";
			});

			// --------------------------------- CONTROLE --------------------------------- //








			app.Run();
		}
	}
}
