using AutoMapper;
using YumApp.WebAPI.Dtos.ReservationDtos;
using YumApp.WebAPI.Dtos.CategoryDtos;
using YumApp.WebAPI.Dtos.EventDtos;
using YumApp.WebAPI.Dtos.FeatureDtos;
using YumApp.WebAPI.Dtos.MessageDtos;
using YumApp.WebAPI.Dtos.NotificationDtos;
using YumApp.WebAPI.Dtos.ProductDtos;
using YumApp.WebAPI.Dtos.ServiceDtos;
using YumApp.WebAPI.Dtos.TestimonialDtos;
using YumApp.WebAPI.Entities;
using YumApp.WebAPI.Dtos.AboutDtos;
using YumApp.WebAPI.Dtos.ImageDtos;
using YumApp.WebAPI.Dtos.EmployeeDtos;
using YumApp.WebAPI.Dtos.GroupDtos;

namespace YumApp.WebAPI.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<ResultEventDto,Event>().ReverseMap();
            CreateMap<UpdateEventDto,Event>().ReverseMap();
            CreateMap<CreateEventDto,Event>().ReverseMap();
            CreateMap<GetByIdEventDto,Event>().ReverseMap();

            CreateMap<ResultEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            CreateMap<GetByIdEmployeeDto, Employee>().ReverseMap();

            CreateMap<ResultCategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<GetByIdCategoryDto, Category>().ReverseMap();


            CreateMap<ResultImageDto, Image>().ReverseMap();
            CreateMap<UpdateImageDto, Image>().ReverseMap();
            CreateMap<CreateImageDto, Image>().ReverseMap();
            CreateMap<GetByIdImageDto, Image>().ReverseMap();

            CreateMap<ResultReservationDto, Reservation>().ReverseMap();
            CreateMap<UpdateReservationDto, Reservation>().ReverseMap();
            CreateMap<CreateReservationDto, Reservation>().ReverseMap();
            CreateMap<GetByIdReservationDto, Reservation>().ReverseMap();


            CreateMap<ResultAboutDto, About>().ReverseMap();
            CreateMap<UpdateAboutDto, About>().ReverseMap();
            CreateMap<CreateAboutDto, About>().ReverseMap();
            CreateMap<GetByIdAboutDto, About>().ReverseMap();

            CreateMap<ResultNotificationDto, Notification>().ReverseMap();
            CreateMap<UpdateNotificationDto, Notification>().ReverseMap();
            CreateMap<CreateNotificationDto, Notification>().ReverseMap();
            CreateMap<GetByIdNotificationDto, Notification>().ReverseMap();

            CreateMap<ResultFeatureDto, Feature>().ReverseMap();
            CreateMap<UpdateFeatureDto, Feature>().ReverseMap();
            CreateMap<CreateFeatureDto, Feature>().ReverseMap();
            CreateMap<GetByIdFeatureDto, Feature>().ReverseMap();

            CreateMap<ResultServiceDto, Service>().ReverseMap();
            CreateMap<UpdateServiceDto, Service>().ReverseMap();
            CreateMap<CreateServiceDto, Service>().ReverseMap();
            CreateMap<GetByIdServiceDto, Service>().ReverseMap();

            CreateMap<ResultTestimonialDto, Testimonial>().ReverseMap();
            CreateMap<UpdateTestimonialDto, Testimonial>().ReverseMap();
            CreateMap<CreateTestimonialDto, Testimonial>().ReverseMap();
            CreateMap<GetByIdTestimonialDto, Testimonial>().ReverseMap();

            CreateMap<ResultGroupReservationDto, GroupReservation>().ReverseMap();
            CreateMap<UpdateGroupReservationDto, GroupReservation>().ReverseMap();
            CreateMap<CreateGroupReservationDto, GroupReservation>().ReverseMap();
            CreateMap<GetByIdGroupReservationDto, GroupReservation>().ReverseMap();


            CreateMap<ResultMessageDto, Message>().ReverseMap();
            CreateMap<UpdateMessageDto, Message>().ReverseMap();
            CreateMap<CreateMessageDto, Message>().ReverseMap();
            CreateMap<GetByIdMessageDto, Message>().ReverseMap();

            CreateMap<ResultProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<GetByIdProductDto, Product>().ReverseMap();
            CreateMap<Product, ResultProductWithCategoryDto>().ForMember(x => x.CategoryName, y => y.MapFrom(z => z.Category.CategoryName)).ReverseMap();
        }
    }
}
