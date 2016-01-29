using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyDiary.Models.DTOs;
using BabyDiary.Models.Entities;

namespace BabyDiary.Helpers
{
    public class EntityMapper
    {
        public static ChildDto ToChildDto(Child child, ChildDto childDto = null)
        {
            if (childDto == null)
            {
                childDto = new ChildDto();
            }
            childDto.ChildId = child.ChildId;
            childDto.FirstName = child.FirstName;
            childDto.LastName = child.LastName;
            childDto.Surname = child.Surname;
            childDto.BirthDate = child.BirthDate;
            childDto.BirthTime = child.BirthTime;
            childDto.BirthPlace = child.BirthPlace;
            childDto.Sex = child.Sex;

            return childDto;
        }
    }
}