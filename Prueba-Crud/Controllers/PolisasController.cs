using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Prueba_Crud.Models;

namespace Prueba_Crud.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PolisasController : ControllerBase
    {
        private readonly dbContext _context;

        public PolisasController(dbContext context)
        {
            _context = context;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Login()
        {

            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fggfasdfaVSDFNFGNDFVASDJKFHEUIFUIgfuiasdgfasgfasdui"));

            var claims = new[]
                {

                    new Claim(JwtRegisteredClaimNames.Sub, "1"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("roles", "admin"),
                    new Claim("nombre", "Jose"),
                    new Claim("usuario", "user")

                };
            var expiration = DateTime.Now.AddMonths(2);

            var token = new JwtSecurityToken(
                    issuer: "https://github.com/jcaroyanez",
                    audience: "https://github.com/jcaroyanez",
                    claims: claims,
                    signingCredentials: new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256),
                    expires: expiration
                    );


            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [Authorize]
        // GET: api/Polisas
        [HttpGet]
        public IEnumerable<Polisas> GetPolisas()
        {
            return _context.Polisas;
        }

        [Authorize]
        // GET: api/Polisas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolisas([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var polisas = await _context.Polisas.FindAsync(id);

            if (polisas == null)
            {
                return NotFound();
            }

            return Ok(polisas);
        }

        [Authorize]
        // PUT: api/Polisas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolisas([FromRoute] int id, [FromBody] Polisas polisas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != polisas.id)
            {
                return BadRequest();
            }

            _context.Entry(polisas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolisasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize]
        // POST: api/Polisas
        [HttpPost]
        public async Task<IActionResult> PostPolisas([FromBody] Polisas polisas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Polisas.Add(polisas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        // DELETE: api/Polisas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolisas([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var polisas = await _context.Polisas.FindAsync(id);
            if (polisas == null)
            {
                return NotFound();
            }

            _context.Polisas.Remove(polisas);
            await _context.SaveChangesAsync();

            return Ok(polisas);
        }

        private bool PolisasExists(int id)
        {
            return _context.Polisas.Any(e => e.id == id);
        }
    }
}