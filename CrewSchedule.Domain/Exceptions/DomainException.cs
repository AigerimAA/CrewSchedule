using System;
using System.Collections.Generic;
using System.Text;

namespace CrewSchedule.Domain.Exceptions
{
    /// <summary>
    /// Исключение для нарушений бизнес-правил внутри доменной модели
    /// Бросается агрегатами когда входные данные или состояние нарушают инварианты домена
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
