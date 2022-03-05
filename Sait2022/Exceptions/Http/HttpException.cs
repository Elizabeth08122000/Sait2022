using Sait2022.Exceptions.Http;
using System;

namespace Sait2022.Exceptions.Http
{
    /// <summary>
    /// Ошибка, соответствующая ошибке протокола HTTP
    /// </summary>
    public abstract class HttpException : Exception
    {
        /// <summary>
        /// Создание экземпляра класса <see cref="HttpException"/>
        /// </summary>
        /// <param name="statusCode">Код ошибки</param>
        /// <param name="errorObject">Объект описания ошибки</param>
        protected HttpException(int statusCode, object errorObject)
        {
            StatusCode = statusCode;
            ErrorObject = errorObject;
        }

        /// <summary>
        /// Создание экземпляра класса <see cref="HttpException"/>
        /// </summary>
        /// <param name="statusCode">Код ошибки</param>
        /// <param name="reasonPhrase">Причина ошибки</param>
        protected HttpException(int statusCode, string reasonPhrase)
            : base(reasonPhrase)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Код ошибки, см. <see cref="Microsoft.AspNetCore.Http.StatusCodes"/>
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Объект описания ошибки
        /// </summary>
        public object ErrorObject { get; }
    }
}
