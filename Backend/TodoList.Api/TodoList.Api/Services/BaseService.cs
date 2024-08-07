using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using TodoList.Api.Dtos;
using TodoList.Api.Models;
using TodoList.Api.Repositories;

namespace TodoList.Api.Services
{
    public class BaseService<TDto, TModel> : IService<TDto> where TDto : IDto where TModel : IModel
    {
        protected readonly IRepository<TModel> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IRepository<TModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TDto> AddAsync(TDto dto)
        {
            var model = _mapper.Map<TModel>(dto);

            await _repository.AddAsync(model);

            return _mapper.Map<TDto>(model);
        }

        public Task DeleteAsync(Guid id)
        {
            return _repository.DeleteAsync(id);
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var results = await _repository.GetAllAsync();

            return results.Select(result => _mapper.Map<TDto>(result));
        }

        public virtual async Task<TDto> GetByContentAsync(string content)
        {
            var result = await _repository.GetByContentAsync(content);

            return _mapper.Map<TDto>(result);
        }

        public virtual async Task<TDto> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            return _mapper.Map<TDto>(result);
        }

        public bool IsExistsAsync(Guid id)
        {
            return _repository.IsExistsAsync(id);
        }

        public virtual Task UpdateAsync(Guid id, TDto dto)
        {
            return _repository.UpdateAsync(_mapper.Map<TModel>(dto));
        }

    }
}