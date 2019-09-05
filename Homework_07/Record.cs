using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    struct Record
    {
        #region Свойства

        /// <summary>
        /// Номер записи
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Заоголовок
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата и время создания записи
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// Дата и время последнего изменения записи
        /// </summary>
        public DateTime LastModifyDateTime { get; set; }
        #endregion

        #region Конструкторы

        public Record(int number, string caption, string description, DateTime createDateTime, DateTime lastModifyDateTime)
        {
            Number = number;
            Caption = caption;
            Description = description;
            CreateDateTime = createDateTime;
            LastModifyDateTime = lastModifyDateTime;
        }

        #endregion

        #region Методы

        #endregion
    }
}
