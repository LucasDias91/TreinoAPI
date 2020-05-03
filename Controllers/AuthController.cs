using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using ApresentacoesAPI.DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TreinoAPI.DAO;
using TreinoAPI.DTO.Auth;
using TreinoAPI.DTO.Usuarios;

namespace TreinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult postLogin([FromBody]LoginDTO login,
                                       [FromServices]UsuariosDAO usuariosDAO,
                                       [FromServices]SigningConfigurationsDTO signingConfigurations,
                                       [FromServices]TokenConfigurationsDTO tokenConfigurations,
                                       [FromServices]IDistributedCache cache)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool credenciaisValidas = false;
            var _IDUsuario = string.Empty;

            AccessDataDTO _accessData = new AccessDataDTO();

            try
            {
                if (login.Grant_Type == "password")
                {
                    var _usuario = usuariosDAO.SelectUsuarioPorCredenciais(login.UserName, login.Password);

                    credenciaisValidas = (_usuario != null);

                    if (credenciaisValidas)
                    {
                        if (_usuario.Ativo == false)
                        {
                            return BadRequest("Usuário bloqueado! Entre em contato com o administrador.");
                        }

                        _IDUsuario = _usuario.IDUsuario.ToString();

                    }
                }
                else if (login.Grant_Type == "refresh_token")
                {
                    RefreshTokenDataDTO refreshTokenBase = null;
                    string strTokenArmazenado = cache.GetString(login.RefreshToken);
                    if (!String.IsNullOrWhiteSpace(strTokenArmazenado))
                    {
                        refreshTokenBase = JsonConvert.DeserializeObject<RefreshTokenDataDTO>(strTokenArmazenado);
                    }

                    credenciaisValidas = (refreshTokenBase != null && login.RefreshToken == refreshTokenBase.RefreshToken);
                    // Elimina o token de refresh já que um novo será gerado
                    if (credenciaisValidas)
                    {
                        _IDUsuario = refreshTokenBase.IDUsuario;
                        cache.Remove(login.RefreshToken);
                    }
                }
                else
                {
                    return BadRequest("Invalid grant_type");
                }

                if (!credenciaisValidas)
                {
                    return BadRequest("Usuário ou senha inválidos. Caso não se lembre, entre em contato com o administrador!");
                }

                _accessData = GenerateToken(_IDUsuario, signingConfigurations, tokenConfigurations, cache);

                SaveSessao(_IDUsuario, login, _accessData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok(_accessData);
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public IActionResult PostRegister([FromBody] RegisterDTO Register,
                                       [FromServices]UsuariosDAO usuariosDAO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UsuariosDTO _usuario = usuariosDAO.SelectUsuarioPorEmail(Register.Email);
                if (_usuario != null)
                {
                    return BadRequest(new { msg = "Email já cadastrado" });
                }

                usuariosDAO.InsertUsuario(Register);

            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.InnerException));
            }

            return Ok(new {msg = "Registro efetuado com sucesso!" });
        }

        private void SaveSessao(string idUsuario, LoginDTO Login, AccessDataDTO AccessData)
        {
            SessoesDTO _Sessao = new SessoesDTO();
            _Sessao.IDUsuario = Convert.ToInt16(idUsuario);
            _Sessao.GrantType = Login.Grant_Type;
            _Sessao.Token = AccessData.AccessToken;
            _Sessao.RefreshToken = AccessData.RefreshToken;
            _Sessao.datainicio = Convert.ToDateTime(AccessData.Created);
            _Sessao.datafim = Convert.ToDateTime(AccessData.Expiration);
            new SessoesDAO().InsertSessao(_Sessao);
        }

        private AccessDataDTO GenerateToken(string userID, SigningConfigurationsDTO signingConfigurations, TokenConfigurationsDTO tokenConfigurations, IDistributedCache cache)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                 new GenericIdentity(userID, "IDUsuario"),
                   new[] {
                        new Claim("IDUsuario",  userID)
                 }
             );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            // Calcula o tempo máximo de validade do refresh token
            // (o mesmo será invalidado automaticamente pelo Redis)
            TimeSpan finalExpiration = TimeSpan.FromSeconds(tokenConfigurations.FinalExpiration);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            AccessDataDTO resultado = new AccessDataDTO();
            resultado.AccessToken = token;
            resultado.RefreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty);
            resultado.Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss");
            resultado.Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss");

            // Armazena o refresh token em cache através do Redis 
            var refreshTokenData = new RefreshTokenDataDTO();
            refreshTokenData.RefreshToken = resultado.RefreshToken;
            refreshTokenData.IDUsuario = userID;

            DistributedCacheEntryOptions opcoesCache = new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(finalExpiration);
            cache.SetString(resultado.RefreshToken, JsonConvert.SerializeObject(refreshTokenData), opcoesCache);
            return resultado;
        }

    

    }
}