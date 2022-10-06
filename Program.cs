//	dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 6.0 
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
		
		public DbSet<ControleEstoque> ControleEstoque { get; set; } = null!;
		
	}
	
	class Program
	{
		static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("Produtos") ?? "Data Source=Produtos.db";
			builder.Services.AddSqlite<BaseProdutos>(connectionString);

			var app = builder.Build();


			// ControleEstoque controleEstoque = new ControleEstoque();
			// var qdt = 0;

			// // Mostra toda a base do Estoque atual
			// app.MapGet("/zxcv", (BaseProdutos baseProdutos) => {
			// 	return baseProdutos.ControleEstoque.ToList();
			// });

			// app.MapGet("/mostraTodoEstoque/{quantidadeTotal}", (BaseProdutos baseProdutos, int quantidadeTotal) => {
			// 	return baseProdutos.Produtos.Find(quantidadeTotal);
			// });



			// --------------------------------- PRODUTO --------------------------------- //
			//--- Cadastrar produto --- 
			app.MapPost("/cadastrar", (BaseProdutos baseProdutos, Produto produto) =>
			{
				// TODO: Deixar add somente das categorias "Tv", "Monitor", "Celular" 
				// TODO: Deixar add somente das modelos "Tv", "Monitor", "Celular" 

				baseProdutos.Produtos.Add(produto);
				baseProdutos.SaveChanges();
		

				// var atualizaEstoque = baseProdutos.Produtos.Find();
				// atualizaEstoque.quantidade = produtoAtualizado.quantidade;


				return "Produto adicionado";

			});

			// --- Atualizar Cadastro por ID ---
			app.MapPost("/atualizarCadastroPorId/{id}", (BaseProdutos baseProdutos, Produto produtoAtualizado, int id) =>
			{
				var cadastro = baseProdutos.Produtos.Find(id);
				cadastro.categoria = produtoAtualizado.categoria;
				cadastro.modelo = produtoAtualizado.modelo;
				cadastro.quantidade = produtoAtualizado.quantidade;
				//TODO data do pedido - está dando erro ao utilizar a api
				//cadastro.data = produtoAtualizado.data;

				// var qdt = produtoAtualizado.quantidade;
				// controle.estoqueAtual(qdt);

				baseProdutos.SaveChanges();
				return "Cadastro atualizado";
			});


			// --- Mostrar todos os Cadastros realizados ---
			app.MapGet("/mostrarCadastros", (BaseProdutos baseProdutos) => {
				return baseProdutos.Produtos.ToList();
			});


			// --- Mostar um Cadastro em especifico listando pelo ID
			app.MapGet("/mostrarCadastroPorId/{id}", (BaseProdutos baseProdutos, int id) => {
				return baseProdutos.Produtos.Find(id);
			});

			// --- Mostra todo o estoque por categoria
			app.MapGet("/mostrarEstoquePorCategoria/{categoriaProcurada}", 
			(BaseProdutos baseProdutos, string categoriaProcurada) => baseProdutos.Produtos.Where(elemento => elemento.categoria.Contains(categoriaProcurada)));

			// --- Mostra todo o estoque por modelo
			app.MapGet("/mostrarEstoquePorModelo/{modeloProcurado}", 
			(BaseProdutos baseProdutos, string modeloProcurado) => baseProdutos.Produtos.Where(elemento => elemento.modelo.Contains(modeloProcurado)));

			// --- Deletar cadastro por ID --- 
			app.MapPost("/deletarCadastroPorId/{id}", (BaseProdutos baseProdutos, int id) =>
			{
				var produto = baseProdutos.Produtos.Find(id);
				baseProdutos.Remove(produto);
				baseProdutos.SaveChanges();
				return "produto atualizado";
				// TODO: Criar outro deletar porém equivalente a enviar a outra filial
			});

			var qdt = 0;




			// --------------------------------- Estoque Atualizado --------------------------------- //
			// *** Código abaixo não está funcionando 
			
				app.MapGet("/mostrarEstoqueAtualizado/{categoria}", (BaseProdutos baseProdutos, string categoriaInformada) => {
				// TODO: Percorrer a lista Produtos e somar a quantidade de acordo com a Categoria ou Produto
				var categoriaLocalizada = baseProdutos.Produtos.Find(categoriaInformada);
				foreach (var item in baseProdutos.Produtos)
				{
					if (String.Equals(categoriaLocalizada, categoriaInformada))  
    				Console.WriteLine(" have same value.");  
					
				}
				return baseProdutos.ControleEstoque.ToList();
			});
			
			// --- Mostar um Cadastro em especifico listando pelo ID
			app.MapGet("/xmostrarEstoqueAtualizado/{id}", (BaseProdutos baseProdutos, int id) => {
				return baseProdutos.Produtos.Find(id);
			});

			//app.MapGet("/mostarSoma/{categoria}", async (BaseProdutos baseProdutos, string categoria) => await baseProdutos.Produtos.Where(s => s.categoria == categoria).FirstOrDefault()).RequireAuthorization();

			// --------------------------------- CONTROLE --------------------------------- //

			app.Run();
		}
	}
}
