using Applications.Domains.Model;
using AutoMapper;
using T.Application.Commons.AutoMapper;

namespace Applications.Service.Dto
{
    public class UserProfile : Profile, IProfile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }

    public static class PersonDtoExtension
    {
        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="dto">人员登记数据传输对象</param>
        public static User ToEntity(this UserDto dto)
        {
            if (dto == null)
                return new User();
            return dto.MapTo<User>();
        }

        /// <summary>
        /// 转换为数据传输对象
        /// </summary>
        /// <param name="entity">人员登记实体</param>
        public static UserDto ToDto(this User entity)
        {
            if (entity == null)
                return new UserDto();
            return entity.MapTo<UserDto>();
        }
    }
}