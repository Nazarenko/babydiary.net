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
        public static ChildDto ChildToDto(Child child, ChildDto childDto = null)
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

        public static Child DtoToChild(ChildDto childDto, Child child = null)
        {
            if (child == null)
            {
                child = new Child();
            }
            child.ChildId = childDto.ChildId;
            child.FirstName = childDto.FirstName;
            child.LastName = childDto.LastName;
            child.Surname = childDto.Surname;
            child.BirthDate = childDto.BirthDate;
            child.BirthTime = childDto.BirthTime;
            child.BirthPlace = childDto.BirthPlace;
            child.Sex = childDto.Sex;

            return child;
        }
    }
}