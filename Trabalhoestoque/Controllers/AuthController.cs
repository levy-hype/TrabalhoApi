// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalhoestoque.Data;
using Trabalhoestoque.DTOs;
using Trabalhoestoque.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly EstoqueContext _context;

    public AuthController(EstoqueContext context)
    {
        _context = context;
    }

    // Endpoint para registrar um novo usuário (necessário para criar o primeiro usuário)
    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar(LoginRequestDto request)
    {
        // Verifica se o usuário já existe
        if (await _context.Usuarios.AnyAsync(u => u.NomeUsuario == request.NomeUsuario))
        {
            return BadRequest("Nome de usuário já existe.");
        }

        // Gera o HASH da senha
        string senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

        var usuario = new Usuario
        {
            NomeUsuario = request.NomeUsuario,
            SenhaHash = senhaHash
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Usuário registrado com sucesso!" });
    }


    // Endpoint para realizar o login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        // 1. Encontra o usuário pelo nome
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NomeUsuario == request.NomeUsuario);

        // 2. Se o usuário não existe ou a senha está incorreta, retorna erro.
        //    A verificação do BCrypt já lida com o caso do usuário ser nulo implicitamente,
        //    mas é bom ter uma mensagem genérica para não informar qual campo está errado.
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
        {
            return Unauthorized(new { Message = "Usuário ou senha inválidos." });
        }

        // 3. Se chegou aqui, o login foi bem-sucedido
        return Ok(new { Message = "Login bem-sucedido!" });
    }
}
