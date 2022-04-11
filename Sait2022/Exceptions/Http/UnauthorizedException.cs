using Sait2022.Exceptions.Http;
using Microsoft.AspNetCore.Http;

namespace Sait2022.Exceptions.Http
{
    /// <summary>
    /// Ошибка авторизации (код 401)
    /// </summary>
    public class UnauthorizedException : HttpException
    {
        /// <summary>
        /// Создание экземпляра класса <see cref="UnauthorizedException"/>
        /// </summary>
        /// <param name="errorObject">Объект описания ошибки</param>
        public UnauthorizedException(object errorObject)
            : base(StatusCodes.Status401Unauthorized, errorObject)
        { }

        /// <summary>
        /// Создание экземпляра класса <see cref="UnauthorizedException"/>
        /// </summary>
        public UnauthorizedException()
            : this(null)
        { }
    }
}
