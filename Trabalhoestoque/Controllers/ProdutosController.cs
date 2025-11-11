// Controllers/ProdutosController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalhoestoque.Data;
using Trabalhoestoque.Models;

[ApiController]
[Route("api/[controller]")] // Rota base será: api/produtos
public class ProdutosController : ControllerBase
{
    private readonly EstoqueContext _context;

    public ProdutosController(EstoqueContext context)
    {
        _context = context;
    }

    // GET: api/produtos (Listar todos os produtos)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos([FromQuery] string? nome)
    {
        // Inicia a consulta base, sem executar ainda
        var query = _context.Produtos.AsQueryable();

        // Se um nome foi passado na URL (ex: /api/produtos?nome=Coca)
        if (!string.IsNullOrEmpty(nome))
        {
            // Adiciona um filtro na consulta
            query = query.Where(p => p.Nome.Contains(nome));
        }

        // Executa a consulta no banco e retorna o resultado
        return await query.ToListAsync();
    }

    // GET: api/produtos/5 (Buscar um produto por ID)
    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null)
        {
            return NotFound(); // Retorna 404 se não encontrar
        }

        return produto;
    }

    // POST: api/produtos (Cadastrar um novo produto)
    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        // Retorna 201 Created com o produto criado e um link para ele
        return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
    }

    // PUT: api/produtos/5 (Atualizar/Editar um produto existente)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduto(int id, Produto produto)
    {
        if (id != produto.Id)
        {
            return BadRequest(); // Retorna 400 se os IDs forem diferentes
        }

        _context.Entry(produto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Produtos.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent(); // Retorna 204 indica sucesso sem conteúdo de retorno
    }

    // DELETE: api/produtos/5 (Deletar um produto)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
