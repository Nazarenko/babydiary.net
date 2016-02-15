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

        public async Task<List<ChildDto>> GetChildsAsync()
        {
            List<Child> childs = await _childRepository.FindChildsForUserAsync(_currentUser.UserId);
            List<ChildDto> childsDto = new List<ChildDto>();
            foreach (var child in childs)
            {
                ChildDto childDto = EntityMapper.ChildToDto(child);
                childsDto.Add(childDto);
            }
            return childsDto;
        }

        public async Task<ChildDto> SaveChildAsync(ChildDto childDto)
        {
            if (childDto.ChildId == 0)
            {
                var newChild = EntityMapper.DtoToChild(childDto);
                newChild.UserId = _currentUser.UserId;
                await _childRepository.CreateChildAsync(newChild);
                childDto.ChildId = newChild.ChildId;
                return childDto;
            }
            else
            {
                var child = await _childRepository.FindChildByIdAsync(childDto.ChildId);
                if (child == null)
                {
                    throw new NotImplementedException();
                }
                if (child.UserId != _currentUser.UserId)
                {
                    throw new NotImplementedException();
                }
                EntityMapper.DtoToChild(childDto, child);
                await _childRepository.SaveChangesAsync();
                return childDto;
            }
        }

        public async Task DeleteChildAsync(long childId)
        {
            var child = await _childRepository.FindChildByIdAsync(childId);
            if (child == null)
            {
                throw new NotImplementedException();
            }
            if (child.UserId != _currentUser.UserId)
            {
                throw new NotImplementedException();
            }
            await _childRepository.DeleteChildAsync(child);
        }
    }
}