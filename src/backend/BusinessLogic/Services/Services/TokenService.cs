using Repository.Interfaces;
using Services.Interfaces;
using System;
using Domain.Core;
using Enums.Core;

namespace Services.Services
{
    public class TokenService: ITokenService
    {
        #region Fields

        private readonly ITokenRepository _tokenRepository;
        #endregion
        public TokenService( ITokenRepository tokenRepository)
        {
            this._tokenRepository = tokenRepository;

            if (this._tokenRepository == null)
                throw new ArgumentNullException("TokenService.ITokenRepository");
        }

        public void Deactivate(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("Deactivate.id");

            this._tokenRepository.DeactivateToken((byte)StatusType.Consumed, DateTime.UtcNow, id);

        }

        public string Add(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("Add.userId");

            var item = new Token
            {
                Id = Guid.NewGuid().ToString("N"),
                UserId = userId,
                TokenType = (byte)TokenType.ForgotPassword,
                Status = (byte)StatusType.Active,
                CreatedOn = DateTime.UtcNow
            };
            this._tokenRepository.DeleteAndAdd(x => x.UserId == userId && x.TokenType == (byte)TokenType.ForgotPassword, item);
            return item.Id;
        }

        public Tuple<bool, string> IsValid(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("IsValid.token");

            var expireDate = DateTime.UtcNow.AddHours(-24);
            var tokenObj = this._tokenRepository.GetSingle(x => x.Id == token && x.Status == (byte)StatusType.Active && x.CreatedOn >= expireDate);
            
                return new Tuple<bool, string>(tokenObj != null?true:false, tokenObj?.UserId);
 
        }
    }
}
