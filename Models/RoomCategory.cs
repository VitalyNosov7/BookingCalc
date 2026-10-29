using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCalc.Models
{
    /// <summary>Класс содержит информацию о категории номера санатория</summary>
    public class RoomCategory
    {

        /// <summary>Конструктор категории номера без параметров</summary>		
        public RoomCategory() { }

        /// <summary>Идентификатор категории номера санатория</summary>
        private Int32 _roomCategoryID;
        /// <summary>Идентификатор категории номера санатория</summary>
        public Int32 RoomCategoryID
        {
            get { return _roomCategoryID; }
            set { _roomCategoryID = value; }
        }

        /// <summary>Идентификатор санатория</summary>
        private Int32 _sanatoriumID;
        /// <summary>Идентификатор санатория</summary>
        public Int32 SanatoriumID
        {
            get { return _sanatoriumID; }
            set { _sanatoriumID = value; }
        }

        /// <summary>Текущий санаторий</summary>
        private Sanatorium _currentSanatorium = null!;
        /// <summary>Текущий санаторий</summary>
        public Sanatorium CurrentSanatorium
        {
            get { return _currentSanatorium; }
            set { _currentSanatorium = value; }
        }


        /// <summary>Наименование категории номера санатория</summary>
        private String _roomCategoryName = String.Empty;
        /// <summary>Наименование категории номера санатория</summary>
        public String RoomCategoryName
        {
            get { return _roomCategoryName; }
            set { _roomCategoryName = value; }
        }
    }
}
