using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Model.Enums;

namespace Model.Entities
{
    /// <summary>
    /// Represents API tokens used to access the web api.
    /// </summary>
    [Table("ApiToken")]
    [Index(nameof(Token), IsUnique = true)]
    public class ApiToken : Entity
    {
        /// <summary>
        /// User for whom the token is created.
        /// </summary>
        [ForeignKey("User")]
        public long UserId { get; set; }

        /// <summary>
        /// User of ApiToken.
        /// </summary>
        public virtual Individual User { get; set; }

        /// <summary>
        /// The token.
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string Token { get; set; }

        /// <summary>
        /// The optional related HTTP cookie.
        /// </summary>
        [MaxLength(36)]
        public string Cookie { get; set; }

        /// <summary>
        /// Humanreadable token type.
        /// </summary>
        [Required]
        public string TokenTypeAsString
        {
            get => TokenType.ToString();
            set => TokenType = (TokenType)Enum.Parse(typeof(TokenType), value, true);
        }

        /// <summary>
        /// Type of token, see: <see cref="TokenType"/>.
        /// </summary>
        public TokenType TokenType { get; set; }

        /// <summary>
        /// Expiration date and time of the token.
        /// </summary>
        [Required]
        public DateTime Expiration { get; set; }
    }
}
