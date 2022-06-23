using Sait2022.Exceptions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace Sait2022.Exceptions.Http
{
    /// <summary>
    /// Не найдено (код 404)
    /// </summary>
    public class NotFoundException : HttpException
    {
        /// <summary>
        /// Тип сущности
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>
        /// Ид сущности
        /// </summary>
        public object EntityId { get; private set; }

        /// <summary>
        /// Создание экземпляра класса <see cref="NotFoundException"/>
        /// </summary>
        /// <param name="entityType">Объект описания ошибки</param>
        /// <param name="entityId">Идентификатор сущности</param>
        public NotFoundException(Type entityType, object entityId)
            : base(StatusCodes.Status404NotFound, $"{entityType.Name}:{JsonConvert.SerializeObject(entityId)}")
        {
            EntityType = entityType;
            EntityId = entityId;
        }

        /// <summary>
        /// Создание экземпляра класса <see cref="NotFoundException"/>
        /// </summary>
        public NotFoundException(Type entityType)
            : base(StatusCodes.Status404NotFound, $"Absent {entityType.Name}")
        {
            EntityType = entityType;
        }

        /// <summary>
        /// Создание экземпляра класса <see cref="NotFoundException"/>
        /// </summary>
        public NotFoundException(string reasonPhrase)
            : base(StatusCodes.Status404NotFound, reasonPhrase)
        { }
    }
}
