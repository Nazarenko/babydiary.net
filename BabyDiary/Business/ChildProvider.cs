using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BabyDiary.Business.Interfaces;
using BabyDiary.DAL.Interfaces;
using BabyDiary.Helpers;
using BabyDiary.Models.DTOs;
using BabyDiary.Models.Entities;

namespace BabyDiary.Business
{
    public class ChildProvider : IChildProvider
    {
        private readonly IChildRepository _childRepository;
        private readonly ICurrentUser _currentUser;

        public ChildProvider(IChildRepository childRepository, ICurrentUser currentUser)
        {
            _childRepository = childRepository;
            _currentUser = currentUser;
        }

        public async Task<List<ChildDto>> GetChilds()
        {
            List<Child> childs = await _childRepository.FindChildsForUserAsync(_currentUser.UserId);
            List<ChildDto> childsDto = new List<ChildDto>();
            foreach (var child in childs)
            {
                ChildDto childDto = EntityMapper.ToChildDto(child);
                childsDto.Add(childDto);
            }
            return childsDto;
        }
    }
}